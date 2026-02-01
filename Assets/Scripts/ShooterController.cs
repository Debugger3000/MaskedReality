using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject BulletPrefab;

    public GameObject projectileSpawnPoint;

    [SerializeField]
    public float shootInterval = 2f;

    [SerializeField]
    public float shootDelay = 2f;

    public bool aimsAtPlayer;

    public bool usesColliderTrigger;

    public bool isTriggered = false;

    private void Awake()
    {
        //start shooting immediately if not using collider trigger
        if (!usesColliderTrigger)
        {
            InvokeRepeating("shoot", shootDelay, shootInterval);
        }
    }

    private void FixedUpdate()
    {
        //rotate to face player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && aimsAtPlayer)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        //start shooting if triggered by collider
        if (usesColliderTrigger && isTriggered)
        {
            InvokeRepeating("shoot", shootDelay, shootInterval);
            isTriggered = false; //reset trigger to avoid multiple invocations
        }
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
