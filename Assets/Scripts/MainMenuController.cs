using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    public float testee = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void playGame() {

        // change to next scene using scene name
        SceneManager.LoadScene("Zone01_Remake");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
