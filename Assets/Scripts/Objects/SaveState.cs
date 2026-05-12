using System;
using System.Text;

[Serializable]
public class SaveState
{
    public string version;
    public string created_at;
    public string updated_at;
    public string playtime;
    public int dayCount;
    public int offset;
    public int[] characterAddresses;
    public string flags;

    public SaveState(string version = "1.0",
                    string created_at = "",
                    string updated_at = "",
                    string playtime = "0:00:00",
                    int dayCount = 0,
                    int offset = 0,
                    int[] characterAddresses = null,
                    string flags = null)
    {
        this.version = version;
        this.created_at = created_at;
        this.updated_at = updated_at;
        this.playtime = playtime;
        this.dayCount = dayCount;
        this.offset = offset;
        this.characterAddresses = characterAddresses;
        this.flags = flags;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("version: " + this.version + "\n");
        stringBuilder.Append("created_at: " + this.created_at + "\n");
        stringBuilder.Append("updated_at: " + this.updated_at + "\n");
        stringBuilder.Append("playtime: " + this.playtime + "\n");
        stringBuilder.Append("dayCount: " + this.dayCount + "\n");
        stringBuilder.Append("offset: " + this.offset + "\n");
        stringBuilder.Append("flags: " + this.flags + "\n");
        return stringBuilder.ToString();
    }
}