using System;
using UnityEngine;

[Flags]
public enum E_Attr
{
    NULL = 0,
    PLASTICS = 2 << 0,
}

[CreateAssetMenu(fileName = "ItemObj", menuName = "GameObj/ItemObj")]
public class ItemObj : ScriptableObject
{
    public ShapeObj ShapeObj;
    public E_Attr Attr;
    public int Quality;
    public int Multiple;
    public string Pic => name.Replace("ItemObj_", "");
    public float PicScale = 1;
    public float PicAngle;
}