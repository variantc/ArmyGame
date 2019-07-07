using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public ReferenceController rc;

    public Text text_GameState;
    public Text text_Resources;

    private void Start()
    {
        UpdateGameStateText();
    }

    // Function to be called when Unit button clicked - currently toggles into and out of SpawnUnit gameState
    public void Button_Unit()
    {
        // toggle spawn unit gameState
        if (rc.gc.gameState != GameController.GameState.SpawnUnit)
        {
            // set the game state to SpawnUnit state in GameController
            rc.gc.SetGameState(GameController.GameState.SpawnUnit);

            // set the button colour to green
            GameObject.FindGameObjectWithTag("UnitButton").GetComponent<Image>().color = Color.green;
        }
        else
        {
            // set the game state to Defaul state in GameController
            rc.gc.SetGameState(GameController.GameState.Default);

            // set the button colour to white
            GameObject.FindGameObjectWithTag("UnitButton").GetComponent<Image>().color = Color.white;
        }
        // update the gamestate display
        UpdateGameStateText();
    }

    void UpdateGameStateText()
    {
        // Change the text to display the current gamestate
        text_GameState.text = "GameState:\n" + rc.gc.gameState.ToString();
    }

    public void DisplaySurroundingResources(MapController.Resource[] surroundingResources)
    {
        string resourceString = "Resources:\n";
        foreach (MapController.Resource r in surroundingResources)
        {
            // ignore None resources
            if (r == MapController.Resource.None)
                continue;

            resourceString += r.ToString() + "\n";
        }

        // update the Resources text box
        text_Resources.text = resourceString;

    }
}