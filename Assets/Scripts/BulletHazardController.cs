using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletHazardController : MonoBehaviour
{
    public GameObject onDeathEffectBlood;
    public GameObject bulletModel;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Destroy(bulletModel);
            
            //play blood prefab or particle effect here
            Instantiate(onDeathEffectBlood, other.gameObject.transform.position, Quaternion.identity);

            Invoke("Kill", 1f);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("TrueGround")) 
        {
            Destroy(gameObject);
        }
        
    }

    void Kill()
    {   
        //wait one second before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
