using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

    public float moveSpeed, jumpForce;

    public Transform groundCheckPoint;
    public LayerMask whatisGround;

    private bool isGrounded;

    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatisGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        //flip direction
        if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1, 1f);
        }


        //anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        //anim.SetBool("isGrounded", isGrounded);
    }
}
