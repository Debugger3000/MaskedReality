using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletHazardController : MonoBehaviour
{
    public GameObject onDeathEffectBlood;
    public GameObject bulletModel;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private LayerMask currentLayerCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentLayerCollider = Camera.main.GetComponent<CameraController>().getCurrentMask();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(bulletModel);

            rb.linearVelocity = new Vector2(0f, 0f);
            
            //play blood prefab or particle effect here
            Instantiate(onDeathEffectBlood, other.gameObject.transform.position, Quaternion.identity);

            Invoke("Kill", 1f);
        }
        else
        {
            boxCollider.enabled = false;
            Destroy(bulletModel);
            Invoke("destroyBullet", 1.1f);
        }
        
    }

    void Kill()
    {   
        //wait one second before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void destroyBullet()
    {
        Destroy(gameObject);
    }
}
