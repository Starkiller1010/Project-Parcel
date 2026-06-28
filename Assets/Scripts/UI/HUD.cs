//TODO - Create Class responsible for all HUD UI objects
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUD : MonoBehaviour
{
    public static VisualElement ui;
    static VisualElement interactionBox = null;
    static VisualElement confirmationPanel = null;
    public static Confirmation confirmationCommands = null;
    public static string dialogueText = "";
    public static string dialogueAuthor = "Me:";
    public static string dayText = "Day ";

    void OnEnable()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        ui.Bind(new SerializedObject(this));
    }

    void Start()
    {
        interactionBox = GetElement("DialogueBox");
        confirmationCommands = GetComponent<Confirmation>();
        SetDayText(Game.GET_TIME_TRACKER().GetDay().ToString());
        HideAllUI();
    }

    private void HideAllUI()
    {
        HideDialoguePanel();
        HideConfirmationPanel();
    }

    private static VisualElement GetElement(string elementName)
    {
        return ui.Q<VisualElement>(elementName);
    }

    public void SetDayText(string text)
    {
        // dayText = "Day " + text;
        Label dayLabel = ui.Q<Label>("DayCounter");
        if (dayLabel != null)
        {
            dayLabel.text = dayText + text;
        }
        else
        {
            Debug.LogError("Label with name 'DayCounter' not found in the scene.");
        }
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

    public static void ShowConfirmationPanel(string promptText, Action onConfirm, Action onReject)
    {
        GUI.LoadModal();
        confirmationPanel = GetElement("ConfirmationPanel");
        SetConfirmationPromptText(promptText);
        SetConfirmationActions(onConfirm, onReject);
    }

    public static void HideConfirmationPanel()
    {
        if (confirmationPanel != null)
        {
            // confirmationPanel.AddToClassList("hide");
            confirmationPanel.RemoveFromHierarchy();
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

    private static void SetConfirmationPromptText(string promptText)
    {
        if (confirmationPanel != null)
        {
            Label promptLabel = confirmationPanel.Q<Label>("ConfirmationHeader");
            promptLabel.text = promptText;
        }
        else
        {
            Debug.LogError("Label with name 'ConfirmationHeader' not found in the scene.");
        }
    }

    private static void SetConfirmationActions(Action onConfirm, Action onReject)
    {
        if (confirmationCommands != null)
        {
            confirmationCommands.SetActions(onConfirm, onReject);
        }
        else
        {
            Debug.LogError("Confirmation component not found in the scene.");
        }
    }
}