using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TerrainGrassData{
    public string name;
    public float min_width;
    public float max_width;
    public float min_height;
    public float max_height;
    public float density;
    public Color health_color;
    public Color dry_color;
    

}
[CreateAssetMenu(fileName = "Terrain Settings", menuName = "Games/Hunt/Util/terrain gass data")]
public class TerrainSettings : ScriptableObject
{
    public List<TerrainGrassData> terrainGrassDatas;
}
