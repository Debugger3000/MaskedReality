using UnityEngine;

public class MaskPickupController : MonoBehaviour
{

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
