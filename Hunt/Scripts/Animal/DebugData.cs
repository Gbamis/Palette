using UnityEngine;
using System.Text;

public class DebugData : MonoBehaviour
{
    private StringBuilder console = new StringBuilder();
    [TextArea(10, 20)] public string debug;

    public void Print(string value)
    {
        console.Append(value + "\n");
        debug = console.ToString();
    }
}
