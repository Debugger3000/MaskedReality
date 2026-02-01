using UnityEngine;

public class TriggerZoneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created 

    public GameObject targetShooter;
        public void OnTriggerEnter2D(Collider2D other)
    {
        if (targetShooter != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                targetShooter.GetComponent<ShooterController>().isTriggered = true;
                //destroy this trigger zone after triggering
                Destroy(gameObject);
            }
        }
    }
}
