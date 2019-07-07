<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Controls the main game loop

public class GameController : MonoBehaviour {

    public ReferenceController rc;

    // A state to control the 'phase' of the game in the Update loop and initialise to Default
    public enum GameState { SpawnUnit, Default, NextTurn }
    // assign the public gameState to Default
    // MAKE GAMESTATE NON-PUBLIC?
    public GameState gameState = GameState.Default;

    // Prefab for spawning units - TODO: MOVE TO A UNIT CONTROLLER?
    public Unit unitPrefab;

    public int turn = 0;

    private void Start()
    {
        // Centralise and scale the camera
        SetupCamera();
    }

    // main loop depending on game states
    private void Update()
    {
        switch (gameState)
        {
            case GameState.SpawnUnit :
                if (Input.GetMouseButtonDown(0))
                {
                    // On left-click round to the nearest Floor location (integer value)
                    Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spawnPos.x = Mathf.Round(spawnPos.x);
                    spawnPos.y = Mathf.Round(spawnPos.y);
                    spawnPos.z = 0;
                    // find the floor object at the desired spawn location
                    Floor floor = rc.mc.GetFloorAt(spawnPos);

                    // test to see if floor exists
                    if (floor != null)
                    {
                        // if exists, check if there's a unit already there and if not, spawn here
                        if (floor.hasUnit == false)
                            floor.AddUnit(Instantiate(unitPrefab, spawnPos, Quaternion.identity));
                    }
                }
                // un-select SpawnUnit mode on right click
                if (Input.GetMouseButtonDown(1))
                {
                    gameState = GameState.Default;
                }
                break;
            case GameState.NextTurn :
                // Gather relevant resources
                Unit[] units = FindObjectsOfType<Unit>();
                foreach (Unit u in units)
                {
                    u.GatherResources();
                }
                turn++;
                gameState = GameState.Default;
                break;
            case GameState.Default :
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameState = GameState.NextTurn;
                }
                break;
        }
    }

    // function to allow other scripts to change game state
    public void SetGameState (GameState state)
    {
        gameState = state;
    }

    void SetupCamera()
    {
        // Centralise and scale the camera
        Camera.main.transform.position = new Vector3(rc.mc.width / 2 - 0.5f, rc.mc.height / 2 - 0.5f, -10f);
        Camera.main.orthographicSize = Mathf.Min(rc.mc.width, rc.mc.height) / 2;
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Controls the main game loop

public class GameController : MonoBehaviour {

    public ReferenceController rc;

    // A state to control the 'phase' of the game in the Update loop and initialise to Default
    public enum GameState { SpawnUnit, Default, NextTurn }
    // assign the public gameState to Default
    // MAKE GAMESTATE NON-PUBLIC?
    public GameState gameState = GameState.Default;

    // Prefab for spawning units - TODO: MOVE TO A UNIT CONTROLLER?
    public Unit unitPrefab;

    public int turn = 0;

    private void Start()
    {
        // Centralise and scale the camera
        SetupCamera();
    }

    // main loop depending on game states
    private void Update()
    {
        switch (gameState)
        {
            case GameState.SpawnUnit :
                if (Input.GetMouseButtonDown(0))
                {
                    // On left-click round to the nearest Floor location (integer value)
                    Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    spawnPos.x = Mathf.Round(spawnPos.x);
                    spawnPos.y = Mathf.Round(spawnPos.y);
                    spawnPos.z = 0;
                    // find the floor object at the desired spawn location
                    Floor floor = rc.mc.GetFloorAt(spawnPos);

                    // test to see if floor exists
                    if (floor != null)
                    {
                        // if exists, check if there's a unit already there and if not, spawn here
                        if (floor.hasUnit == false)
                            floor.AddUnit(Instantiate(unitPrefab, spawnPos, Quaternion.identity));
                    }
                }
                // un-select SpawnUnit mode on right click
                if (Input.GetMouseButtonDown(1))
                {
                    gameState = GameState.Default;
                }
                break;
            case GameState.NextTurn :
                // Gather relevant resources
                Unit[] units = FindObjectsOfType<Unit>();
                foreach (Unit u in units)
                {
                    u.GatherResources();
                }
                turn++;
                gameState = GameState.Default;
                break;
            case GameState.Default :
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gameState = GameState.NextTurn;
                }
                break;
        }
    }

    // function to allow other scripts to change game state
    public void SetGameState (GameState state)
    {
        gameState = state;
    }

    void SetupCamera()
    {
        // Centralise and scale the camera
        Camera.main.transform.position = new Vector3(rc.mc.width / 2 - 0.5f, rc.mc.height / 2 - 0.5f, -10f);
        Camera.main.orthographicSize = Mathf.Min(rc.mc.width, rc.mc.height) / 2;
    }
}
>>>>>>> refs/remotes/origin/master
