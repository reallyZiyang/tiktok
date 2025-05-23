using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData
{
    public readonly string Name;
    public int Index;
    public readonly ItemObj Obj;
    public ItemData(string name)
    {
        Name = name;
        Obj = ObjManager.Instance.GetItemObj(name);
    }
}

public class ItemView : MonoBehaviour
{
    public Image ImgPic;

    public ItemData Data;
    public int Dir;
    public void SetData(string name, int dir)
    {
        Data = new ItemData(name);
        Dir = dir;

        ImgPic.sprite = Resources.Load<Sprite>($"Sprites/Item/{Data.Obj.Pic}");
        ImgPic.transform.localScale = Vector3.one * Data.Obj.PicScale;
        ImgPic.transform.localEulerAngles = new Vector3(0, 0, Data.Obj.PicAngle + dir * -90);
    }

}
