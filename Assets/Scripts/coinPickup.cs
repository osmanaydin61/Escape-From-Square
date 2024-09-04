using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickups;
    [SerializeField] int pointForCoinPickup =100;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player"&& !wasCollected)
        {
            wasCollected = true;
            FindAnyObjectByType<gameSession>().AddtoScore(pointForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickups,Camera.main.transform.position);
           
            Destroy(gameObject);
        }
    }
}
 