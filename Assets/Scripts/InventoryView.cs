using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public RectTransform Content;
    private GridLayoutGroup _GridLayout;

    private float _TweenDuration = 0.15f;
    private float _TweenDelay = 0.05f;

    [SerializeField]
    private int _AddItemIndex;
    [SerializeField]
    private int _AddItemSlotIndex;
    [SerializeField]
    private int _AddItemSlotDir;

    private List<ItemView> _ItemViews = new List<ItemView>();
    private ItemSlot[] _ItemSlots;

    private ItemView _ItemViewAsset;

    void Awake()
    {
        _GridLayout = Content.GetComponent<GridLayoutGroup>();
        _ItemSlots = _GridLayout.GetComponentsInChildren<ItemSlot>();
        for (int i = 0; i < _ItemSlots.Length; i++)
        {
            var itemSlot = _ItemSlots[i];
            itemSlot.Index = i;
        }
    }

    [ContextMenu("添加随机道具")]
    void AddRandomItem()
    {
        var list = ObjManager.Instance.ItemObjList;
        var name = list[_AddItemIndex].name;
        AddItemView(name, _AddItemSlotIndex, _AddItemSlotDir, true);
    }

    void OnEnable()
    {
        TweenUtils.RippleGridItems(_GridLayout, _TweenDuration, _TweenDelay);
    }

    void OnDisable()
    {
        for (int i = 0; i < _ItemSlots.Length; i++)
        {
            var itemSlot = _ItemSlots[i];
            itemSlot.SetData(null);
        }

        foreach (var itemView in _ItemViews)
        {
            Destroy(itemView.gameObject);
        }
        _ItemViews.Clear();
    }

    public void AddItemView(string itemName, int index, int dir, bool unlock)
    {
        var itemObj = ObjManager.Instance.GetItemObj(itemName);
        if (!CollectItemSLot(index, itemObj.ShapeObj, dir, out var itemSlots))
        {
            Debug.LogError($"无法添加物品{itemName}");
            return;
        }

        _ItemViewAsset ??= Resources.Load<ItemView>("Prefabs/ItemView");

        ItemView itemView = Instantiate(_ItemViewAsset, transform);
        itemView.SetData(itemName, dir);
        _ItemViews.Add(itemView);

        (itemView.transform as RectTransform).anchoredPosition = CalcSlotAnchorCenter(itemSlots);

        foreach (var itemSlot in itemSlots)
        {
            itemSlot.SetData(itemView);
        }
    }

    bool CollectItemSLot(int index, ShapeObj shapeObj, int dir, out List<ItemSlot> itemSlots, bool check = true)
    {
        itemSlots = new List<ItemSlot>();
        int[] grid = shapeObj.RotateGrid(dir, out var rows, out var cols);
        int gridCols = cols;
        int gridRows = rows;
        int slotCols = _GridLayout.constraintCount;
        for (int y = 0; y < gridRows; y++)
        {
            for (int x = 0; x < gridCols; x++)
            {
                int gridI = y * gridCols + x;
                if (grid[gridI] == 0)
                    continue;

                int rowFirstI = index + y * slotCols + 0;
                int slotI = index + y * slotCols + x;

                int indexRow = rowFirstI / slotCols;
                int slotRow = slotI / slotCols;

                if (indexRow != slotRow)
                    return false;

                if (check)
                    {
                        if (slotI >= _ItemSlots.Length)
                            return false;
                        if (grid[gridI] == 1 && !_ItemSlots[slotI].IsEmpty)
                            return false;
                    }

                itemSlots.Add(_ItemSlots[slotI]);
            }
        }

        return true;
    }

    Vector2 CalcSlotAnchorCenter(List<ItemSlot> itemSlots)
    {
        Vector2 anchor = Vector2.zero;
        foreach (var slot in itemSlots)
        {
            anchor += (slot.transform as RectTransform).anchoredPosition;
        }
        anchor /= itemSlots.Count;
        return anchor;
    }

}
