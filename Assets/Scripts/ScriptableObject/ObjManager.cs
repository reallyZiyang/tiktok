using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : SingletonObject<ObjManager>
{
    [SerializeField]
    private List<ItemObj> _ItemObjList = new List<ItemObj>();

    public List<ItemObj> ItemObjList => _ItemObjList;

    public ItemObj GetItemObj(string name)
    {
        return _ItemObjList.Find(x => x.name == name);
    }

    
}
