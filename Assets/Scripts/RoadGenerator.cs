using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {
    public ReferenceController rc;

    public Town targetTown;
    public Town sourceTown;

    Town[] allTowns;

    // direction come from
    Vector3 previousDirection = Vector3.zero;

    Floor currentFloor;
    Floor nextFloor;

    public void DoRoads ()
    {
        {
            GetAllTowns();
            AssignRandomTowns();

            // get the overall direction
            Vector3 direction = targetTown.transform.position - sourceTown.transform.position;

            // find the prevailingDirection - the furthest direction (x or y) needed to travel
            Vector3[] dirs = FindPrevailingDirections(direction);

            Vector3 prevailingDirection = dirs[0];
            Vector3 otherDirection = dirs[1];

            currentFloor = rc.mc.GetFloorAt(sourceTown.transform.position);

            nextFloor = FindNextFloor(currentFloor, prevailingDirection, otherDirection);

            
            nextFloor.IncreaseRoadValue();

            Debug.Log(currentFloor + " " + nextFloor);
        }
    }

    // Get all Town objects and
    void GetAllTowns ()
    {
        allTowns = FindObjectsOfType<Town>();
    }

    // NOT CURRENTLY USED - Assign the source and target towns by Town objects
    void AssignTowns(Town source, Town target)
    {
        targetTown = target;
        sourceTown = source;
    }

    // randomly pick source and target towns
    void AssignRandomTowns()
    {
        if (allTowns.Length == 0)
        {
            GetAllTowns();
        }

        if (allTowns.Length <= 1)
        {
            Debug.LogError("Not enough towns to do roads");
            return;
        }

        sourceTown = allTowns[Random.Range(0, allTowns.Length)];
        do
        {
            targetTown = allTowns[Random.Range(0, allTowns.Length)];
        } while (sourceTown == targetTown);
    }
    
    Floor FindNextFloor (Floor source, Vector3 prevailingDirection, Vector3 otherDirection)
    {
        Floor next;

        // check the tile in the prevailing direction
        if (CheckNextFloor(prevailingDirection) != null)
        {
            next = CheckNextFloor(prevailingDirection);
            previousDirection = -prevailingDirection;
        }
        // if has objects (stone / wood), check in the 'other' direction
        else if (CheckNextFloor(otherDirection) != null)
        {
            next = CheckNextFloor(otherDirection);
            previousDirection = -prevailingDirection;
        }
        // if has objects (stone / wood), check in the opposite of prevailing direction
        else if (CheckNextFloor(-prevailingDirection) != null)
        {
            next = CheckNextFloor(-prevailingDirection);
            previousDirection = prevailingDirection;
        }
        // if has objects (stone / wood), check in the opposite of 'other' direction
        else if (CheckNextFloor(-otherDirection) != null)
        {
            next = CheckNextFloor(-otherDirection);
            previousDirection = otherDirection;
        }
        // ERROR - NO PATH AVAILABLE
        else
        {
            Debug.LogError("No path available");
            next = null;
        }

        return next;
    }

    Vector3[] FindPrevailingDirections (Vector3 direction)
    {
        Vector3[] prevDirs = new Vector3[2];

        Vector3 prevDir1 = Vector3.zero;
        Vector3 prevDir2 = Vector3.zero;

        // find the prevailing direction based on whether it's more 'y' or more 'x'
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            prevDir1.x = direction.x;
            prevDir2.y = direction.y;
}
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            prevDir1.y = direction.y;
            prevDir2.x = direction.x;
        }
        else
        {
            // if neither, just pick one randomly
            if (Random.Range(0f,1f) >= 0.5f)
            {
                prevDir1.x = direction.x;
                prevDir2.y = direction.y;
            }
            else
            {
                prevDir1.y = direction.y;
                prevDir2.x = direction.x;
            }
        }
        Vector3[] returnDirs = new Vector3[2];
        returnDirs[0] = prevDir1.normalized;
        returnDirs[1] = prevDir2.normalized;

        Debug.Log(returnDirs[0]);

        return returnDirs;
    }

    Floor CheckNextFloor (Vector3 checkDir)
    {
        Floor f = rc.mc.GetFloorAt(currentFloor.transform.position + checkDir);

        if (f != null)
        {
            if (f.resource == MapController.Resource.None)
            {
                return f;
            }
            else
            {
                Debug.LogError("Resource " + f.resource + " at " + this.transform.position);
                return null;
            }
        }
        else
        {
            Debug.LogError("Can't get Floor at: " + this.transform.position);
            return null;
        }
    }
}
