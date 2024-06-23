using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX;
    private bool isFalling = false;
    private bool facingRight = true;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * 10f;


        anim.SetBool("isRunning", Mathf.Abs(dirX) > 0 && rb.velocity.y == 0);
        anim.SetBool("isJumping", rb.velocity.y > 0);
        anim.SetBool("isFalling", isFalling);

    }

    private void FixedUpdate()
    {

        if (rb.velocity.y < 0 )
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    private void LateUpdate()
    {
        if ((dirX > 0 && !facingRight) || (dirX < 0 && facingRight))
            FlipCharacter();
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
}
