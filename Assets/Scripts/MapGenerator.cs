using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  This script just generates the map and the terrain on it
//  Designed to be run only once at start
//
//  Called by the MapController script

public class MapGenerator : MonoBehaviour {
    public ReferenceController rc;

    // Prefabs for the terrain objects
    public Floor floor;
    public GameObject tree;
    public GameObject stone;
    
    // maps array
    public Floor[,] map;

    private void Start()
    {
        // create map array of Floor objects based upon the height and width defined in MapController
        map = new Floor[rc.mc.width, rc.mc.height];

        // spawn grass in all locationss
        CreateGrass();

        //// add water - TEMP - Add around the border - NOT WORKING!
        //CreateWater();
        //map[2, 3].SetWater();

        // Add trees and rocks - hard-coded currently
        AddTrees();
        AddStone();

        // Centralise and scale the camera
        Camera.main.transform.position = new Vector3(rc.mc.width / 2 - 0.5f, rc.mc.height / 2 - 0.5f, -10f);
        Camera.main.orthographicSize = Mathf.Min(rc.mc.width, rc.mc.height) / 2;
    }

    void CreateGrass ()
    {
        // creates a grass (Floor) object for each location in map array
        for (int x = 0; x < rc.mc.width; x++)
        {
            for (int y = 0; y < rc.mc.height; y++)
            {
                map[x, y] = Instantiate(floor, new Vector3(x, y, 0.75f), Quaternion.identity);
                map[x, y].transform.SetParent(this.transform);
                map[x, y].transform.name = "Floor_" + x + "_" + y;
            }
        }
    }

    void AddTrees()
    {
        PlaceTree(2, 2);
        PlaceTree(2, 3);
        PlaceTree(3, 2);
        PlaceTree(7, 6);
        PlaceTree(4, 3);
        PlaceTree(19, 2);
        PlaceTree(12, 16);
        PlaceTree(19, 3);
        PlaceTree(18, 2);
        PlaceTree(12, 12);
        PlaceTree(8, 13);
        PlaceTree(7, 12);
    }

    void PlaceTree(int x, int y)
    {
        GameObject go = Instantiate(tree, map[x, y].transform.position, Quaternion.identity);
        go.transform.SetParent(this.transform);
        go.transform.name = "Tree_" + x + "_" + y;
        map[x, y].resource = MapController.Resource.Tree;
    }

    void AddStone()
    {
        PlaceStone(12, 6);
        PlaceStone(2, 13);
        PlaceStone(3, 12);
        PlaceStone(7, 16);
        PlaceStone(10, 13);
        PlaceStone(10, 12);
    }

    void PlaceStone(int x, int y)
    {
        GameObject go = Instantiate(stone, map[x, y].transform.position, Quaternion.identity);
        go.transform.SetParent(this.transform);
        go.transform.name = "Stone_" + x + "_" + y;
        map[x, y].resource = MapController.Resource.Stone;
    }

    void CreateWater()
    {
        for (int x = 0; x < rc.mc.width; x++)
        {
            int y = 0;
            map[x, y].SetWater();
            y = rc.mc.height - 1;
            map[x, y].SetWater();
        }
        for (int y = 0; y < rc.mc.height; y++)
        {
            int x = 0;
            map[x, y].SetWater();
            x = rc.mc.width - 1;
            map[x, y].SetWater();
        }
    }
}
