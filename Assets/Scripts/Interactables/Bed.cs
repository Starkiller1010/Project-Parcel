using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bed : MonoBehaviour
{
    public VisualTreeAsset ui = null;
    private GUI gUI = null;
    private bool opened = false;

    // private void OpenUI()
    // {
    //     if (ui)
    //     {
    //         gUI = new GUI();
    //     }
    // }

    public void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape) && opened)
        {
            OnBedReject();
        }
    }

    public void Interact()
    {
        Game.FreezePlayer(true);
        opened = true;
        HUD.ShowConfirmationPanel("Do you want to sleep and end the day?", OnBedConfirmation, OnBedReject);
    }
    
    private void OnBedConfirmation()
    {
        FileManager.SaveGameState(Game.GET_GAME_STATE());
        Game.GET_GAME_STATE().EndDay();
        opened = false;
    }

    private void OnBedReject()
    {
        Debug.Log("Player has chosen not to sleep and end the day.");
        Game.FreezePlayer(false); // Unfreeze player movement after rejecting the action
        HUD.HideConfirmationPanel(); // Hide the confirmation panel after the player makes a choice
        opened = false;
    }
}
