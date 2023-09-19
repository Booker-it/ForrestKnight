using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class KnightController : MonoBehaviour
{
   
    public float moveSpeed = 5f;

    //private bool _isMoving = false;
    //public bool IsMoving { get
    //    {
    //        return _isMoving;
    //    } 
    //    private set
    //    {
    //        _isMoving = value;
    //        int tmp;
    //        if (_isMoving)
    //        {
    //            tmp = 1;
    //        }
    //        else
    //            tmp = 0;
    //        //delayToIdle = 0.05f;
    //        animator.SetInteger("AnimState", tmp);
    //    }
    //}


    Rigidbody2D rb;
    private Animator animator;

    [Header("Character Stuff")]
    public Knight knight;
    [SerializeField] GameObject slideDust;

    [Header("Camera Stuff")]
    [SerializeField] private GameObject cameraFollowGo;
    private CameraFollowObject cameraFollowObject;

    [Header("Jump stuff")]
    [SerializeField] int limitJump = 2;
    [SerializeField] int numberJump;

    private bool isWallSliding = false;
    private bool grounded = false;

    [SerializeField] float jumpForce = 14.3f;

    private Sensor_HeroKnight groundSensor;
    private Sensor_HeroKnight wallSensorR1;
    private Sensor_HeroKnight wallSensorR2;
    private Sensor_HeroKnight wallSensorL1;
    private Sensor_HeroKnight wallSensorL2;
    
    readonly int facingDirection = 1;


    //private float delayToIdle = 0.0f;
    private int currentAttack = 0;
    private float timeSinceAttack = 0.0f;

    public MagicSignManager msm;

    // Vector2 moveInput;

    PlayerInputActions controls;
    float direction = 0;


    [Header("Audio")]
    [SerializeField] private AudioSource collectAudio;
    [SerializeField] private AudioSource jumpAudio;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        cameraFollowObject = cameraFollowGo.GetComponent<CameraFollowObject>();

        controls = new PlayerInputActions();
        controls.Enable();
        controls.Test.Move.performed += ctx =>
        {
                direction = ctx.ReadValue<float>();
                if ((direction < 0 || direction > 0) && !(knight.isDead))
                {
                    TurnCheck();
                }

                if (Mathf.Abs(direction) > Mathf.Epsilon)
                {
                    animator.SetInteger("AnimState", 1);

                }
                else
                {
                    animator.SetInteger("AnimState", 0);
                }
        };

        controls.Test.Move.canceled += ctx =>
        {
            direction = ctx.ReadValue<float>();
            direction = 0;
        };

        controls.Test.Jump.performed += ctx =>
        {
            if (ctx.performed && grounded)
            {

                Debug.Log("jump");
                jumpAudio.Play();
                animator.SetTrigger("Jump");
                grounded = false;
                animator.SetBool("Grounded", grounded);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                groundSensor.Disable(0.2f);

            }

            if (ctx.performed && numberJump < limitJump && isWallSliding) // double jump if wallsliding on the wall
            {
                jumpAudio.Play();
                animator.SetTrigger("Jump");
                grounded = false;
                animator.SetBool("Grounded", grounded);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                groundSensor.Disable(0.2f);
                numberJump += 1;
            }
        };

    }
    // Start is called before the first frame update
    void Start()
    {
        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

        // IsMoving = false; // at the start character is not moving
        animator.SetInteger("AnimState", 0); // at the start the character must not moving

    }

    // Update is called once per frame
    void Update()
    {


        // Increase timer that controls attack combo

        timeSinceAttack += Time.deltaTime;

        //Check if character just landed on the ground
        if (!grounded && groundSensor.State())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
            numberJump = 0;
        }

        //Check if character just started falling
        if (grounded && !groundSensor.State())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }

        //Set AirSpeed in animator
        animator.SetFloat("AirSpeedY", rb.velocity.y);

        isWallSliding = (wallSensorR1.State() && wallSensorR2.State()) || (wallSensorL1.State() && wallSensorL2.State());
        animator.SetBool("WallSlide", isWallSliding);

    }

    private void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        
    }

    //public void OnMove(InputAction.CallbackContext ctx)
    //{
    //    moveInput = ctx.ReadValue<Vector2>();

    //    IsMoving = moveInput != Vector2.zero;

    //    if ((moveInput.x < 0 || moveInput.x > 0) && !(knight.isDead))
    //    {
    //        TurnCheck();
    //    }
    //}

    //public void OnJump(InputAction.CallbackContext ctx)
    //{
    //    if (ctx.performed && grounded)
    //    {

    //        Debug.Log("jump");
    //        jumpAudio.Play();
    //        animator.SetTrigger("Jump");
    //        grounded = false;
    //        animator.SetBool("Grounded", grounded);
    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //        groundSensor.Disable(0.2f);

    //    }

    //    if (ctx.performed && numberJump < limitJump && isWallSliding) // double jump if wallsliding on the wall
    //    {
    //        jumpAudio.Play();
    //        animator.SetTrigger("Jump");
    //        grounded = false;
    //        animator.SetBool("Grounded", grounded);
    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //        groundSensor.Disable(0.2f);
    //        numberJump += 1;
    //    }
    //}

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && timeSinceAttack > 0.25f && !knight.isDead)
        {

            currentAttack++;

            // Loop back to one after third attack
            if (currentAttack > 3)
                currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (timeSinceAttack > 1.0f)
                currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            animator.SetTrigger("Attack" + currentAttack);

            // Reset timer
            timeSinceAttack = 0.0f;

        }
    }

    public void OnBlock(InputAction.CallbackContext ctx)
    {
        if (!knight.isDead && ctx.performed)
        {
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
        }
        else
        {
            animator.SetBool("IdleBlock", false);
        }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (facingDirection == 1)
            spawnPosition = wallSensorR2.transform.position;
        else
            spawnPosition = wallSensorL2.transform.position;

        if (slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(facingDirection, 1, 1);
        }
    }

    private void TurnCheck()
    {
        if (direction > 0 && !knight.IsFacingRight)
        {
            Turn();
        }
        else if (direction < 0 && knight.IsFacingRight)
        {
            Turn();

        }
    }

    private void Turn()
    {
        if (knight.IsFacingRight)
        {
            Vector3 rotator = new(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            knight.IsFacingRight = !knight.IsFacingRight;

            //turn camera follow object
            cameraFollowObject.CallTurn();
        }

        else
        {
            Vector3 rotator = new(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            knight.IsFacingRight = !knight.IsFacingRight;

            //turn camera follow object
            cameraFollowObject.CallTurn();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MagicSign"))
        {
            collectAudio.Play();
            Destroy(other.gameObject);
            msm.magicSignCount ++;
        }
        
    }
}
