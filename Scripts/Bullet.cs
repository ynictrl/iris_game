// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Bullet : MonoBehaviour
// {
//     public float speed;
//     public float timeDestroy;
//     public float damage;

//     private Player player;

//     public bool intangilvel;
//     public bool boomerang;
//     public bool boomerangReturn;
//     public bool roar;
//     public bool _alpha;
//     public bool ghostPower;
//     public bool ravenPower;
//     public bool _power;
    
//     private Transform playerPos;
//     private Animator anim;
//     private AudioSource sound;
//     public Transform spawnBullet;

//     public bool noDestroy; 
//     public bool noSound; 

//     public SpriteRenderer stormSprite;

//     // Start is called before the first frame update
//     void Start()
//     {
//       //timeDestroy = 1.0f;
//       //Destroy(gameObject, timeDestroy); 
//       player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
//       playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
//       anim = GetComponent<Animator>();
//       sound = GetComponent<AudioSource>();
//       //spwblt = GameObject.FindGameObjectWithTag("SpawnBullet").GetComponent<Transform>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//       if(!noSound)
//       {
//       sound.volume = SoundController.volume;
      
        
//         switch (SoundController.volume)
//         {
//             case 0: sound.volume = 0f; break;
//             case 1: sound.volume = 0.1f; break;
//             case 2: sound.volume = 0.2f; break;
//             case 3: sound.volume = 0.3f; break;
//             case 4: sound.volume = 0.4f; break;
//             case 5: sound.volume = 0.5f; break;
//             case 6: sound.volume = 0.6f; break;
//             case 7: sound.volume = 0.7f; break;
//             case 8: sound.volume = 0.8f; break;
//             case 9: sound.volume = 0.9f; break;
//             case 10: sound.volume = 1f; break;
//         }

//       }

//       if(_alpha)
//       {
//         if(player.ghostMode){
//           GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);}
//       }else{
//         if(player.ghostMode){
//           GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);}
//       }

//       if(player.defaultMode && !ghostPower && !ravenPower && !_power){
//         speed = 8;
//         timeDestroy = 0.5f;
//         //damage = 1;


//         anim.SetInteger("transition", 0);
//         intangilvel = false;
//         boomerang = false;
//         //transform.rotation.z = 45;
//       }

//       if(player.bearMode && !roar){
//         speed = 10;
//         timeDestroy = 0.5f;
//         damage = 1.25f;

//         anim.SetInteger("transition", 1);
//         intangilvel = false;
//         boomerang = false;
//       }

//       if(player.ghostMode && !ghostPower){
//         speed = 10;
//         timeDestroy = 0.4f;
//         damage = 0.5f;
//         _alpha = true;

//         anim.SetInteger("transition", 2);
//         if(ghostPower){
//           intangilvel = true;
//         }else{
//           intangilvel = false;
//         }
//         boomerang = false;
//       }else{
//         _alpha = false;
//       }
//       if(player.ravenMode && !ravenPower){
//         if(!noDestroy)
//         {
//         speed = 12;
//         timeDestroy = 0.5f;
//         damage = 0.75f;

//         anim.SetInteger("transition", 3);
//         intangilvel = false;
//         boomerang = false;
//         }
          
//       }
//       if(player.antMode && !_power){
//         speed = 18f;
//         timeDestroy = 0.75f;
//         damage = 1.5f;

//         anim.SetInteger("transition", 4);
//         intangilvel = true;
//         boomerang = true;

//        // if(player.defaultMode)
//         //{
//         //  gameObject.SetActive(false);
//         //}
//       }else{ 
//         //destroyBullet = true;
//       }
      

//      // if(destroyBullet)
//     //  {
//     //        gameObject.SetActive(false);
//             //GetComponent<SpriteRenderer>().enabled = false;
//    //   }


      
      
//       if(boomerang)
//       {
//         StartCoroutine(goBack());
//       }else
//       {
//         if(ghostPower)
//         {
//           transform.rotation = spawnBullet.rotation;
//           if(player.tpPoint)
//           {
//             transform.Translate(Vector2.up * speed * Time.deltaTime);
//             GetComponent<SpriteRenderer>().enabled = true;
//             GetComponent<CircleCollider2D>().enabled = true;
//           }
//           else
//           {
//             transform.position = playerPos.position;
//             GetComponent<SpriteRenderer>().enabled = false;
//             GetComponent<CircleCollider2D>().enabled = false;
//           }

//         }
        

//         if(ravenPower)
//         {
//           if(player.isStorm && player.ravenMode)
//           {
//             //transform.rotation = Quaternion.Euler(0, 0, 0);
//             transform.Translate(Vector2.up * speed * Time.deltaTime);
//             stormSprite.enabled = true;
//             GetComponent<CircleCollider2D>().enabled = true;
//           }
//           else
//           {
//             transform.rotation = spawnBullet.rotation;
//             transform.position = playerPos.position;
//             stormSprite.enabled = false;
//             GetComponent<CircleCollider2D>().enabled = false;
//           }

//         }
        

//         if(!ravenPower && !ghostPower)
//         {
//           transform.Translate(Vector2.up * speed * Time.deltaTime);
//           if(!noDestroy){
//           Destroy(gameObject, timeDestroy);
//           }
//         }
//       }
        
      
//       if(boomerangReturn){
//         if(Vector2.Distance(transform.position, playerPos.position) > 0) //seguir player
//        {
//             transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);         
//        } 
//       }else{
//         if(!ghostPower && !ravenPower){
//             transform.Translate(Vector2.up * speed * Time.deltaTime);
//         }
//       }
//     }

//     IEnumerator goBack()
//     {
//       //boomerangReturn = false;
//       yield return new WaitForSeconds(timeDestroy); 
     
//       boomerangReturn = true;
//       yield return new WaitForSeconds(timeDestroy + 0.4f);
//       Destroy(gameObject, timeDestroy); 

//     }


  

//     void OnTriggerEnter2D(Collider2D other)
//     {
//       if(other.gameObject.tag == "Wall")
//       {
//         if(!intangilvel)
//         {
//           if(!noDestroy){
//           Destroy(gameObject);
//           }
//         }else
//         {
//           if(ghostPower)
//           {
//             player.tpPoint = false;
//             player.slow = false;
//             player.isCry = false;
//           }
//           if(ravenPower)
//           {
//             player.isStorm = false;
//           }
//         }
//       }

//       if(other.gameObject.tag == "Enemy")
//       {
//         Enemy enemy = other.GetComponent<Enemy>();

//       //if(enemy !=null){
//           enemy.DamageEnemy(damage);
//           if(!roar)
//           {
//             player.hitPoint += 1;
//           }
//       //}
//         if(!intangilvel)
//         {
//           if(!noDestroy){
//           Destroy(gameObject);
//           }
//         }
//       }   

//       if(other.gameObject.tag == "Player")
//       {
//         GetComponent<SpriteRenderer>().enabled = false;
//       }
//     }

//     void OnTriggerExit2D(Collider2D other)
//     {
//       if(other.gameObject.tag == "Player")
//       {
//         GetComponent<SpriteRenderer>().enabled = true;
//       }
//     }
// }
