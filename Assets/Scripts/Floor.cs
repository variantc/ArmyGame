﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for floor object - currently just 'grass'

public class Floor : MonoBehaviour {
    ReferenceController rc;

    // MapGenerator map array location
    public int xPos;
    public int yPos;

    // Material and default colour of the floor (green (grass))
    //Material material;
    Color colourGrass = Color.green;
    Color colourWater = Color.blue;

    // flag for is this floor object currently has a unit present
    public bool hasUnit = false;
    // unit present in this floor object
    Unit unit;
    
    // resource present here, if any - default to None - assigned in MapGenerator
    public MapController.Resource resource = MapController.Resource.None;

    private void Start()
    {
        // get the ReferenceController
        rc = FindObjectOfType<ReferenceController>();

        // set the position
        xPos = Mathf.RoundToInt(this.transform.position.x);
        yPos = Mathf.RoundToInt(this.transform.position.y);

        // store the colour of the Floor object in colour and set the colour to green
        //material = this.GetComponent<Renderer>().material;
        //material.color = colourGrass;
        this.GetComponent<Renderer>().material.color = colourGrass;
    }

    public void SetWater()
    {
        //material.color = colourWater;
        //this.GetComponent<Renderer>().material = material;
        this.GetComponent<Renderer>().material.color = colourWater;
    }

    // public function to add the unit to Floor object
    // DO WE WANT TO MAKE FUNCTION RETURN BOOL TO INDICATE SUCCESS/FAILURE?
    public void AddUnit (Unit unit)
    {
        // check if we don't already have a unit here
        if (hasUnit == false)
        {
            this.unit = unit;
            hasUnit = true;
        }
    }

    // public function to remove unit from this Floor object
    // DO WE WANT TO MAKE FUNCTION RETURN BOOL TO INDICATE SUCCESS/FAILURE?
    public void RemoveUnit()
    {
        // check if we have a unit here to remove
        if (hasUnit == true)
        {
            this.unit = null;
            hasUnit = false;
        }
    }

    // NOT CURRENTLY USING
    // Colour the floor to indicate it's selected
    public void SelectFloor()
    {
        this.GetComponent<Renderer>().material.color = Color.green;
    }
}