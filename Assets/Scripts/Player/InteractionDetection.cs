using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionDetection
{

    public bool isInteracting = false;
    public bool processingInteraction = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // This method is intentionally left empty to ensure that the Rigidbody2D's collision detection is active.
        // The actual interaction logic is handled in the OnCollisionStay2D and OnCollisionExit2D methods.
        Debug.Log("Player has collided with an object: " + collision.gameObject.name);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        isInteracting = false;
        processingInteraction = false;
        if (gameObject.tag == "DialogueEmitter")
        {
            TextEmitter textEmitter = gameObject.GetComponent<TextEmitter>();
            if (textEmitter != null)
            {
                // textEmitter.Deactivate();
                // textEmitter.HideTextPanel(); // Hide the panel when the player moves away from the DialogueEmitter
                HUD.HideDialoguePanel(); // Hide the dialogue panel when the player moves away from the DialogueEmitter
                HUD.HideConfirmationPanel(); // Hide the confirmation panel as well, in case it was triggered by the DialogueEmitter
            }
        }
    }
    
    public void CollisionCheck(GameObject gameObject)
    {
        if (isInteracting && !processingInteraction)
        {
            processingInteraction = true;
            Debug.Log("Player is touching a collider and has pressed E.");
            switch (gameObject.tag)
            {
                case "TransitionObject":
                    interactWithDoorway();
                    break;
                case "DialogueEmitter":
                    interactWithDialogueEmitter(gameObject.GetComponent<Dialogue>());
                    break;
                case "Bed":
                    interactWithBed();
                    break;
                case "Mail Container":
                    interactWithMailContainer(gameObject.GetComponent<Mailbox>());
                    break;
                // case "Mail Chute":
                //     interactWithChute();
                //     break;
                case "Test":
                    Debug.Log("Interacting with Test Object");
                    interactToSave();
                    break;
                default:
                    Debug.LogWarning("Player is touching a collider with an unhandled tag: " + gameObject.tag);
                    break;
            }
            processingInteraction = false;
            isInteracting = false;
        }
    }

    private void interactToSave()
    {
        FileManager.SaveGameState(Game.GET_GAME_STATE());
    }

    private void interactWithMailContainer(Mailbox mailbox)
    {
        int mailCount = mailbox.GetMailCount();
        if (mailCount > 0)
        {
            HUD.ShowConfirmationPanel("You collected " + mailCount + " pieces of mail.", null, null);
        }
        else
        {
            HUD.ShowConfirmationPanel("Your mailbox is empty.", null, null);
        }
        mailbox.GetLetters();
    }

    private void interactWithBed()
    {
        Game.GET_PLAYER().GetControls().ToggleMovementState(); // Freeze player movement when interacting with a DialogueEmitter
        HUD.ShowConfirmationPanel("Do you want to sleep and end the day?", OnBedConfirmation, OnBedReject);

    }

    private void OnBedConfirmation()
    {
        Debug.Log("Player has chosen to sleep and end the day.");
        Game.GET_GAME_STATE().EndDay();
         // Reload the current scene to reflect the new day
        // ToggleFreezePlayer(); // Unfreeze player movement after confirming the action
        // HUD.HideConfirmationPanel(); // Hide the confirmation panel after the player makes a choice
    }

    private void OnBedReject()
    {
        Debug.Log("Player has chosen not to sleep and end the day.");
        ToggleFreezePlayer(); // Unfreeze player movement after rejecting the action
        HUD.HideConfirmationPanel(); // Hide the confirmation panel after the player makes a choice
    }
    
    private void interactWithDialogueEmitter(Dialogue textEmitter = null)
    {
        Debug.Log("Player has interacted with a DialogueEmitter.");
        HUD.ShowDialoguePanel(textEmitter.GetText());
    }

    private void interactWithDoorway()
    {
        Debug.Log("Interacted with doorway, switching cameras.");
        Director.SwitchCamera(Director.GetNextCameraIndex());
    }

    private void ToggleFreezePlayer()
    {
        Game.GET_PLAYER().GetControls().ToggleMovementState();
    }
}
