using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldHazardController : MonoBehaviour
{
    public GameObject onDeathEffectBlood;

    void Awake()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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
