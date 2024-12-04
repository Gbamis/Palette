using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class UI_Bounce_Anim : MonoBehaviour, IPointerEnterHandler
{
    public RectTransform rect;
    public float originalScale;
    public float endScale;
    public float duration;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale.x;
    }
    public async void OnPointerEnter(PointerEventData ped)
    {
        HT.SFX.Core.ButtonHover();
        await rect.DOScale(endScale, duration).SetEase(Ease.OutBounce).OnComplete(() => rect.DOScale(originalScale, duration));
    }

}
