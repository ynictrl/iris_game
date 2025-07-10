using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{

    private Transform playerPos;
    public float speed;
    private Player player;

    public bool mouseActive;
    public bool mouseX;
    public bool mouseY;

    public float number;
    public float trot;


    public Transform spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spawnPos = GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        

        if(Vector2.Distance(transform.position, playerPos.position) > 0) //seguir player
       {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);         
       } 

  

       // CONTROLE MOUSE

       if(!PauseMenu.isPaused){
       Vector3 mousePos = Input.mousePosition;
       mousePos = Camera.main.ScreenToWorldPoint(mousePos); 
       Vector2 direction = new Vector2(mousePos.x - transform.position.x + number, mousePos.y - transform.position.y + number);
       transform.up = direction;
       }

       float mousePositionX = Input.GetAxis("Mouse X");
       float mousePositionY = Input.GetAxis("Mouse Y");

       if(mousePositionX == 0 && mousePositionX == 0){
           mouseActive = false;
       }else{
           mouseActive = true;
       }
       if(mousePositionX > 0){
           mouseX = true;
       }
       if(mousePositionX < 0){
           mouseX = false;
       }
       if(mousePositionY > 0){
           mouseY = true;
       }
       if(mousePositionY < 0){
           mouseY = false;
       }

        //float tmpx = transform.position.x;
    /*
        CONTROLE X E Y

        if(player.lastX)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
        if(player.lastXNeg)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        if(player.lastY)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if(player.lastYNeg)
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
        }
      

    //if(player.IsMouse){
    if(transform.rotation.z > 0){
        player.lastRotX = false;
    }
    if(transform.rotation.z < 0){
        player.lastRotX = true;
            }
        //}*/
    }
}
