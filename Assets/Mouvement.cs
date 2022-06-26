using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{

    public int movementSpeed;
    public int movementRatioBase;
    private int movementRatio;
    private float mouvementMultiplier;
    public int jumpPower;
    private bool isGrounded, isOnWallRight, isOnWallLeft;
    public Rigidbody2D Body;

    public Transform groundCheck1, groundCheck2, wallCheckUpRight, wallCheckDownRight, wallCheckUpLeft, wallCheckDownLeft;
    public LayerMask Platform;


    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        
    }

    void MovePlayer()
    {
        isGrounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, Platform);
        
        isOnWallRight = Physics2D.OverlapCircle(wallCheckUpRight.position, 0.2f, Platform) && Physics2D.OverlapCircle(wallCheckDownRight.position, 0.2f, Platform);
        isOnWallLeft = Physics2D.OverlapCircle(wallCheckUpLeft.position, 0.2f, Platform) && Physics2D.OverlapCircle(wallCheckDownLeft.position, 0.2f, Platform);


        if(isGrounded)
        {
            movementRatio = movementRatioBase;
        }
        else
        {
            movementRatio = movementRatioBase / 2;
        }
        
        if (Input.GetKey("d"))
        {

            if(mouvementMultiplier < 0)
            {
                mouvementMultiplier += Time.deltaTime * movementRatio;
            }
            else
            {
                mouvementMultiplier += Time.deltaTime * (movementRatio * 2);
            }

        }
        if (Input.GetKey("q"))
        {
            if (mouvementMultiplier > 0)
            {
                mouvementMultiplier -= Time.deltaTime * movementRatio;
            }
            else
            {
                mouvementMultiplier -= Time.deltaTime * (movementRatio * 2);
            }
            
        }
        if (!Input.GetKey("q") && !Input.GetKey("q"))
        {
            if (mouvementMultiplier > 0)
            {
                mouvementMultiplier -= Time.deltaTime;
            }
            if (mouvementMultiplier < 0)
            {
                mouvementMultiplier += Time.deltaTime;
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if(isGrounded)
            {
                Body.AddForce(Vector2.up * jumpPower);
            }
            else if(isOnWallRight)
            {
                Body.AddForce(Vector2.up * jumpPower);
                mouvementMultiplier = -1f;
            }
            else if(isOnWallLeft)
            {
                Body.AddForce(Vector2.up * jumpPower);
                mouvementMultiplier = 1f;
            }
        }

        //Body.AddForce(Vector2.right * movementSpeed * (mouvementMultiplier + 0.5f) * MovementDirecton);

        mouvementMultiplier = Mathf.Clamp(mouvementMultiplier, -1f, 1f);
        /*if (mouvementMultiplier < -1f)
        {
            mouvementMultiplier = -1;
        }
        else if (mouvementMultiplier > 1f)
        {
            mouvementMultiplier = 1;
        }*/


        Body.velocity = new Vector2(movementSpeed * (mouvementMultiplier), Body.velocity.y);
        
    }
    
}
