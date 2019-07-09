using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour {
    public ReferenceController rc;

    Town targetTown;
    Town sourceTown;

    Town[] allTowns;

    // direction come from
    Vector3 previousDirection = Vector3.zero;

    void GetAllTowns ()
    {
        allTowns = FindObjectsOfType<Town>();
    }

    void AssignTowns(Town source, Town target)
    {
        targetTown = target;
        sourceTown = source;
    }

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

    void DoPath ()
    {
        // get the overall direction
        Vector3 direction = targetTown.transform.position - sourceTown.transform.position;

        // find the prevailingDirection - the furthest direction (x or y) needed to travel
        Vector3 prevailingDirection = FindPrevailingDirection(direction);

        // check the tile in the prevailing direction

        // if has objects (stone / wood), check in the 'other' direction

        // if has objects (stone / wood), check in the opposite of prevailing direction

        // if has objects (stone / wood), check in the opposite of 'other' direction

        // ERROR - NO PATH AVAILABLE

        // if no objects, make that Floor currentFloor
        // set previousDirection to negative?

    }

    Vector3 FindPrevailingDirection (Vector3 direction)
    {
        Vector3 prevDir = Vector3.zero;

        // find the prevailing direction based on whether it's more 'y' or more 'x'
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            prevDir.x = direction.x;
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            prevDir.y = direction.y;
        }
        else
        {
            // if neither, just pick one randomly
            if (Random.Range(0f,1f) >= 0.5f)
            {
                prevDir.x = direction.x;
            }
            else
            {
                prevDir.y = direction.y;
            }
        }
        return prevDir;
    }


}
