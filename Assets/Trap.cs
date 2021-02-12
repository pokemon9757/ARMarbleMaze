using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{ 
    public GameManager gameManager;
    void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Player"){
            other.collider.transform.position = gameManager.startPos;
            // Some sort of troll maybe
        }
    }
}
