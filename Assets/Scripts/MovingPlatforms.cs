using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;
    public bool loop = true;
    public bool pingPong = false;

    int targetIndex = 0;
    int direction = 1;

    void FixedUpdate()
    {
        if (points == null || points.Length == 0) return;

        Vector3 target = points[targetIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            if (pingPong)
            {
                if (targetIndex == points.Length - 1) direction = -1;
                if (targetIndex == 0) direction = 1;
                targetIndex += direction;
            }
            else
            {
                targetIndex++;
                if (targetIndex >= points.Length)
                    targetIndex = loop ? 0 : points.Length - 1;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.transform.SetParent(null);
    }
}
