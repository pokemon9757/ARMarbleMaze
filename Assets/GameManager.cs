using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 0;
    public GameObject player;
    public MazeSpawner SideUp;
    public MazeSpawner SideDown;


    // Start is called before the first frame update
    void Awake()
    {
        currentLevel = SideUp.RandomSeed;
        SideDown.RandomSeed = currentLevel + 1;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
