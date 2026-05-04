using UnityEngine;

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
            if (textEmitter != null && textEmitter.IsActivated())
            {
                textEmitter.Deactivate();
                textEmitter.HideTextPanel(); // Hide the panel when the player moves away from the DialogueEmitter
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
                    interactWithDialogueEmitter(gameObject.GetComponent<TextEmitter>());
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
        GameState gameState = Game.GET_GAME_STATE();
        FileManager.SaveGameState(gameState);
    }

    private void interactWithMailContainer(Mailbox mailbox)
    {
        int mailCount = mailbox.GetMailCount();
        if (mailCount > 0)
        {
            HUD.ShowConfirmationPanel("You collected " + mailCount + " pieces of mail.");
        }
        else
        {
            HUD.ShowConfirmationPanel("Your mailbox is empty.");
        }
        mailbox.GetLetters();
    }

    private void interactWithBed()
    {
        HUD.ShowConfirmationPanel("Do you want to sleep and end the day?");
    }
    
    private void interactWithDialogueEmitter(TextEmitter textEmitter = null)
    {
        Debug.Log("Player has interacted with a DialogueEmitter.");
        // Additional logic for interacting with a DialogueEmitter can be added here
        textEmitter.Activate();
        textEmitter.DisplayText();
    }

    private void interactWithDoorway()
    {
        Debug.Log("Interacted with doorway, switching cameras.");
        Director.SwitchCamera(Director.GetNextCameraIndex());
    }
}
