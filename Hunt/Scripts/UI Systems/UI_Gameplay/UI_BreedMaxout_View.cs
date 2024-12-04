using UnityEngine;
using UnityEngine.UI;


namespace HT
{
    public class UI_BreedMaxout_View : UI_Views
    {
        [SerializeField] private Image progress;

        public void SetLevel(int current, int max)
        {
            Debug.LogFormat("total: {0} and max {1}", current, max);
            float a = current;
            float b = max;
            float value = a / b;
            Debug.Log(value);
            progress.fillAmount = value;
        }

    }
}
