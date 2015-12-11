using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedMilestoneCount;
    private float moveSpeedStore;
    private float speedMilestoneCountStore;
    private float speedIncreaseMilestoneStore;

    public float jumpForce;
    public float jumpDuration;
    private float jumpDurationCounter;

    private bool stoppedJumping;
    private bool canDoubleJump;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundDetecter;
    public float groundDetecterRadius;

    public GameManager theGameManager;

    private Animator myAnimator;
    public AudioSource jumpSound;
    public AudioSource deathSound;

    //private Collider2D myCollider;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        jumpDurationCounter = jumpDuration;

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;
        //calling animator
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundDetecter.position,groundDetecterRadius,whatIsGround);

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }
        
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
    

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }

            if(!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        if((Input.GetKey (KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if(jumpDurationCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpDurationCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpDurationCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpDurationCounter = jumpDuration;
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            deathSound.Play();
            
        }
    }
}
