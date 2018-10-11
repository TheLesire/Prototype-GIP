using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float jumpPower = 330f;
    [SerializeField] Transform GroundTrigger;
    [SerializeField] float GroundTriggerRadius = 1f;
    [SerializeField] LayerMask GroundLayer;



    Rigidbody2D rb;
    SpriteRenderer sr;
    float curSpeed = 0f;
    bool jump = false;
    bool isGrounded = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        curSpeed = Input.GetAxis("Horizontal") * maxSpeed;

        ChangeDirection();

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }


    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundTrigger.position, GroundTriggerRadius, GroundLayer);
        Move();
        Jump();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GroundTrigger.position, GroundTriggerRadius);
    }
    private void Move()
    {
        rb.velocity = new Vector2(curSpeed, rb.velocity.y);
    }


    void ChangeDirection()
    {
        if( curSpeed > 0 && sr.flipX )
        {
            sr.flipX = false;
        }
        else if( curSpeed < 0 && !sr.flipX )
        {
            sr.flipX = true;
        }
    }


    void Jump()
    {
        if(jump)
        {
            jump = false;
            rb.AddForce(Vector2.up * jumpPower);
        }
        

    }
}

    