using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField]float bulletSpeed=20f;
    character_movement player;
    float xSpeed;
     
    // Start is called before the first frame update
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
        player=FindObjectOfType<character_movement>();
        xSpeed=player.transform.localScale.x*bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity=new Vector2(xSpeed,0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy")
        {
            Destroy(other.gameObject);
        }    
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
