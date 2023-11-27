using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    SpriteRenderer sr;
    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public Hitbox hitboxPrefab;
    public Projectile projectilePrefab;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (!spawnPointLeft || !spawnPointRight || !hitboxPrefab)
        {
            Debug.Log("Please set default values on " + gameObject.name);
        }
        if (projectileSpeed <= 0) projectileSpeed = 7.0f;
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Hitbox curHitbox = Instantiate(hitboxPrefab, spawnPointLeft.position, spawnPointLeft.rotation);
        }
        else
        {
            Hitbox curHitbox = Instantiate(hitboxPrefab, spawnPointRight.position, spawnPointRight.rotation);
        }
    }

    public void Shoot()
    {
        if (!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            curProjectile.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
