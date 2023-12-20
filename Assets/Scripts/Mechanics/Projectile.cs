using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime;
    [HideInInspector]
    public float speed;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0) lifetime = 2.0f;

        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }

        Debug.Log("Collided");
        if (collision.gameObject.CompareTag("Whip") && gameObject.CompareTag("Enemy Projectile"))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.lives--;
        }
    }
}
