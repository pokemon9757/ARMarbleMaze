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
        var tmp = SideUp.GetComponent<MazeSpawner>().startLocation;
        tmp.y += player.transform.localScale.y * SideUp.GetComponent<MazeSpawner>().scale;
        player.transform.position = tmp;
    
    }


    public void FinishLevel()
    {
        foundExit = false;
        Vector3 startPoint = Vector3.zero;
        Vector3 temp = Vector3.zero;
        switch (currentPosition)
        {
            case Position.Top:
                currentPosition = Position.Bottom;
                temp = SideDown.GetComponent<MazeSpawner>().startLocation;
                temp.y -= player.transform.localScale.y * SideUp.GetComponent<MazeSpawner>().scale;
                // temp.y = -temp.y;
                startPoint = temp;
                break;

            case Position.Bottom:
                currentPosition = Position.Top;
                temp = SideUp.GetComponent<MazeSpawner>().startLocation;
                temp.y += player.transform.localScale.y * SideUp.GetComponent<MazeSpawner>().scale;
                startPoint = temp;
                break;
        }
        // Debug.Log("start point " + startPoint);
        player.transform.position = startPoint;
    }
}
