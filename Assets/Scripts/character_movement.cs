using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_movement : MonoBehaviour
{
    Vector2 moveInput;
    Vector2 jumpInput;
    Rigidbody2D rb2d;
    Animator myAnimator;
    CircleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;



    [SerializeField]float runSpeed = 10f;
    [SerializeField]float jumpSpeed = 10f;
    [SerializeField]float climbSpeed = 10f;
    [SerializeField]Vector2 deathKick=new Vector2(10f,10f );
    [SerializeField]GameObject bullet;
    [SerializeField]Transform gun;
    float gravityScaleAtStart;
    bool isAlive=true; 
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
        myAnimator=GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CircleCollider2D>();
        gravityScaleAtStart=rb2d.gravityScale;
        myBoxCollider = GetComponent<BoxCollider2D>();
    }
    
    void OnFire(InputValue value)
    {
        if(!isAlive){return;}
        Instantiate(bullet,gun.position,transform.rotation); 
    }
    // Update is called once per frame
    void Update()
    {
        if(!isAlive){return;}
        Run();
        flipSprite();
        ClimbLadder();
        Die();
        swimWater();
    }
    void OnMove(InputValue value)
    {
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value)
    {
        if(!isAlive){return;}
        if(!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if(value.isPressed)
        {
            rb2d.velocity += new Vector2(0f,jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed,rb2d.velocity.y);
        rb2d.velocity=playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHasHorizontalSpeed);
    }
    void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x),1f);
        }
         
    }
    void ClimbLadder()
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb2d.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing",false);
            return;
        }
        
        
        Vector2 climbVelocity = new Vector2 (rb2d.velocity.x,moveInput.y * climbSpeed);
        rb2d.velocity=climbVelocity;
        rb2d.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",playerHasVerticalSpeed);
    }

    void Die()
    {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive=false;
            myAnimator.SetTrigger("Dying");
            rb2d.velocity=deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }
    void swimWater()
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            runSpeed=4f;
            rb2d.gravityScale=0f;
            jumpSpeed=5f;
            return;
        }
        else{
            runSpeed=10f;
            rb2d.gravityScale=2f;
            jumpSpeed=9f;
        }
    }
}
