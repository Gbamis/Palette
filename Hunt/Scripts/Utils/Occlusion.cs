using UnityEngine;

public class Occlusion : MonoBehaviour
{
    public GameObject child;
    private void OnBecameVisible() => Render(true);
    private void OnBecameInvisible() => Render(false);

    private void Render(bool value)
    {
        if (child == null)
        {
            transform.GetChild(0).gameObject.SetActive(value);
        }
        else
        {
            child.SetActive(value);
        }
    }
}
