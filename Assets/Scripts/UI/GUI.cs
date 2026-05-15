using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GUI
{
    private VisualElement gui;
    public enum Column { Left, Middle, Right };

    public GUI(GameObject gameObject)
    {
        CreateGUI(gameObject);
    }

    private void CreateGUI(GameObject gameObject)
    {
        gui = gameObject.GetComponent<UIDocument>().rootVisualElement;
        gui.Bind(new SerializedObject(gameObject));
    }

    private VisualElement GetCol(Column col)
    {
        switch (col)
        {
            case Column.Left:
                return gui.Q<VisualElement>(className: "left");
            case Column.Middle:
                return gui.Q<VisualElement>(className: "middle");
            case Column.Right:
                return gui.Q<VisualElement>(className: "right");
            default:
                Debug.LogError("No Column Found");
                return null;
        }
    }

    public VisualElement MakeTable()
    {
        VisualElement table = GetCol(Column.Middle);
        table.AddToClassList("table");
        return table;
    }

    public void AddRow(Column col)
    {
        VisualElement column = GetCol(col);
        if (column != null)
        {
            VisualElement row = new VisualElement();
            row.AddToClassList("row");
            column.Add(row);
        }
    }

    private VisualElement[] GetRows(Column column)
    {
        return GetCol(column).Query<VisualElement>(className: "row").ToList().ToArray();
    }

    public void HideGUI()
    {
        gui.AddToClassList("hide");
    }

    public void ShowGUI()
    {
        gui.RemoveFromClassList("hide");
    }

    public void AddElementToRow(Column column, VisualElement element, int index = 0)
    {
        if (element != null)
        {
            CheckRows(column)[index].Add(element);
        } else
        {
            LogError("Add", "AddElementToRow");
        }
    }

    public void RemoveElementFromRow(Column column, VisualElement element, int index = 0)
    {
        if (element != null)
        {
            CheckRows(column)[index].Remove(element);
        }
        else
        {
            LogError("Remove", "RemoveElementToRow");
        }
    }

    public VisualElement AddTemplateToColumn(Column col, VisualTreeAsset template, int index = 0)
    {
        TemplateContainer element = template.Instantiate();
        AddElementToRow(col, element, index);
        return element;
    }


    public T AddTemplateToColumn<T>(Column col, VisualTreeAsset template, int index = 0) where T: VisualElement
    {
        TemplateContainer element = template.Instantiate();
        AddElementToRow(col, element, index);
        return element.ElementAt(0) as T;
    }

    private VisualElement[] CheckRows(Column col)
    {
        VisualElement[] rows = GetRows(col);
        if (rows == null || rows.Length == 0)
        {
            AddRow(col);
            rows = GetRows(col);
        }
        return rows;
    }

    private void LogError(string action, string method)
    {
        Debug.LogError(string.Format("Failed to {0} element in {1}", action, method));
    }
}