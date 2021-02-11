using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered exit level " + _gameManager.currentLevel);
        if(other.tag == "Player") {
            _gameManager.foundExit = true;
        }
    }
}
