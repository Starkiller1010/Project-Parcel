using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Dialogue : TextEmitter
{
    private static string DIALOGUE_DIRECTORY = "Dialogue";

    private List<string> gameAreas = new List<string>() { "Home", "Street", "Workroom" };

    public override string GetText()
    {
        base.directory = DIALOGUE_DIRECTORY;
        return base.GetText();
    }

    protected override string PathBuilder()
    {
        StringBuilder pathBuilder = new StringBuilder();
        GameState gameState = Game.GET_GAME_STATE();
        if (transform.parent == null || !gameAreas.Contains(transform.parent.name))
        {
            pathBuilder.Append("Default");
        }
        else
        {
            pathBuilder.Append(transform.parent.name + "/");
            pathBuilder.Append(gameState.GetTimeTracker().GetDay() + "/");
            pathBuilder.Append(this.name);
        }
        Debug.Log("Constructed path: " + pathBuilder.ToString());
        return pathBuilder.ToString();
    }
}
