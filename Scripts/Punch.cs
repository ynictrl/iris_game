using UnityEngine;

public class Punch : MonoBehaviour
{
    GameObject player;
    public Animator anim;
    public float[] damageCurrent;
    public float[] speedurrent;
    public float[] jumpForceXCurrent;
    public float[] jumpForceYCurrent;
    float rotZ = 0;
    public enum PunchStates
    {
        IDLE, LEFT, RIGHT
    }
    public PunchStates currentState;

    int xInputHash = Animator.StringToHash("xInput");


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ControlAnim();
        PunchTransform();
    }

    void ControlAnim()
    {
        anim.SetFloat(xInputHash, player.GetComponent<Player>().input_x);

        switch (currentState)
        {
            case PunchStates.IDLE:
                anim.Play("IDLE");
                break;
            case PunchStates.LEFT:
                anim.Play("LEFT");
                // anim.speed = 1;
                // player.GetComponent<Player>().attackTime = 0.6f; // 0.3f
                // player.GetComponent<Player>().attackTimeInterval = 0.7f; // 0.4f

                break;
            case PunchStates.RIGHT:
                anim.Play("RIGHT");
                // anim.speed = 2;
                // player.GetComponent<Player>().attackTime = .3f;
                // player.GetComponent<Player>().attackTimeInterval = .4f;
                break;
        }
    }

    void PunchTransform()
    {
        // low punch
        if (player.GetComponent<Player>().lowered){
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 0.7f);
        }
        else{
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        }

        // Punho diagonal
        if (!player.GetComponent<Player>().isAttacking)
        {
            if (Input.GetKey(KeyCode.W)){
                if (player.GetComponent<Player>().input_x == 1)
                {
                    rotZ = 45f; //67.5f
                }
                else
                {
                    rotZ = -45f;
                }
            }

            if (Input.GetKey(KeyCode.S) && player.GetComponent<Player>().isJumping){
                if (player.GetComponent<Player>().input_x == 1)
                {
                    rotZ = -45f;
                }
                else
                {
                    rotZ = 45f;
                }
            }

            if ((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) || player.GetComponent<Player>().lowered){
                rotZ = 0;
            }
        }

        Quaternion rotacao = Quaternion.Euler(0, 0, rotZ);
        transform.rotation = rotacao;
        
        // Layer player
        switch (currentState)
        {
            case PunchStates.LEFT:
                if (player.GetComponent<Player>().input_x == -1){
                    GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
                else{
                    GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            break;
            case PunchStates.RIGHT:
                if (player.GetComponent<Player>().input_x == +1){
                    GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
                else{
                    GetComponent<SpriteRenderer>().sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
            break;
        }
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {

    //     // tocando na espada do inimigo
    //     if (other.gameObject.tag == "Enemy" && player.GetComponent<Player>().isAttacking)
    //     {
    //         // rb.linearVelocity = new Vector2(1, rb.linearVelocity.y);
    //         other.GetComponent<Enemy>().Hit(damageCurrent);
    //         // other.GetComponent<Enemy>().StartCoroutine(cam.Shaking(2,1));
    //         // other.GetComponent<Enemy>().JumpHit();

    //         //Destroy(gameObject);
    //     }

    // }
}
