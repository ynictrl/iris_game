using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Transform playerPos;
    private Player player;
    public float speed;
    public float timeDestroy;
    public int damage;

    //public int num; 

    // public Enemy enemy;
    // //public GameObject enemyObj;
    // public Transform enemyPos;
    // private AudioSource sound;

    // private Animator anim;

    // public string nameEnemy;

    // //public Transform position;

    // public bool intangilvel;
    // public bool boomerang;
    // public bool boomerangReturn;
    // public bool isReflect;
    
    // //public bool powerGhost;
    // //public GameObject returnBullet;

    // public bool noDestroy;

    // public bool isSlow;
    // public bool isDark;
    // public bool isSound;

    // Start is called before the first frame update
    void Start()
    {
      //ANT BOOMERANG
      //enemy = GameObject.Find(nameEnemy).GetComponent<Enemy>();
      //enemyPos = GameObject.Find(nameEnemy).GetComponent<Transform>();
      playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
      // anim = GetComponent<Animator>();
      // sound = GetComponent<AudioSource>();
    }

  // Update is called once per frame
  void FixedUpdate()
  {
    transform.Translate(Vector2.up * speed * Time.deltaTime);
    Destroy(gameObject, timeDestroy);

      // if(isSound)
    // {
    // sound.volume = SoundController.volume;


    //   switch (SoundController.volume)
    //   {
    //       case 0: sound.volume = 0f; break;
    //       case 1: sound.volume = 0.1f; break;
    //       case 2: sound.volume = 0.2f; break;
    //       case 3: sound.volume = 0.3f; break;
    //       case 4: sound.volume = 0.4f; break;
    //       case 5: sound.volume = 0.5f; break;
    //       case 6: sound.volume = 0.6f; break;
    //       case 7: sound.volume = 0.7f; break;
    //       case 8: sound.volume = 0.8f; break;
    //       case 9: sound.volume = 0.9f; break;
    //       case 10: sound.volume = 1f; break;
    //   }
    // }
    //transform.rotation = trot;
    //target = GameObject.Find("Player").GetComponent<Transform>();
    //if(!staticPower){
    //transform.Translate(Vector2.up * speed * Time.deltaTime);
    //}else{
    //  transform.Translate( speed * Time.deltaTime);
    //}
    // if(boomerang)
    // {
    //   enemy = GameObject.Find(nameEnemy).GetComponent<Enemy>();
    //   enemyPos = GameObject.Find(nameEnemy).GetComponent<Transform>();
    //   StartCoroutine(goBack());
    // }else{

    //     transform.Translate(Vector2.up * speed * Time.deltaTime); 
    //     Destroy(gameObject, timeDestroy);

    // }

    /*if(enemy.ghost)
    {
        if(enemy.enemyIsCry)
        {
            if(Vector2.Distance(transform.position, playerPos.position) > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
            } 

            //GetComponent<SpriteRenderer>().enabled = true;

        }else{
            transform.position = enemyPos.position;
            //GetComponent<SpriteRenderer>().enabled = false;
        }
    }*/


    // if(boomerangReturn)
    // {
    //   if(Vector2.Distance(transform.position, enemyPos.position) > 0) //seguir enemy
    //   {
    //     transform.position = Vector2.MoveTowards(transform.position, enemyPos.position, speed * Time.deltaTime);
    //     //transform.Translate(Vector2.up * -speed * Time.deltaTime);         
    //   } 
    // }else{
    //   transform.Translate(Vector2.up * speed * Time.deltaTime);
    // }

    // if(boomerang && !enemy.enemyIsAlive)
    // {
    //   Destroy(gameObject);
    // }


  }
    

    // IEnumerator goBack()
    // {
      
    //   //transform.Translate(Vector2.up * speed * Time.deltaTime);
    //   yield return new WaitForSeconds(timeDestroy); 
     
    //   boomerangReturn = true;
    //   yield return new WaitForSeconds(timeDestroy /*+ 0.4f*/);
    //   Destroy(gameObject, timeDestroy);

    // }

    // IEnumerator SlowSprit()
    // {    
    //     //player.inSlow = true;
    //     player.slow = true;
    //     player.fireRate = 1.6f;
    //     yield return new WaitForSeconds (1.5f); 
    //     player.fireRate = 1.3f;
    //     //player.inSlow = false;
    //     player.slow = false;
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.tag == "Wall")
      {
        Destroy(gameObject);
      }
      if(other.gameObject.tag == "Player")
      {
        Destroy(gameObject, 0.01f);
      }
      
    }

    void OnTriggerExit2D(Collider2D other)
    {
      if(other.gameObject.tag == "Enemy")
      {
        GetComponent<SpriteRenderer>().enabled = true;
      }
    }

}
