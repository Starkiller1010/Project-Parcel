using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public abstract class TextEmitter : MonoBehaviour
{
    public TextAsset textFile = null;
    bool activated = false;
    String text = null;
    public GameObject textPanel = null;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        textPanel = GameObject.FindGameObjectWithTag("DialogueBox");
    }

    protected string PathBuilder(bool isDialogue = false)
    {
        StringBuilder pathBuilder = new StringBuilder();
        if (isDialogue)
        {
            pathBuilder.Append(transform.parent.name + "/");
        }
        pathBuilder.Append(Game.GetGameState().GetCurrentDay() + "/");
        pathBuilder.Append(this.name);
        Debug.Log("Constructed path: " + pathBuilder.ToString());
        return pathBuilder.ToString();
    }
    
    abstract protected void GetText();

    public void Activate()
    {
        activated = true;
    }

    public void Deactivate()
    {
        activated = false;
    }

    public bool IsActivated()
    {
        return activated;
    }

    protected void SetText(string newText)
    {
        this.text = newText;
    }

    public void DisplayText()
    {
        GetText();
        if (text != null)
        {
            UpdateTextPanel(text);
        }
        else
        {
            Debug.LogError("Text is null. Cannot display text.");
        }
    }

    private void UpdateTextPanel(string newText = "")
    {
        if (textPanel != null)
        {
            textPanel.SetActive(true);
            textPanel.GetComponentInChildren<Text>().text = newText;
        }
        else
        {
            Debug.LogError("Panel GameObject with name 'Dialogue Box' not found in the scene.");
        }
    }

    public void HideTextPanel()
    {
        if (textPanel != null)
        {
            textPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel GameObject with name 'Dialogue Box' not found in the scene.");
        }
    }
}
