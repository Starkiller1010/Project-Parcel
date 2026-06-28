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
        // Debug.Log("Player has collided with an object: " + collision.gameObject.name);
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
        else if (gameObject.tag == "Workstation")
        {
            Workstation workstation = gameObject.GetComponent<Workstation>();
            workstation.DisableWorkTable();
        }
        else if (gameObject.tag == "Mail Container")
        {
            Mailbox mailbox = gameObject.GetComponent<Mailbox>();
            mailbox.CloseMailboxMenu();
        }
    }

    public void CollisionCheck(GameObject gameObject)
    {
        if (isInteracting && !processingInteraction)
        {
            processingInteraction = true;
            Debug.Log(string.Format("Interacting with {0}", gameObject.name));
            switch (gameObject.tag)
            {
                case "TransitionObject":
                    interactWithDoorway(gameObject.GetComponent<SpriteRenderer>());
                    break;
                case "DialogueEmitter":
                    interactWithDialogueEmitter(gameObject.GetComponent<Dialogue>());
                    break;
                case "Bed":
                    interactWithBed(gameObject.GetComponent<Bed>());
                    break;
                case "Mail Container":
                    interactWithMailContainer(gameObject.GetComponent<Mailbox>());
                    break;
                case "Workstation":
                    interactWithWorkTable(gameObject.GetComponent<Workstation>());
                    break;
                case "Test":
                    interactWithTest(gameObject);
                    break;
                default:
                    Debug.LogWarning("Player is touching a collider with an unhandled tag: " + gameObject.tag);
                    break;
            }
            processingInteraction = false;
            isInteracting = false;
        }
    }

    private void interactWithWorkTable(Workstation workstation)
    {
        workstation.EnableWorkTable();
    }
    
    private void interactWithTest(GameObject gameObject)
    {
        Debug.Log("Interacting with Test Object");
        // Example interaction logic for the test object
        gameObject.GetComponent<InsurgentSelector>().Open();
    }

    private void interactToLoad()
    {
        string fileName = FileManager.GetSavedGameStates()[0];
        GameState gameState = FileManager.LoadGameState("/" + fileName);
        Game.GET_GAME_STATE().SetState(gameState);
    }

    private void interactWithMailContainer(Mailbox mailbox)
    {
        int mailCount = mailbox.GetMailCount();
        if (mailCount > 0)
        {
            Player.AddLetters(mailbox.GetLetters());
            mailbox.ClearMail();
        }
        else
        {
            Debug.Log("Player attempted to collect mail from an empty mailbox.");
        }
        if (!mailbox.isUIOpen)
            mailbox.OpenMailboxMenu();
    }

    private void interactWithBed(Bed bed)
    {
        bed.Interact();
    }

    
    private void interactWithDialogueEmitter(Dialogue textEmitter = null)
    {
        Debug.Log("Player has interacted with a DialogueEmitter.");
        HUD.ShowDialoguePanel(textEmitter.GetText());
    }

    private void interactWithDoorway(SpriteRenderer sprite)
    {
        if (sprite.isVisible)
        {
            Debug.Log("Interacted with doorway, switching cameras.");
            Director.SwitchCamera(Director.GetNextCameraIndex());
        }
    }
}
