using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("coliddd");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("You Won!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
    }
}
