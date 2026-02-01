using UnityEngine;

public class NextLevelDoorController : MonoBehaviour
{
    public string nextLevelName;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("loadLevel", 0.75f);
        }
    }

    public void loadLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
    }
}
