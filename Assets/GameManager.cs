using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 0;

    public Position currentPosition;
    public GameObject player;
    public GameObject SideUp;
    public GameObject SideDown;
    public bool foundExit { get; set; }
    public enum Position { Top, Bottom };
    MazeSide up;
    MazeSide down;
    // Start is called before the first frame update
    void Awake()
    {
        currentLevel = SideUp.GetComponent<MazeSpawner>().RandomSeed;
        currentPosition = Position.Top;
        SideDown.GetComponent<MazeSpawner>().RandomSeed = currentLevel + 1;
        foundExit = false;
    }

    void Start()
    {
        up = SideUp.GetComponent<MazeSide>();
        down = SideDown.GetComponent<MazeSide>();
        up.level = currentLevel;
        down.level = currentLevel + 1;
    }


    public void FinishLevel()
    {
        foundExit = false;
        Vector3 startPoint = Vector3.zero;
        switch (currentPosition)
        {
            case Position.Top:
                currentPosition = Position.Bottom;
                startPoint = new Vector3(0, -1, 0);
                break;
            case Position.Bottom:
                currentPosition = Position.Top;
                startPoint = new Vector3(0, 1, 0);
                break;
        }
        // Debug.Log("start point " + startPoint);
        player.transform.position = startPoint;
    }
}
