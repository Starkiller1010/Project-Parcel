using System.Collections.Generic;
using UnityEngine;
public class Controls
{
    List<KeyCode> movementKeys = new List<KeyCode>() { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    List<KeyCode> optionKeys = new List<KeyCode>() { KeyCode.A, KeyCode.D };
    List<KeyCode> interactionKeys = new List<KeyCode>() { KeyCode.E };
    List<Movement> movementScripts = new List<Movement>();

    private void AddMovementScript(Movement movementScript)
    {
        movementScripts.Add(movementScript);
    }

    public Controls()
    {
        InitializeControls();
    }

    public void InitializeControls()
    {
        movementScripts.Clear();
        GameObject[] characterObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject character in characterObjects)
        {
            Movement movementScript = character.GetComponent<Movement>();
            if (movementScript != null)
            {
                AddMovementScript(movementScript);
            }
        }
    }

    public bool ChangeOption()
    {
        foreach (KeyCode key in optionKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    public void ToggleMovementState()
    {
        foreach (Movement movementScript in movementScripts)
        {
            if (movementScript != null && movementScript.gameObject.GetComponent<Renderer>().isVisible)
            {
                if (!movementScript.allowMovement)
                {
                    movementScript.unfreezeMovement();
                }
                else
                {
                    movementScript.freezeMovement();
                }
            }
        }
    }
}