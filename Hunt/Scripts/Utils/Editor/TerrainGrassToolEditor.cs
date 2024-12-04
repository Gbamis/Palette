using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainGrassTool))]
public class TerrainGrassToolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainGrassTool myScript = (TerrainGrassTool)target;
        if (GUILayout.Button("Remap Details"))
        {
            myScript.Adjust();
        }
    }
}
