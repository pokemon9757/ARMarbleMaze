using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 0;
    public GameObject player;
    public GameObject SideUp;
    public GameObject SideDown;
    MazeSide up;
    MazeSide down;


    // Start is called before the first frame update
    void Awake()
    {
        currentLevel = SideUp.GetComponent<MazeSpawner>().RandomSeed;
        SideDown.GetComponent<MazeSpawner>().RandomSeed = currentLevel + 1;
    }

    void Start()
    {
        up = SideUp.GetComponent<MazeSide>();
        down = SideDown.GetComponent<MazeSide>();
        up.level = currentLevel;
        down.level = currentLevel + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
