//TODO - Create Class responsible for all HUD UI objects
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static Confirmation confirmationPanel = null;

        public static void ShowConfirmationPanel(string promptText)
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.gameObject.SetActive(true);
            confirmationPanel.SetPromptText(promptText);
        }
        else
        {
            Debug.LogError("Panel GameObject with name 'Confirmation Box' not found in the scene.");
        }
    }

    public static void HideConfirmationPanel()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel GameObject with name 'Confirmation Box' not found in the scene.");
        }
    }
}