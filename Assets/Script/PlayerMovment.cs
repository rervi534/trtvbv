using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float speed = 10f;
    public float jumpPower;
    private Rigidbody2D body;
    private Animator anim;
    //public bool grounded;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private float wallJumpCooldown;
    private CapsuleCollider2D capsuleCollider;
    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent <Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        
       
        //flip
        if (horizontal > 0.01f)
        {
            transform.localScale= Vector3.one;
        }
        else if(horizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        //anim
        anim.SetBool("move",horizontal != 0);
        anim.SetBool("ground", IsGrounded());

        if (wallJumpCooldown < 0.2f)
        {
            
            body.velocity = new Vector2(horizontal * speed, body.velocity.y);

            if(onWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale= 3;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !IsGrounded())
        {
            wallJumpCooldown = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontal == 0 && IsGrounded() && !onWall();
    }



}
