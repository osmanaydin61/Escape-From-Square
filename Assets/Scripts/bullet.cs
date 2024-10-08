using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   [SerializeField] float bulletSpeed=5f;
   Rigidbody2D myRigidbody;
   PlayerMovement player;
   float xSpeed;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

   
    void Update()
    {
        myRigidbody.velocity = new Vector2 (xSpeed,0f);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
     }
     void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
     }
}
