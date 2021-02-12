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
    public Vector3 startPos { get; set; }
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
        tmp.y += player.transform.localScale.y;
        startPos = tmp;
        player.transform.localPosition = tmp;
    }


    public void FinishLevel()
    {
        if (foundExit)
        {
            foundExit = false;
            Vector3 temp = Vector3.zero;
            switch (currentPosition)
            {
                case Position.Top:
                    currentPosition = Position.Bottom;
                    temp = SideDown.GetComponent<MazeSpawner>().startLocation;
                    temp.y -= player.transform.localScale.y;

                    startPos = temp;
                    currentLevel += 1;
                    break;

                case Position.Bottom:
                    currentPosition = Position.Top;
                    temp = SideUp.GetComponent<MazeSpawner>().startLocation;
                    temp.y += player.transform.localScale.y;
                    startPos = temp;
                    break;
            }
        }
        // Debug.Log("start point " + startPoint);
        player.transform.localPosition = startPos;
    }
}
