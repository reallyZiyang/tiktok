using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeObj", menuName = "GameObj/ShapeObj")]
public class ShapeObj : ScriptableObject
{
    [SerializeField]
    public int Width;
    [SerializeField]
    public int Height;
    [SerializeField]
    [ReadOnly]
    public int[] Grid;
    [ReadOnly]
    [SerializeField]
    public int Occupy;

    public void UpdateOccupy()
    {
        Occupy = 0;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (Grid[i * Width + j] != 0)
                {
                    Occupy++;
                }
            }
        }
    }

    public int[] RotateGrid(int dir, out int height, out int width)
    {
        var spacePos0 = new int[Grid.Length];
        var spacePos90 = new int[Grid.Length];
        var spacePos180 = new int[Grid.Length];
        var spacePos270 = new int[Grid.Length];

        height = Height;
        width = Width;

        if (Grid.Length == 1)
            return Grid;

        if (dir == 1 || dir == 3)
        {
            height = Width;
            width = Height;
        }

        int rows = height;
        int cols = width;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j;
                spacePos0[index] = Grid[index];
                spacePos90[(cols - 1 - j) * rows + i] = Grid[index];
                spacePos180[(rows - 1 - i) * cols + cols - 1 - j] = Grid[index];
                spacePos270[(cols - 1 - j) * rows + i] = Grid[index];
            }
        }

        if (dir == 1)
        {
            return spacePos0;
        }
        else if (dir == 2)
        {
            return spacePos0;
        }
        else if (dir == 3)
        {
            return spacePos0;
        }
        else
        {
            return spacePos0;
        }
    }

}


#if UNITY_EDITOR

public class ReadOnlyAttribute : PropertyAttribute { }

[UnityEditor.CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        UnityEditor.EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

[UnityEditor.CustomEditor(typeof(ShapeObj))]
public class ShapeObjInspector : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ShapeObj shapeObj = (ShapeObj)target;

        if (shapeObj.Grid == null || shapeObj.Grid.Length != shapeObj.Width * shapeObj.Height)
        {
            shapeObj.Grid = new int[shapeObj.Width * shapeObj.Height];
        }

        GUILayout.Space(10);
        GUILayout.Label("Grid:");

        bool update = false;
        for (int x = 0; x < shapeObj.Height; x++)
        {
            GUILayout.BeginHorizontal();
            for (int y = 0; y < shapeObj.Width; y++)
            {
                bool value = GUILayout.Toggle(shapeObj.Grid[x * shapeObj.Width + y] == 1, "", GUILayout.Width(20));
                if (value != (shapeObj.Grid[x * shapeObj.Width + y] == 1))
                {
                    update = true;
                }
                shapeObj.Grid[x * shapeObj.Width + y] = value ? 1 : 0;
            }
            GUILayout.EndHorizontal();
        }

        if (update)
        {
            shapeObj.UpdateOccupy();
            UnityEditor.EditorUtility.SetDirty(shapeObj);
            UnityEditor.AssetDatabase.SaveAssets();
        }

    }
}
#endif