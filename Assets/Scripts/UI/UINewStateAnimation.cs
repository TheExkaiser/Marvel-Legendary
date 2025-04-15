using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;


public class UINewStateAnimation : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    UIManager uiManager;
    [SerializeField] RectTransform animationRectangle;
    [SerializeField] GameObject animationText;
    [SerializeField] float rectangleDuration;
    [SerializeField] float textMoveDuration;
    [SerializeField] float textStayDuration;
    RectTransform textTransform;
    TextMeshProUGUI text;
    float textTransformX;

    private void Start()
    {
        text = animationText.GetComponent<TextMeshProUGUI>();
        textTransform = animationText.GetComponent<RectTransform>();
        textTransformX = textTransform.anchoredPosition.x;
    }

    public void PlayAnimation(string phaseName)
    {
        text.text = phaseName;
        Sequence seq = DOTween.Sequence();
        seq.Append(animationRectangle.DOScaleY(1, rectangleDuration));
        seq.Append(textTransform.DOAnchorPos(new Vector2(0, textTransform.anchoredPosition.y), textMoveDuration).SetEase(Ease.InQuad));
        seq.AppendInterval(textStayDuration);
        seq.Append(textTransform.DOAnchorPos(new Vector2(-textTransformX, textTransform.anchoredPosition.y), textMoveDuration).SetEase(Ease.OutQuad));
        seq.Append(animationRectangle.DOScaleY(0, rectangleDuration));
        seq.OnComplete(() => { EventManager.NewStateAnimationEnd(); textTransform.anchoredPosition = new Vector2(textTransformX, textTransform.anchoredPosition.y); });     
    }



}
