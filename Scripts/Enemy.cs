using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // [Header("Status")]
    // public int typeEnemy; // 0: asteroid/ 1: atirador/ 2: boss

    public enum EnemyType
    {
        SKULL, SHOOTERSKULL, RAT, BAT
    }
    public EnemyType currentType;

    [Header("Components")]
    ShakeCamera cam;
    private Rigidbody2D rb;
    GameObject player;
    public GameObject contactDamage;
    // public GameObject GO_sprite;

    [Header("Move")]
    public bool onMove = true;
    public float moveSpeed_default;
    public float moveSpeed_current;
    public float direction_x;

    // public float jumpHitForceX_default;
    public float jumpHitForceX_current;
    public float jumpHitForceY_current = 6;
    public bool isGrounded;

    public float initX;
    public float deltaX;
    public float initY;

    [Header("Health")]
    public float healthCurrent;
    public float healthMax;

    [Header("Hurt")]
    public float sufferingTime;
    // public bool onDamageContact = true;
    // public float damageContact;

    [Header("Animation")]
    // //public float speedRotation;
    public int state; // -1 dead, 0 idle, 1 hurt, 2 attack
                      // public bool boolAnim; //hit

    [Header("Sounds")]
    public AudioSource sound_hurt;

    // [Header("Shooter")]
    // public Transform spawnBullet;
    // public float fireRate;
    // float nextFire;
    // public bool onFire;
    // public GameObject bulletObject;
    // public float speedBullet;

    // [Header("Boss")]
    // public Transform wb;

    //[Header("Other")]


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("Cam").GetComponent<ShakeCamera>();

        initX = transform.position.x;
        initY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Animator>().SetInteger("State", state);
        MoveLoop();

        if (healthCurrent <= 0)
        {
            onMove = false;
            contactDamage.SetActive(false);
            state = -1;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (currentType == EnemyType.BAT)
            {
                rb.gravityScale = 6f;
            }
        }



        // if (transform.position.x == )
        // {
        //     direction_x *= -1;
        //     if (GetComponent<SpriteRenderer>().flipX == true)
        //     {
        //         GetComponent<SpriteRenderer>().flipX = false;
        //     }
        //     else
        //     {
        //         GetComponent<SpriteRenderer>().flipX = true;
        //     }
        // }

        if (onMove && isGrounded)
        {
            if (direction_x == 1)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (direction_x == -1)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }



    }

    void MoveLoop()
    {
        if (onMove && isGrounded)
        {
            // transform.Translate(Vector2.right * speed * Time.deltaTime);
            rb.linearVelocity = new Vector2(direction_x * moveSpeed_current, rb.linearVelocity.y);

            if (transform.position.x >= initX + deltaX)
            {
                direction_x = -1;
                rb.linearVelocity = new Vector2(direction_x * moveSpeed_current, rb.linearVelocity.y);
            }

            if (transform.position.x <= initX - deltaX)
            {
                direction_x = 1;
                rb.linearVelocity = new Vector2(direction_x * moveSpeed_current, rb.linearVelocity.y);
            }

        }
    }

    // Health
    public void UpdateHealthBar()//atualização da barra de vida
    {
        // healthPorcent = healthCurrent/healthMax;
        // healthBar.fillAmount = healthPorcent;

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

    IEnumerator SufferingDamage()//tomando o tiro
    {
        //numAnim = 1;
        // rb.linearVelocity = new Vector2(1, 1);
        GetComponent<SpriteRenderer>().color = new Color(.8f, .1f, .2f, 1f);//vermelho
        onMove = false;
        moveSpeed_current = 0;
        state = 1;
        sound_hurt.enabled = true;

        // JumpHit();

        //go_trigger.SetActive(false);
        yield return new WaitForSeconds(sufferingTime);

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);//cor original

        moveSpeed_current = moveSpeed_default;
        state = 0;
        sound_hurt.enabled = false;

        if (currentType != EnemyType.SHOOTERSKULL) {
            onMove = true;
        }

        //go_trigger.SetActive(true);
    }

    IEnumerator WhenInvicicle()//tempo invencivel
    {
        contactDamage.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        contactDamage.SetActive(true);

    }
    public void Hit(float dmg)//levando dano
    {
        UpdateHealth(-dmg);
        //healthBarObject.SetActive(true);
        StartCoroutine(SufferingDamage());
        StartCoroutine(WhenInvicicle());
    }

    public void JumpHit(float punchForceX, float punchForceY) // pulo ao levar dano
    {

        if (direction_x == player.GetComponent<Player>().input_x)
        {
            if (player.GetComponent<Player>().input_x == 1)
            {
                rb.AddForce(Vector2.right * jumpHitForceX_current * punchForceX, ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(jumpHitForceX_current * punchForceX, rb.linearVelocity.y);
            }
            else
            {
                rb.AddForce(Vector2.left * jumpHitForceX_current * punchForceX , ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(jumpHitForceX_current * punchForceX * -1, rb.linearVelocity.y);
            }
        }
        else
        {
            if (player.GetComponent<Player>().input_x == 1)
            {
                rb.AddForce(Vector2.right * jumpHitForceX_current * punchForceX, ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(jumpHitForceX_current * punchForceX, rb.linearVelocity.y);
            }
            else
            {
                rb.AddForce(Vector2.left * jumpHitForceX_current * punchForceX, ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(jumpHitForceX_current * punchForceX * -1, rb.linearVelocity.y);
            }
        }

        rb.AddForce(Vector2.up * jumpHitForceY_current * punchForceY, ForceMode2D.Impulse);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHitForceY_current * punchForceY);

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (state != -1) // vivo
        {
            // tocando na parede
            if (other.gameObject.tag == "Wall" && onMove)
            {
                direction_x *= -1;
                rb.linearVelocity = new Vector2(direction_x * moveSpeed_current, rb.linearVelocity.y);
                // if (GetComponent<SpriteRenderer>().flipX == true)
                // {
                //     GetComponent<SpriteRenderer>().flipX = false;
                // }
                // else
                // {
                //     GetComponent<SpriteRenderer>().flipX = true;
                // }
            }

            // tocando na espada do player
            if (other.gameObject.tag == "Punch" && player.GetComponent<Player>().isAttacking)
            {
                // moveSpeed_current = 0;

                if (player.GetComponent<Player>().handSide)
                {
                    // if (currentType != EnemyType.BAT){
                    //     JumpHit(other.GetComponent<Punch>().jumpForceXCurrent[1], other.GetComponent<Punch>().jumpForceYCurrent[1]);}
                    JumpHit(other.GetComponent<Punch>().jumpForceXCurrent[1], other.GetComponent<Punch>().jumpForceYCurrent[1]);
                    Hit(other.GetComponent<Punch>().damageCurrent[1]);
                }
                else
                {
                    // if (currentType != EnemyType.BAT){
                    //     JumpHit(other.GetComponent<Punch>().jumpForceXCurrent[0], other.GetComponent<Punch>().jumpForceYCurrent[0]);}
                    JumpHit(other.GetComponent<Punch>().jumpForceXCurrent[0], other.GetComponent<Punch>().jumpForceYCurrent[0]);
                    Hit(other.GetComponent<Punch>().damageCurrent[0]);  
                }
                
                StartCoroutine(cam.Shaking(1.8f, 1));
                //Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 10)
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 10)
        {
            isGrounded = false;
        }
    }
}
