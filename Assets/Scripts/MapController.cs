using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MapController - Initially calls the MapGenerator and then controls the operation
// of the map during runtime

public class MapController : MonoBehaviour {
    public ReferenceController rc;

    // resource types
    public enum Resource { None, Tree, Stone }

    // map dimensions - TODO: - make private with accessors?
    public int width;
    public int height;

    // return the floor object at a particular position if exists - test map limits
    public Floor GetFloorAt(Vector3 pos)
    {
        // test out of bounds
        if (CheckInBounds((int)pos.x, (int)pos.y) == false)
            return null;
        
        // if here, then within bounds, therefore, return map here (rounding to int)
        return rc.mg.map[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)];
    }

    //public bool CheckInBounds(float fx, float fy)
    public bool CheckInBounds(int x, int y)
    {
        if (x < width || x >= 0 || y < height || y >= 0)
            return true;
        else
            return false;
    }
}
