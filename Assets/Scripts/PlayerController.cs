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

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        Debug.Log(hInput);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        if (Input.GetButton("Fire1"))
        {
            isAttacking = true;
        }
        else isAttacking = false;
        Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDirection;

        anim.SetFloat("hInput", Mathf.Abs(hInput));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsAttacking", isAttacking);
    }
}
