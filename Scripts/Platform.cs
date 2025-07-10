using UnityEngine;

public class Platform : MonoBehaviour
{
    GameObject player;
    public float yPlus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Transform>().position.y > GetComponent<Transform>().position.y + yPlus)
        {
            // GetComponent<Collider2D>().enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        else
        {
            // GetComponent<Collider2D>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("TransparentFX");
        }
    }
}
