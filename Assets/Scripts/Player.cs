using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    //public float speed;
    //public float jumpForce;
    //private float moveInput;

    bool isAlive = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float checkRadius;
    float gravityScaleAtStart;

    private int extraJumps;
    public int extraJumpsValue;

    public int _playerHealth = 3;

    public UIManager _uiManager;
    public EnemyMovement EnemyMovement;
    
    // Use this for intialization
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        extraJumps = extraJumpsValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }

    private void ClimbLadder()
    {
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }

    private void Jump()
    {

        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            myRigidBody.velocity = Vector2.up * jumpSpeed;
            extraJumps--;
        }else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            myRigidBody.velocity = Vector2.up * jumpSpeed;
        }

        //    Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
        //  myRigidBody.velocity += jumpVelocityToAdd;
        // }
        // if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "EnemyProjectile")
        {
            DecreaseHealth();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyColliders")
        {
            EnemyMovement.EnemyLook();
        }
    }

    public void AddHealth()
    {
        _playerHealth += 1;
        _uiManager.HealthUpdate(_playerHealth);
    }

    public void DecreaseHealth()
    {
        _playerHealth -= 1;
        _uiManager.HealthUpdate(_playerHealth);
        if (_playerHealth <= 0)
        {
            isAlive = false;
            Die();
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            _playerHealth -= 3;
            _uiManager.HealthUpdate(_playerHealth);
            isAlive = false;
        }
        if(isAlive == false)
        {             
             myAnimator.SetTrigger("Dying");
        //    GetComponent<Rigidbody2D>().velocity = deathKick;
        }     
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            if (myRigidBody.velocity.x < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180f, 0);
            }
            else if (myRigidBody.velocity.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        // Instantiate(bullet, firePoint, transform.rotation);
        
    }

}
