using System;
using System.Text;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class TextEmitter : MonoBehaviour
{
    protected TextAsset textFile = null;
    [UxmlAttribute, CreateProperty]
    protected String text = null;
    protected string directory = "";
    // public GameObject textPanel = null;
    // protected string PathBuilder(bool isDialogue = false)
    // {
    //     StringBuilder pathBuilder = new StringBuilder();
    //     if (isDialogue)
    //     {
    //         pathBuilder.Append(transform.parent.name + "/");
    //     }
    //     pathBuilder.Append(Game.GET_GAME_STATE().GetTimeTracker().GetDay() + "/");
    //     pathBuilder.Append(this.name);
    //     Debug.Log("Constructed path: " + pathBuilder.ToString());
    //     return pathBuilder.ToString();
    // }

    abstract protected string PathBuilder();
    
    public virtual string GetText()
    {
        if (text != null)
        {
            return text;
        } else if (textFile == null)
        {
            textFile = FileManager.LoadTextFile(directory, PathBuilder());
            if (textFile == null)
            {
                Debug.LogError("Failed to load text for: " + this.name + " at path: " + directory + "/" + PathBuilder() + ".txt");
                return null;
            }
            Debug.Log("Dialogue text loaded successfully: " + textFile.text);
            SetText(textFile.text);
        }
        return text;
    }

    protected void SetText(string newText)
    {
        this.text = newText;
    }

    // public void DisplayText()
    // {
    //     GetText();
    //     if (text != null)
    //     {
    //         UpdateTextPanel(text);
    //     }
    //     else
    //     {
    //         Debug.LogError("Text is null. Cannot display text.");
    //     }
    // }

    // private void UpdateTextPanel(string newText = "")
    // {
    //     if (textPanel != null)
    //     {
    //         textPanel.SetActive(true);
    //         textPanel.GetComponentInChildren<Text>().text = newText;
    //     }
    //     else
    //     {
    //         Debug.LogError("Panel GameObject with name 'Dialogue Box' not found in the scene.");
    //     }
    // }

    // public void HideTextPanel()
    // {
    //     if (textPanel != null)
    //     {
    //         textPanel.SetActive(false);
    //     }
    //     else
    //     {
    //         Debug.LogError("Panel GameObject with name 'Dialogue Box' not found in the scene.");
    //     }
    // }
}
