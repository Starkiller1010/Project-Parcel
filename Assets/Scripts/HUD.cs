//TODO - Create Class responsible for all HUD UI objects
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUD : MonoBehaviour
{
    public VisualElement ui;
    static VisualElement interactionBox = null;
    public static Confirmation confirmationPanel = null;
    public static string dialogueText = "";

    void OnEnable()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        ui.Bind(new SerializedObject(this));
    }

    void Start()
    {
        interactionBox = ui.Q<VisualElement>("DialogueBox");
        HideAllUI();
    }

    private void HideAllUI()
    {
        HideDialoguePanel();
        HideConfirmationPanel();
    }

    public void SetInteractionText(string text)
    {
        if (interactionBox != null)
        {
            interactionBox.Q<TextElement>("DialogueText").text = text;
        }
        else
        {
            Debug.LogError("TextElement with name 'InteractionText' not found in the scene.");
        }
    }

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

    public static void ShowDialoguePanel(string content, string author = "Me:")
    {
        if (interactionBox == null)
        {
            Debug.LogError("TextElement with name 'DialogueBox' not found in the scene.");
            return;
        }
        SetDialogueText(content, author);
        interactionBox.RemoveFromClassList("hide");
    }

    public static void HideDialoguePanel()
    {
        if (interactionBox == null)
        {
            Debug.LogError("TextElement with name 'DialogueBox' not found in the scene.");
            return;
        }

        interactionBox.AddToClassList("hide");
    }

    private static void SetDialogueText(string context, string author)
    {
        if (interactionBox != null)
        {
            TextField dialogueText = interactionBox.Q<TextField>("DialogueText");
            dialogueText.value = context;
            dialogueText.label = author;
        }
        else
        {
            Debug.LogError("TextElement with name 'DialogueText' not found in the scene.");
        }
    }
}