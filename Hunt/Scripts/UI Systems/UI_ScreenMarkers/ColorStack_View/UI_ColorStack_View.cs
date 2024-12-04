using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Zenject;
using System.Linq;

namespace HT
{
    public enum COLOR_DIR { LEFT = -1, RIGHT = 1 };

    public class UI_ColorStack_View : UI_Views
    {
        private Stack<Color> m_colors;
        private List<Color> sceneColors = new();
        private float visibleDistance;
        private Action reduceCallback;


        [SerializeField] private RectTransform content;
        [SerializeField] private COLOR_DIR layout_direction;
        [SerializeField] private float content_spacing;
        [SerializeField] private UI_ColorItem colorItem;
        [SerializeField] private Button skipBtn;

        public Color current_color;

        private void OnEnable() => skipBtn.onClick.AddListener(() => SkipColor());
        private void OnDisable() => skipBtn.onClick.RemoveAllListeners();


        public async void CreateColorItems(List<Color> b_colors)
        {
            List<Color> colors = b_colors.OrderBy(x => UnityEngine.Random.value).ToList();

            m_colors = new Stack<Color>();

            foreach (Color color in colors)
            {
                UI_ColorItem clone = Instantiate(colorItem, content.position, Quaternion.identity, content);
                clone.transform.SetSiblingIndex(0);
                clone.SetColor(color);
                clone.gameObject.SetActive(true);
            }

            for (int i = colors.Count - 1; i > -1; i--)
            {
                m_colors.Push(colors[i]);
            }
            current_color = m_colors.Pop();
            await Arrange();
        }

        private async UniTask Arrange()
        {
            Vector2 startPos = content.position;
            float lastSpace = 0;

            for (int i = content.childCount - 1; i > -1; i--)
            {
                Vector2 pos = startPos;
                pos.x += lastSpace * (int)layout_direction;
                RectTransform rect = content.GetChild(i).GetComponent<RectTransform>();
                await MoveTo(rect, pos, 0.04f);
                lastSpace += content_spacing;
            }
        }

        private async UniTask MoveTo(RectTransform rect, Vector2 to, float sec)
        {
            await rect.DOMove(to, sec, true);
        }

        private void SkipColor()
        {
            if (bankAccount.Debit(10)) { RemoveTopColor(); }
        }


        public void CheckIfColorAtTopMatch(Color color, float diff, Action reduce)
        {
            visibleDistance = diff;
            reduceCallback = reduce;

            bool similarColor = IsColorSimilarTo(color);

            if (similarColor) { RemoveTopColor(); }
            else { sceneColors.Add(color); }
        }

        private bool IsColorSimilarTo(Color color)
        {
            float distance = Mathf.Sqrt(
                  MathF.Pow(color.r - current_color.r, 2) +
                  Mathf.Pow(color.g - current_color.g, 2) +
                  Mathf.Pow(color.b - current_color.b, 2)
                 );
            return distance <= visibleDistance;
        }



        public void RemoveTopColor()
        {
            PopColorItem();
            current_color = m_colors.Pop();
            reduceCallback?.Invoke();
           // CheckForRemnantColors();
        }

        private async void PopColorItem()
        {
            int last = content.childCount - 1;
            RectTransform rect = content.GetChild(last).GetComponent<RectTransform>();
            Vector2 screenPos = rect.position;
            screenPos.y += 100;
            await MoveTo(rect, screenPos, .5f);
            Destroy(content.GetChild(last).gameObject);
            Arrange().Forget();
        }

        private void CheckForRemnantColors()
        {
            foreach (Color color in sceneColors)
            {
                bool match = IsColorSimilarTo(color);
                if (match)
                {
                    RemoveTopColor();
                    CheckForRemnantColors();
                    break;
                }

            }
        }
    }
}
