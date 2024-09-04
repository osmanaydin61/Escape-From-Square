using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    [SerializeField] Vector2 deadjump = new Vector2 (13f,13f);
    [SerializeField] float movement;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Animator myAnimator;
    float gravityScaleAt;
    BoxCollider2D myFeetCollider;
    CapsuleCollider2D myBodyCollider;
    bool isAlive = true;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAt = myRigidBody.gravityScale;
        
    }

   
    void Update()
    {
        if(!isAlive){return;}
        Run();
        FlipSprate();
        onClimb();
        dead();
       
        
        
    }

    void dead()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("enemies","hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("dead");
            myRigidBody.velocity = deadjump;
            FindObjectOfType<gameSession>().ProcessPlayerDeath();
        }

    }
    void OnFire(InputValue value)
    {
        if(!isAlive){return;}
        Instantiate(bullet,gun.position,transform.rotation);
    }
    

    void OnMove(InputValue value)
    {
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
       
    }
    void onClimb()
    {
        if(!isAlive){return;}
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("climbing")))
        {
            myRigidBody.gravityScale = gravityScaleAt;
            myAnimator.SetBool("climbing", false);
            return;
            
        }
        
            Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x,moveInput.y*climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.y)>Mathf.Epsilon;
           
            myAnimator.SetBool("climbing",playerHasHorizontalSpeed); 
           
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x*movement,myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity ;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x)>Mathf.Epsilon;
        
           myAnimator.SetBool("isRunning",playerHasHorizontalSpeed); 
        
    }
    void OnJump(InputValue value)
    {
        if(!isAlive){return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            return;
        }
        

        if(value.isPressed)
        {
            myRigidBody.velocity+= new Vector2(0f, jumpSpeed);
        }
    }

    void  FlipSprate ()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x)>Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x),1f);
        }
        
    }
}
