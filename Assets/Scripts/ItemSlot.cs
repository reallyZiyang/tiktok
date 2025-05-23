using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public RectTransform ContentRoot;
    public Image Frame;

    public int Index;

    public bool IsEmpty => ItemView == null;
    public ItemView ItemView;

    public void SetData(ItemView itemView)
    {
        ItemView = itemView;

        var frameColorStr = "#404040";

        if (itemView != null)
        {
            int quality = itemView.Data.Obj.Quality;
            switch (quality)
            {
                case 1:
                    frameColorStr = "#35383B";
                    break;
                case 2:
                    frameColorStr = "#31594E";
                    break;
                case 3:
                    frameColorStr = "#2E3C60";
                    break;
                case 4:
                    frameColorStr = "#46465A";
                    break;
                case 5:
                    frameColorStr = "#65524A";
                    break;
            }
        }

        ColorUtility.TryParseHtmlString(frameColorStr, out var color);
        Frame.color = color;
    }
}
