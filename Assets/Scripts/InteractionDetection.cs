using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

//TODO - Move and work off of player object
public class InteractionDetection : MonoBehaviour
{

    public BoxCollider2D[] boxColliders;
    // Start is called before the first frame update\
    public Camera[] cameras;
    public GameObject[] player;
    private int currentCameraIndex = 0;
    private GameState gameState = null;
    // private Timer timer = null;
    void Start()
    {
        gameState = Game.GetGameState();
        boxColliders = GameObject.FindObjectsOfType<BoxCollider2D>();
        //Turn all cameras off, except the first default one
		for (int i=1; i<cameras.Length; i++) 
		{
			cameras[i].gameObject.SetActive(false);
		}

        //If any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }

        if (boxColliders == null || boxColliders.Length == 0)
        {
            Debug.LogError("Doorway requires at least one BoxCollider2D component.");
        }

        if (player == null || player.Length == 0)
        {
            Debug.LogError("Doorway requires at least one GameObject with the tag 'Player'.");
        }
        
        // timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        // if (timer == null)
        // {
        //     Debug.LogError("Timer component not found on timerTextObject.");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject p in player)
        {
            if (p == null || p.GetComponent<Movement>() == null || !p.GetComponent<Movement>().allowMovement)
            {
                continue; // Skip this player if it's null, doesn't have a Movement component, or movement is not allowed
            }

            foreach (BoxCollider2D boxCollider in boxColliders)
            {
                GameObject gameObject = boxCollider.gameObject;
                if (IsPlayerTouchingCollider(p.GetComponent<Movement>(), boxCollider))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
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
                            case "Mail Chute":
                                interactWithChute();
                                break;
                            case "Test":
                                Debug.Log("Interacting with Test Object");
                                interactToSave();
                                break;
                            default:
                                Debug.LogWarning("Player is touching a collider with an unhandled tag: " + gameObject.tag);
                                break;
                        }
                    }
                }
                else if (gameObject.tag == "DialogueEmitter")
                {
                    TextEmitter textEmitter = gameObject.GetComponent<TextEmitter>();
                    if (textEmitter != null && textEmitter.IsActivated())
                    {
                        textEmitter.Deactivate();
                        textEmitter.HideTextPanel(); // Hide the panel when the player moves away from the DialogueEmitter
                        gameState.HideConfirmationPanel(); // Hide the confirmation panel as well, in case it was triggered by the DialogueEmitter
                    }
                }
            }

        }

    }

    private void interactToSave()
    {
        //TODO - Save State is broken when trying to save game.
        gameState.SaveGame(gameState.offset);
    }

    private void interactWithChute()
    {
        Player playerInfo = gameState.GetPlayer();
        if (playerInfo != null && playerInfo.hasMail)
        {
            playerInfo.mailCount -= 1;
        }
    }

    private void interactWithMailContainer(Mailbox mailbox)
    {
        int mailCount = mailbox.GetMailCount();
        Player player = gameState.GetPlayer();
        if (player != null)
        {
            if (mailCount > 0)
            {
                //Character character = gameState.characters[0]; // Get the first character in the GameState's characters array (you may want to implement a more robust way to determine which character's mail is being collected)
                //Debug.Log("Collecting mail for character: " + character.getName() + " with address: " + character.getAddress());
                player.CollectLetters(mailbox.GetLetters()); // Collect all letters from the mailbox
                mailbox.ClearMail(); // Clear the mailbox after collecting mail
            } else
            {
                Debug.Log("No mail to collect.");
                return;
            }
        }
        else
        {
            Debug.LogError("PlayerInfo component not found on Player.");
        }
    }

    private void interactWithBed()
    {
        if (gameState != null)
        {
            gameState.ShowConfirmationPanel("Go to Bed?");
            // gameState.AdvanceDay();
        }
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
        int newindex = currentCameraIndex + 1;
        if (newindex >= cameras.Length) { newindex = 0; }
        changeCamera(newindex);
    }

    private void changeCamera(int newindex)
    {
        cameras[newindex].gameObject.SetActive(true);
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = newindex;
    }

    public bool IsPlayerTouchingCollider(Movement player = null, BoxCollider2D boxCollider = null)
    {
        if (player != null && player.allowMovement && boxCollider != null)
        {
            BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
            if (playerCollider != null)
            {
                if (boxCollider.IsTouching(playerCollider))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
