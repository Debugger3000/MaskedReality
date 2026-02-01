using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldHazardController : MonoBehaviour
{
    public GameObject onDeathEffectBlood;
    public AudioSource audioSource;
    public AudioClip deathClip;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //play death audio
            if (deathClip != null)
            {
                audioSource.PlayOneShot(deathClip);
            }

            Destroy(other.gameObject);

            

            //play blood prefab or particle effect here
            Instantiate(onDeathEffectBlood, other.gameObject.transform.position, Quaternion.identity);

            

            Invoke("Kill", 1f);
        }
        
    }

    void Kill()
    {
    //wait one second before reloading the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
