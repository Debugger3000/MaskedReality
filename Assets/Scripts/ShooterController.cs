using System;
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
        //spawn bullet and rotate according to shooter rotation
        var instantiatedBullet = Instantiate(BulletPrefab, projectileSpawnPoint.transform.position, gameObject.transform.rotation);
        //set velocity of bullet based on direction shooter is facing
        //quaternion to direction vector conversion
        float degrees = gameObject.transform.eulerAngles.z * Mathf.Deg2Rad;
        //calculate direction vector based off 
        Vector2 direction = new Vector2(Mathf.Cos(degrees), Mathf.Sin(degrees));
        instantiatedBullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;
    }
}
