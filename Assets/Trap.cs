using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Player"){
            other.collider.gameObject.SetActive(false);
        }
    }
}
