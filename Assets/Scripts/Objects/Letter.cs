using System;
using System.Collections.Generic;

[Serializable]
public class Letter
{
    private int uid;
    private int fromIndex;
    private int toIndex;
    private string content;
    private List<int> requirements;

    public Letter(int toIndex, string content, int uuid = 50, int fromIndex = 0, int[] requirements = null)
    {
        SetUID(uuid);
        SetFromIndex(fromIndex);
        SetToIndex(toIndex);
        SetContent(content);
        SetRequirements(requirements);
    }

    public int GetUID()
    {
        return uid;
    }

    public void SetUID(int newUid)
    {
        uid = newUid;
    }

    public int GetToIndex()
    {
        return toIndex;
    }

    public void SetToIndex(int newAddress)
    {
        toIndex = newAddress;
    }

    public string GetContent()
    {
        return content;
    }

    public void SetContent(string content)
    {
        this.content = content;
    }

    public int GetFromIndex()
    {
        return fromIndex;
    }

    public void SetFromIndex(int newIndex)
    {
        fromIndex = newIndex;
    }

    public List<int> GetRequirements()
    {
        return requirements;
    }

    public void SetRequirements(int[] requirements)
    {
        if (requirements != null)
        {
            this.requirements = new List<int>(requirements);
        }
    }
}
