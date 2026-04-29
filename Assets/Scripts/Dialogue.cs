using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : TextEmitter
{
    protected override void GetText()
    {
        textFile = FileManager.LoadDialogueFile(PathBuilder(true));
        if (textFile != null)
        {
            Debug.Log("Dialogue text loaded successfully: " + textFile.text);
            SetText(textFile.text);
        }
        else
        {
            Debug.LogError("Failed to load dialogue text for: " + this.name);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
