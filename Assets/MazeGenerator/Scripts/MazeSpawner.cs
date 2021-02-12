using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public Transform Ground = null;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject Pillar = null;
    public GameObject Trap = null;
    public int NumberOfTraps = 0;
    public int Rows = 5;
    public int Columns = 5;
    public float scale = 1.0f;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public bool AddGaps = true;
    public GameObject GoalPrefab = null;
    private bool goalExisted = false;


    private BasicMazeGenerator mMazeGenerator = null;

    void Start()
    {
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        CellWidth = Floor.transform.localScale.x * scale;
        CellHeight = Floor.transform.localScale.z * scale;

        Floor.transform.localScale *= scale;
        Wall.transform.localScale *= scale;
        GoalPrefab.transform.localScale *= scale;
        Trap.transform.localScale *= scale;
        Floor.transform.position = ScalePos(Floor.transform.position, Floor.transform.localScale, scale);
        Wall.transform.position = ScalePos(Wall.transform.position, Wall.transform.localScale, scale);
        GoalPrefab.transform.position = ScalePos(GoalPrefab.transform.position, GoalPrefab.transform.localScale, scale);
        Trap.transform.position = ScalePos(Trap.transform.position, Trap.transform.localScale, scale);
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                var quaterRotation = Floor.transform.rotation;
                Vector3 r = quaterRotation.eulerAngles;
                float x = column * (CellWidth + (AddGaps ? .2f * scale : 0));
                float z = row * (CellHeight + (AddGaps ? .2f * scale : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;

                tmp = Instantiate(Floor, (new Vector3(x, 0, z) + Floor.transform.position), Quaternion.Euler(r.x, 0, 0)) as GameObject;
                tmp.transform.parent = transform;
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, (new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position), Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, (new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position), Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, (new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position), Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, (new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position), Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = transform;
                }
                if (cell.IsGoal && GoalPrefab != null && !goalExisted)
                {
                    quaterRotation = GoalPrefab.transform.rotation;
                    r = quaterRotation.eulerAngles;
                    tmp = Instantiate(GoalPrefab, (GoalPrefab.transform.position + new Vector3(x, 0, z)), Quaternion.Euler(r.x, r.y, r.z)) as GameObject;
                    tmp.transform.parent = transform;
                    goalExisted = true;
                    GoalPrefab.SetActive(false);
                }

                // Debug.Log("wall size" + tmp.transform.localScale);
            }
        }
        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));
                    GameObject tmp = Instantiate(Pillar, (new Vector3(x - CellWidth / 2, Pillar.transform.position.y, z - CellHeight / 2)) * scale, Quaternion.identity) as GameObject;
                    tmp.transform.parent = transform;
                    tmp.transform.localScale *= scale;
                }
            }
            Pillar.SetActive(false);
        }
        if (Trap != null)
        {
            int noOfTraps = 0;
            List<int> trapLocations = new List<int>();
            var quaterRotation = Trap.transform.rotation;
            Vector3 r = quaterRotation.eulerAngles;
            while (noOfTraps < NumberOfTraps)
            {
                int rand;
                do
                {
                    rand = Random.Range(1, Columns);
                }
                while (trapLocations.Contains(rand));
                trapLocations.Add(rand);
                float x = rand * CellWidth;
                float z = rand * CellHeight;

                GameObject tmp = Instantiate(Trap, (Trap.transform.position + new Vector3(x, 0, z)), Quaternion.Euler(r.x, r.y, r.z)) as GameObject;
                // tmp.transform.parent = transform;
                noOfTraps++;
                tmp.transform.localScale *= scale;
            }
            Trap.SetActive(false);
        }
        Floor.SetActive(false);
        Wall.SetActive(false);
    }

    private Vector3 ScalePos(Vector3 pos, Vector3 size, float scale)
    {
        var x = pos.x * (pos.x > 0 ? scale : -scale * -1);
        var y = pos.y * (pos.y > 0 ? scale : -scale * -1);
        var z = pos.z * (pos.z > 0 ? scale : -scale * -1);
        var newX = Ground.position.x - Ground.localScale.x * scale / 5 * 2;
        var newZ = Ground.position.z - Ground.localScale.z * scale / 5 * 2;
        var newY = Ground.position.y + Ground.localScale.y * scale / 2;
        
        return new Vector3(newX, pos.y > 0 ? newY : -newY, newZ);
    }
}