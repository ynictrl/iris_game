using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    Player player;
    public bool onDamageContact = true;
    public float damageContact;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        // tocando no player
        if(other.gameObject.tag == "PlayerTrigger" && onDamageContact && !Player.deadPlayer) 
        {
            player.Hit(damageContact);
            //Destroy(gameObject);
        } 
    }
}
