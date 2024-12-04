using UnityEngine;
using System;

[Serializable]
public struct ColorNode
{
    public float distance;
    public bool isMatched;
    public Color expectedResult;
    public Color playerInput;
    public float visible;

    public void CheckSimilarity(Color baseColor, Color result)
    {

        distance = Mathf.Sqrt(
                  MathF.Pow(baseColor.r - result.r, 2) +
                  Mathf.Pow(baseColor.g - result.g, 2) +
                  Mathf.Pow(baseColor.b - result.b, 2)
                 );
        isMatched = distance <= visible;
    }

}

[CreateAssetMenu(fileName = "ColorTesting", menuName = "Scriptable Objects/ColorTesting")]
public class ColorTesting : ScriptableObject
{
    public float visibilityDistance;
    public Color parentA;
    public Color parenB;

    public ColorNode fullNode;
    public ColorNode leftNode;


    [Header("Generations")]
    public bool full;
    public bool leftHalf;

    private void OnValidate()
    {
        if (full) { Full(); }
        if (leftHalf) { Left(); }

        fullNode.CheckSimilarity(fullNode.expectedResult, fullNode.playerInput);
        leftNode.CheckSimilarity(leftNode.expectedResult, leftNode.playerInput);
    }

    private void Full()
    {
        fullNode.expectedResult.r = (parentA.r + parenB.r) * .5f;
        fullNode.expectedResult.g = (parentA.g + parenB.g) * .5f;
        fullNode.expectedResult.b = (parentA.b + parenB.b) * .5f;
        fullNode.expectedResult.a = 1;
    }

    private void Left()
    {
        leftNode.expectedResult.r = (parentA.r * .25f) + parenB.r;
        leftNode.expectedResult.g = (parentA.g * .25f) + parenB.g;
        leftNode.expectedResult.b = (parentA.b * .25f) + parenB.b;
        leftNode.expectedResult.a = 1;

    }
}
