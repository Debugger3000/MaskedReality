using UnityEngine;

public class TutorialMaskController : MonoBehaviour
{
    
    public Vector2 targetPosition;
    private float speed = 2f;

    void FixedUpdate()
    {
        // move towards target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerControllerScript playerController = other.gameObject.GetComponent<PlayerControllerScript>();
            if (playerController != null)
            {
                if (gameObject.CompareTag("RedMaskPickup"))
                {
                    playerController.hasRedMask = true;
                }
                else if (gameObject.CompareTag("GreenMaskPickup"))
                {
                    playerController.hasGreenMask = true;
                }
            }

            Destroy(gameObject);
        }
    }
}
