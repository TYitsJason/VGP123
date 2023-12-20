using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{

    Rigidbody2D rb;
    public float xSpeed;
    public GameObject squish;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xSpeed <= 0)
        {
            xSpeed = 3;
        }
    }

    public override void TakeDamage(int damage)
    {
        _health -= damage;
        if (damage == 9999)
        {
            anim.SetTrigger("Squish");
            Destroy(transform.parent.gameObject, 1);
            return;
        }
        Debug.Log("Enemy Walker took " + damage.ToString() + " damage.");
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        if (_health <= 0 && curPlayingClips[0].clip.name != "Squish")
        {
            anim.SetTrigger("Death");
            Destroy(transform.parent.gameObject, 1);
        }
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        if (curPlayingClips[0].clip.name == "Walk")
        {
            rb.velocity = sr.flipX ? new Vector2(xSpeed, rb.velocity.y) : new Vector2(-xSpeed, rb.velocity.y);

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.lives--;
            Debug.Log("Hit player");
            sr.flipX = !sr.flipX;
        }
    }
}
