using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tiles : MonoBehaviour
{
    //Common variables
    public bool occupied = false;
    public bool selected = false;
    public bool current = false;
    public bool target = false;


    // Breadth first Search (BFS)
    public List<Tiles> adjacentTiles = new List<Tiles>();
    public bool visited = false;
    public Tiles parent = null;
    public int distance = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(current)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
       else if(selected)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if(target)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }


    // Reseting the default values
    public void Reset()
    {
        occupied = false;
        selected = false;
        current = false;
        target = false;


        // Breadth first Search (BFS)
        adjacentTiles.Clear();
        visited = false;
        parent = null;
        distance = 0;
    }


    // Finding adjacent tiles
    public void Neighbours()
    {
        Reset();

        
    }

    public void CheckTiles(Vector2 direction)
    {

        

    }




}

