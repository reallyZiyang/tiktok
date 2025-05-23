using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public static class TweenUtils
{
    public static void RippleGridItems(GridLayoutGroup gridLayout, float duration = 0.3f, float delayPerStep = 0.1f)
    {
        int row = gridLayout.constraintCount;

        ItemSlot[] items = gridLayout.GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i].ContentRoot;
            int x = i % row;
            int y = i / row;
            float delay = delayPerStep * (x + y);

            var canvasGroup = item.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = item.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, duration).SetDelay(delay);

            item.localScale = Vector3.zero;
            item.DOScale(1, duration).SetDelay(delay);
        }
    }
    
}
