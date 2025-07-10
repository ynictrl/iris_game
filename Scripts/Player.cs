using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D bc2D;
    public BoxCollider2D feet;
    Animator anim;

    public GameObject go_trigger;
    [SerializeField] LayerMask layerGround;
    public ShakeCamera cam;
    public static bool pausePlayer;
    public static bool deadPlayer;

    [Header("Move")]
    //public bool isGrounded;
    public float moviment_x;
    public float moviment_y;
    public float input_x = 1;
    public float moveSpeed_default = 4;
    public float moveSpeed_attacking = 2;
    public float moveSpeed_current;
    public bool onMove = true;
    public bool lowered;
    public bool onLow;
    public bool hurted;

    [Header("Jump")]
    public bool isJumping;
    // 5, 5, 15, 3
    [SerializeField] float jumpHeight = 3;
    [SerializeField] float gravityScale = 5;
    [SerializeField] float fallGravityScale = 5;
    [SerializeField] bool jumpCanceled;
    [SerializeField] float jumpForceCancel = 10;

    [Header("Dash")]
    public float dashForce = 9;
    public bool isDashing;
    public float dashTime = 0.3f;
    public bool onDash = true;
    public float dashTimeInterval = 1f;

    [Header("Attack")]

    // public GameObject[] attackObjs; // r|l
    // public GameObject atualAttackObj;
    // public float damageCurrent;
    public bool isAttacking;
    public float attackTime = 0.6f; // 0.3f
    public bool onAttack = true;
    public float attackTimeInterval = 0.7f; // 0.4f

    public float sufferingTime = 0.4f;
    public float invicibleTime = 1f;

    public bool handSide; // false: left, true: right

    [Header("Health")]
    public Image healthBar;
    public GameObject healthBarObject;
    float healthPorcent;
    public float healthCurrent;

    public float healthMax;


    [Header("Anim")]

    int xInputHash = Animator.StringToHash("xInput");

    public enum PlayerStates
    {
        IDLE, WALK, JUMP, FALL, LOW, PUNCH_L, PUNCH_R,
        DASH, HURT, LOWPUNCH_L, LOWPUNCH_R, JUMPPUNCH_L, JUMPPUNCH_R, DEAD
    }
    public PlayerStates currentState;

    public Punch punch;

    public GameObject currentOneWayPlatform;

    [Header("Sound")]

    public GameObject sound_Dash;
    public GameObject sound_Hurt;
    public GameObject sound_Jump;


    // Start is called before the first frame update
    void Start()
    {
        deadPlayer = false;
        rb = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        moveSpeed_current = moveSpeed_default;
    }

    // Update is called once per frame
    void Update()
    {
        ControlAnim();

        if (healthCurrent <= 0)
        {
            deadPlayer = true;
            cam.Shake(1.25f, 1f);
        }

    }

    void FixedUpdate()
    {
        if (!deadPlayer)
        {
            Jump();
            ControlJump();

            Attack();
            Dash();

            ControlMove();
            Move();
        }

    }

    public bool isGrounded()
    {
        // RaycastHit2D ground = Physics2D.BoxCast(bc2D.bounds.center, bc2D.bounds.size, 0, Vector2.down, 0.1f, layerGround);
        RaycastHit2D ground = Physics2D.BoxCast(feet.bounds.center, feet.bounds.size, 0, Vector2.down, 0.1f, layerGround);
        jumpCanceled = false;
        // sisGrounded_ = true;
        return ground.collider != null;
    }

    public void Move()
    {
        if (!isDashing /*&& !lowered && !isAttacking*/ && onMove)
        {
            moviment_y = Input.GetAxis("Vertical");
            moviment_x = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(moviment_x * moveSpeed_current, rb.linearVelocity.y);
        }
        // if (!onMove)
        // {
        //     rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        // }

        if (!isAttacking)
        {
            if (Input.GetKey(KeyCode.S) && onLow && !isDashing && !isJumping)
            {
                lowered = true;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            else
            {
                lowered = false;
            }
        }

        // if(isAttacking && lowered)
        // {
        //     lowered = true;
        //     rgbd2D.velocity = new Vector2(0, rgbd2D.velocity.y); 
        // }else{
        //     lowered = false;
        // }

        if (isGrounded() && !isDashing)
        {
            onLow = true;
        }
        else
        {
            onLow = false;
        }

        if (lowered && Input.GetKey(KeyCode.Space) && !isJumping)
        {
            
            if (currentOneWayPlatform != null)
            {
                isJumping = true;
                StartCoroutine(DisableCollisionPlatform());
            }
            
        }

        // if(moviment_x != 0)
        // {
        // m_animator.SetFloat("xMove", moviment_x);
        // }
        // anim.SetFloat("xMove", moviment_x);
        // if(moviment_x != 0)
        // {
        //     currentState = PlayerStates.WALK;
        //     anim.SetFloat("xMove", moviment_x);
        // }else{
        //     currentState = PlayerStates.IDLE;
        // }
    }


    public void ControlMove()
    {
        if (!isDashing && !isAttacking && onMove)
        {
            if (moviment_x > 0)
            {
                input_x = 1;
            }
            if (moviment_x < 0)
            {
                input_x = -1;
            }
        }
    }

    void ControlAnim()
    {
        anim.SetFloat(xInputHash, input_x);

        switch (currentState)
        {
            case PlayerStates.IDLE:
                anim.Play("IDLE");
                break;
            case PlayerStates.WALK:
                anim.Play("WALK");
                break;
            case PlayerStates.JUMP:
                anim.Play("JUMP");
                break;
            case PlayerStates.FALL:
                anim.Play("FALL");
                break;
            case PlayerStates.LOW:
                anim.Play("LOW");
                break;
            case PlayerStates.PUNCH_L:
                anim.Play("PUNCH_L");
                break;
            case PlayerStates.PUNCH_R:
                anim.Play("PUNCH_R");
                break;
            case PlayerStates.DASH:
                anim.Play("DASH");
                break;
            case PlayerStates.HURT:
                anim.Play("HURT");
                break;
            case PlayerStates.LOWPUNCH_L:
                anim.Play("LOWPUNCH_L");
                break;
            case PlayerStates.LOWPUNCH_R:
                anim.Play("LOWPUNCH_R");
                break;
            case PlayerStates.JUMPPUNCH_L:
                anim.Play("JUMPPUNCH_L");
                break;
            case PlayerStates.JUMPPUNCH_R:
                anim.Play("JUMPPUNCH_R");
                break;
            case PlayerStates.DEAD:
                anim.Play("DEAD");
                break;
        }

        if (!deadPlayer)
        {


            // punch
            if (!isAttacking)
            {
                punch.currentState = Punch.PunchStates.IDLE;
            }
            else
            {
                if (!hurted)
                {
                    if (!handSide)
                    {
                        punch.currentState = Punch.PunchStates.LEFT;
                    }
                    else
                    {
                        punch.currentState = Punch.PunchStates.RIGHT;
                    }
                }
                else
                {
                    punch.currentState = Punch.PunchStates.IDLE;
                }

            }

            if (isGrounded())
            {

                if (!isAttacking && !lowered && !hurted)
                {
                    if (moviment_x == 0)
                    {
                        currentState = PlayerStates.IDLE;
                    }
                    else
                    {
                        currentState = PlayerStates.WALK;
                    }
                }

                if (isAttacking && !lowered)
                {
                    if (!handSide)
                    {
                        currentState = PlayerStates.PUNCH_L;
                        // anim.speed = 2;
                    }
                    else
                    {
                        currentState = PlayerStates.PUNCH_R;
                    }
                }
                if (lowered)
                {
                    if (isAttacking)
                    {
                        if (!handSide)
                        {
                            currentState = PlayerStates.LOWPUNCH_L;
                        }
                        else
                        {
                            currentState = PlayerStates.LOWPUNCH_R;
                        }
                    }
                    else
                    {
                        currentState = PlayerStates.LOW;
                    }


                }
            }
            else
            {
                if (isJumping)
                {
                    if (isAttacking)
                    {
                        if (!handSide)
                        {
                            currentState = PlayerStates.JUMPPUNCH_L;
                        }
                        else
                        {
                            currentState = PlayerStates.JUMPPUNCH_R;
                        }
                    }
                    else
                    {
                        currentState = PlayerStates.JUMP;
                    }

                }
                else
                {
                    if (!isAttacking)
                    {
                        currentState = PlayerStates.FALL;
                    }

                }
            }

            if (isDashing)
            {
                currentState = PlayerStates.DASH;
            }
            if (hurted)
            {
                currentState = PlayerStates.HURT;
            }


        }
        else
        {
            currentState = PlayerStates.DEAD;
        }

    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded() && !isJumping && !isDashing && !lowered && !hurted && onMove)
        {
            rb.gravityScale = gravityScale;
            float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            jumpCanceled = false;

            GameObject cloneSound = Instantiate(sound_Jump, transform.position, transform.rotation);
            Destroy(cloneSound, 2f);
        }

        if (isJumping)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                jumpCanceled = true;
            }
        }

        if (rb.linearVelocity.y <= 0 && !isDashing)
        {
            rb.gravityScale = fallGravityScale;
            isJumping = false;
        }
    }

    void ControlJump()
    {
        if (jumpCanceled && isJumping && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.down * jumpForceCancel);
            // isJumping = false;
        }
    }

    void Dash()
    {
        if ((Input.GetKey(KeyCode.I)) && !isAttacking && !isDashing && onDash && !hurted) //Input.GetKey(KeyCode.E)
        {
            if (input_x == 1)
            {

                rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(dashForce, 0);
            }
            else
            {
                // input_x == -1

                rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(dashForce * -1, 0);
            }
            GameObject cloneSound = Instantiate(sound_Dash, transform.position, transform.rotation);
            Destroy(cloneSound, 2f);

            StartCoroutine(dashing());
            StartCoroutine(dashInterval());
        }

    }
    IEnumerator dashing()
    {
        isDashing = true;
        lowered = false;
        rb.gravityScale = 0;
        moviment_x = 0;
        onMove = false;

        go_trigger.SetActive(false);

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        moviment_x = 0;
        rb.gravityScale = fallGravityScale;
        onMove = true;

        go_trigger.SetActive(true);
    }
    IEnumerator dashInterval()
    {
        onDash = false;
        yield return new WaitForSeconds(dashTimeInterval);
        onDash = true;
    }

    void Attack()
    {
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.O)) && !isDashing && onAttack && onMove)
        {
            // // soco rápido
            // attackTime = 0.3f;
            // attackTimeInterval = 0.4f;
            // punch.anim.speed = 2;

            handSide = false;
            StartCoroutine(attacking());
            StartCoroutine(attackInterval());
        }
        if ((Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.P)) && !isDashing && onAttack && onMove)
        {
            // // soco default
            // attackTime = 0.6f;
            // attackTimeInterval = 0.7f;
            // punch.anim.speed = 1;

            handSide = true;
            StartCoroutine(attacking());
            StartCoroutine(attackInterval());
        }
    }

    IEnumerator attacking()
    {
        // onAttack = false;
        isAttacking = true;
        // atualAttackObj.SetActive(true);
        // rgbd2D.gravityScale = 0;
        // moviment_x = 0;
        // rgbd2D.velocity = new Vector2(rgbd2D.velocity.x, 0);


        if (isGrounded())
        {
            //onMove = false;
            if (lowered)
            {
                lowered = true;
                onLow = true;
                moveSpeed_current = 0;
            }
            else
            {
                moveSpeed_current = moveSpeed_attacking;
            }
        }
        else
        {
            moveSpeed_current = moveSpeed_default;

        }



        yield return new WaitForSeconds(attackTime);

        // onAttack = true;
        isAttacking = false;
        // atualAttackObj.SetActive(false);

        //onMove = true;

        moveSpeed_current = moveSpeed_default;

        if (!Input.GetKey(KeyCode.S))
        {
            lowered = false;
        }


        // moviment_x = 0;
        // rgbd2D.gravityScale = fallGravityScale;
    }
    IEnumerator attackInterval()
    {
        onAttack = false;
        yield return new WaitForSeconds(attackTimeInterval);
        onAttack = true;
    }


    public void UpdateHealthBar()//atualização da barra de vida
    {
        healthPorcent = healthCurrent / healthMax;
        healthBar.fillAmount = healthPorcent;

        if (healthCurrent < 0)
        {
            healthCurrent = 0;
            //healthBar.fillAmount = 0;
        }
        if (healthCurrent >= healthMax)
        {
            healthCurrent = healthMax;
            //healthBar.fillAmount = 1;
        }
    }

    public void UpdateHealth(float value)//atualizando vida
    {
        healthCurrent += value;
        UpdateHealthBar();
    }

    IEnumerator SufferingDamage()//tomando dano
    {
        hurted = true;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        GetComponent<SpriteRenderer>().color = new Color(1f, .2f, .2f, 1f);//red
        onMove = false;
        yield return new WaitForSeconds(sufferingTime);
        hurted = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);//normal  
        onMove = true;
    }
    IEnumerator WhenInvicicle()//tempo invencivel
    {
        go_trigger.SetActive(false);
        yield return new WaitForSeconds(invicibleTime);
        go_trigger.SetActive(true);
    }

    public void Hit(float dmg)//levando dano
    {
        GameObject cloneSound = Instantiate(sound_Hurt, transform.position, transform.rotation);
        Destroy(cloneSound, 2f);

        UpdateHealth(-dmg);
        //healthBarObject.SetActive(true);
        StartCoroutine(SufferingDamage());
        StartCoroutine(WhenInvicicle());
        StartCoroutine(cam.Shaking(1.2f, 1));
    }

    IEnumerator DisableCollisionPlatform()
    {
        lowered = false;
        // isJumping = true;
        // Collider2D platformCollider = currentOneWayPlatform.GetComponent<Collider2D>();
        PlatformEffector2D platformCollider = currentOneWayPlatform.GetComponent<PlatformEffector2D>();

        // Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), platformCollider);
        platformCollider.colliderMask = LayerMask.NameToLayer("TransparentFX");
        yield return new WaitForSeconds(0.25f);
        // Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), platformCollider, false);
        platformCollider.colliderMask = LayerMask.NameToLayer("Feet");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = other.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

}
