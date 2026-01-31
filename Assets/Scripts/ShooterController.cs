using UnityEngine;

public class ShooterController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject BulletPrefab;

    public GameObject projectileSpawnPoint;

    private float shootInterval = 2f;

    private void Awake()
    {
        InvokeRepeating("shoot", shootInterval, shootInterval);
    }

    private void shoot()
    {

        var instantiatedBullet = Instantiate(BulletPrefab, projectileSpawnPoint.transform.position, Quaternion.identity);

        //set velocity
        instantiatedBullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(10f, 0f);
        
    }
}
