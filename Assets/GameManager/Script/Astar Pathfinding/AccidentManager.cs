using UnityEngine;
using System.Collections.Generic;

public class AccidentManager : MonoBehaviour
{
    public GameObject[] accidents;
    public GameObject noti;
    public Camera cam;
    int randomAccident, randomPosition;
    public bool spawnAllowed;
    GridPath grid;
    private List<Node> walkableNodes = new List<Node>();

    void Awake()
    {
        grid = GetComponent<GridPath>();
    }

    void Start()
    {
        noti.SetActive(false);
        spawnAllowed = true;
        FindWalkableNode();
        InvokeRepeating ("SpawnAccident", 10f, 20f);
    }

    void FindWalkableNode()
    {
        foreach (Node node in grid.grid)
        {
            if (node.walkable)
            {
                walkableNodes.Add(node);
            }
        }
        Debug.Log($"số node đi đc: {walkableNodes.Count}");
    }

    void SpawnAccident()
    {
        if (spawnAllowed)
        {
            randomAccident = Random.Range(0, accidents.Length);
            randomPosition = Random.Range(0, walkableNodes.Count);
            Instantiate(accidents [randomAccident], walkableNodes [randomPosition].worldPosition, Quaternion.identity);
            noti.SetActive(true);
        }
    }
    public void PointToAccident()
    {
        cam.gameObject.transform.Translate(walkableNodes [randomPosition].worldPosition);
        noti.SetActive(false);
    }
}
