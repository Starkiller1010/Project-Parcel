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
            textFile = FileUtils.LoadTextFile(PathBuilder());
            if (textFile == null)
            {
                Debug.LogError("Failed to load text for: " + this.name + " at path: " + PathBuilder() + ".txt");
                return null;
            }
            Debug.Log("Text loaded successfully: " + textFile.text);
            SetText(textFile.text);
        }
        return text;
    }

    protected void SetText(string newText)
    {
        this.text = newText;
    }
}
