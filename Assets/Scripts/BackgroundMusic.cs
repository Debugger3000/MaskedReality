using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    private static BackgroundMusic _instance;

    void Awake()
    {
        // Check if an instance already exists
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); // Kill the duplicate
            return;
        }

        _instance = this;
        // Tell Unity not to kill this object when the scene reloads
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
