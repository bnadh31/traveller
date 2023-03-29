using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CoinPickUp : MonoBehaviour
{
    [SerializeField]AudioClip coinPickupSFX;
    [SerializeField]int coinAmount=1; 
    bool wasCollected=false;
    
    void Start()
    {
        int iScore = PlayerPrefs.GetInt("MyLastScore",0);
        PlayerPrefs.SetInt("MyLastScore", iScore);
        PlayerPrefs.Save();
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected)
        {   
            wasCollected=true;
            FindObjectOfType<GameSession>().AddToScore(coinAmount);    
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            Destroy(gameObject);
            
        }
    }
}
