using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    public float speed = 5.0f;
    public float jumpForce = 300.0f;

    public bool isGrounded;
    public bool isAttacking;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius = 0.02f;
    public enum subweapon
    {
        None = 0,
        Axe = 1,
        Dagger = 2
    }
    public subweapon currentWeapon = subweapon.None;

    
    public void setSubweapon(subweapon sw)
    {
        currentWeapon = sw;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        

        if (rb == null) Debug.Log("No Rigidbody Reference");
        if (sr == null) Debug.Log("No Sprite Renderer Reference");
        if (anim == null) Debug.Log("No Aniimator Reference");
        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("Groundcheck set to default value");
        }

        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("Speed set to default value");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300.0f;
            Debug.Log("Jumpforce set to default value");
        }

        if (groundCheck == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            groundCheck = obj.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (curPlayingClips.Length > 0)
        {
            if (curPlayingClips[0].clip.name == "Attack" || curPlayingClips[0].clip.name == "JumpAttack")
                rb.velocity = new Vector2(0, rb.velocity.y);
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (Input.GetButton("Fire1"))
        {
            isAttacking = true;
        }
        else isAttacking = false;

        /*if (Input.GetButton("Fire2"))
        {
            isAttacking = true;
        }
        else isAttacking = false;*/

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsAttacking", isAttacking);

        if (hInput != 0) sr.flipX = (hInput > 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish"))
        {
            collision.transform.parent.GetComponent<Enemy>().TakeDamage(9999);
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}
