using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class Names
{
    public List<string> first { get; set; }
    public List<string> last { get; set; }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("First Names: ");
        builder.Append(string.Join(",", first));
        builder.Append("\nLast Names: ");
        builder.Append(string.Join(",", last));
        return builder.ToString();
    }
}