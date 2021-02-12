using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // _gameManager = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Player")
        {
            gameManager.foundExit = true;
            gameManager.FinishLevel();
        }
    } 
}
