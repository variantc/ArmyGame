using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    public ReferenceController rc;

    // speed for how quickly this object moves
    float walkSpeed = 1f;

    // the Floor object that this unit currently occupies
    Floor currentFloor;

    bool selected = false;
    Color colourSelected = Color.red;
    Color colourUnselected = Color.white;

    Renderer renderer;

    // counters for resources
    public int num_Wood;
    public int num_Stone;

    // array to store surrounding resources
    public MapController.Resource[] surroundingResources;

    // Use this for initialization
    void Start () {
        rc = FindObjectOfType<ReferenceController>();
        renderer = GetComponent<Renderer>();
        // need to register with the floor tile we spawn on
        RegisterWithFloor();

        // set up array of surrounding resource - initialise only (9 squares)
        surroundingResources = new MapController.Resource[9];
        // Check surrounding resources
        FindSurroundingResources();
        // Display surrounding resources through UIController
        rc.UIc.DisplaySurroundingResources(surroundingResources);
    }
    
    void FixedUpdate () {
        // TEMP - NEED TO FIX AND STOP OUT OF RANGE REFERENCING
        if (this.transform.position.y >= rc.mc.height)
        {
            Destroy(this.gameObject);
            UnRegisterWithFloor();
            return;
        }

        // if leaves floor, register with new floor tile and unregister with old
        if (rc.mc.GetFloorAt(this.transform.position) != currentFloor)
        {
            UnRegisterWithFloor();
            RegisterWithFloor();
        }
    }

    void FindSurroundingResources()
    {
        int x = currentFloor.xPos;
        int y = currentFloor.yPos;

        surroundingResources[0] = CheckResourceAt(x - 1, y - 1);
        surroundingResources[1] = CheckResourceAt(x - 1, y);
        surroundingResources[2] = CheckResourceAt(x - 1, y + 1);
        surroundingResources[3] = CheckResourceAt(x, y - 1);
        surroundingResources[4] = CheckResourceAt(x, y);
        surroundingResources[5] = CheckResourceAt(x, y + 1);
        surroundingResources[6] = CheckResourceAt(x + 1, y - 1);
        surroundingResources[7] = CheckResourceAt(x + 1, y);
        surroundingResources[8] = CheckResourceAt(x + 1, y + 1);
    }

    MapController.Resource CheckResourceAt(int x, int y)
    {
        // check resource is in bounds and if so, return the resource at the floor location
        // otherwise return None
        if (rc.mc.CheckInBounds(x, y) == true)
            return rc.mg.map[x, y].resource;
        else
            return MapController.Resource.None;
    }

    void RegisterWithFloor()
    {
        // TO FIX OUT OF BOUNDS
        if (rc.mc.GetFloorAt(this.transform.position) != null)
        {
            currentFloor = rc.mc.GetFloorAt(this.transform.position);
            currentFloor.AddUnit(this);
        }
    }

    void UnRegisterWithFloor()
    {
        if (currentFloor != null)
            currentFloor.RemoveUnit();
    }

    public void SelectUnit()
    {
        selected = true;
        renderer.material.color = colourSelected;
    }
    public void UselectUnit()
    {
        selected = false;
        renderer.material.color = colourUnselected;
    }

    // loop through the surrounding resources and increment the resource counters accordingly
    // called in GameController
    public void GatherResources()
    {
        foreach (MapController.Resource r in surroundingResources)
        {
            switch (r)
            {
                case MapController.Resource.Tree:
                    num_Wood++;
                    break;
                case MapController.Resource.Stone:
                    num_Stone++;
                    break;
            }
        }
    }
}