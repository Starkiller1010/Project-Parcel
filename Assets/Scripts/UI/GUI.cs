using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GUI : MonoBehaviour
{
    private static VisualElement gui;
    public enum Column { Left, Middle, Right };
    public enum Row { Header, Body, Footer };

    public GUI()
    {
        // if (gui == null)
        //     CreateGUI();
    }

    void Start()
    {
        CreateGUI();
    }

    // void OnDestroy()
    // {
    //     DestroyGUI();
    // }

    public static VisualTreeAsset LoadModal()
    {
        return LoadStaticTemplate("ModalArea");
    }

    public static VisualTreeAsset LoadDialogueBox()
    {
        return LoadStaticTemplate("DialogueBox");
    }

    private static VisualTreeAsset LoadStaticTemplate(string fileName)
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(string.Format("Assets/UI/static/uxml/{0}.uxml", fileName));
        if (visualTree != null)
        {
            // Clone the UXML and add it to the root VisualElement
            VisualElement root = visualTree.CloneTree();
            gui.Add(root);
            return visualTree;
        }
        else
        {
            Debug.LogError("UXML file not found at specified path.");
            return null;
        }
    }

    private VisualTreeAsset LoadTemplate(string fileName)
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(string.Format("Assets/UI/uxml/{0}.uxml", fileName));
        if (visualTree != null)
        {
            // Clone the UXML and add it to the root VisualElement
            VisualElement root = visualTree.CloneTree();
            gui.Add(root);
            return visualTree;
        }
        else
        {
            Debug.LogError("UXML file not found at specified path.");
            return null;
        }
    }

    private void CreateGUI()
    {
        // GameObject obj = GameObject.Find("GUI");
        gui = GetComponent<UIDocument>().rootVisualElement;
        gui.Bind(new SerializedObject(this));
    }

    public void DestroyGUI()
    {
        if (gui != null) gui.RemoveFromHierarchy();
    }

    private VisualElement GetCol(Column col)
    {
        switch (col)
        {
            case Column.Left:
                return gui.Q<VisualElement>("Body").Q<VisualElement>(className: "left");
            case Column.Middle:
                return gui.Q<VisualElement>("Body").Q<VisualElement>(className: "middle");
            case Column.Right:
                return gui.Q<VisualElement>("Body").Q<VisualElement>(className: "right");
            default:
                Debug.LogError("No Column Found");
                return null;
        }
    }

    public VisualElement ChangeColumnWithClass(Column column, string className)
    {
        VisualElement element = GetCol(column);
        element.AddToClassList(className.ToLower());
        return element;
    }

    public void RemoveClassFromColumn(Column column, string className)
    {
        VisualElement element = GetCol(column);
        element.RemoveFromClassList(className.ToLower());
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

    public void AddElement(Column column, VisualElement element)
    {
        if (element != null)
        {
            GetCol(column).Add(element);
        }
        else
        {
            LogError("Add", "AddElement");
        }
    }

    public void RemoveElement(Column column, VisualElement element)
    {
        if (element != null)
        {
            GetCol(column).Remove(element);
        }
        else
        {
            LogError("Remove", "RemoveElement");
        }
    }

    public void AddElementToRow(Column column, VisualElement element, int index = 0)
    {
        if (element != null)
        {
            CheckRows(column)[index].Add(element);
        }
        else
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

    public T AddTemplateToColumn<T>(Column col, string fileName) where T : VisualElement
    {
        VisualTreeAsset template = LoadTemplate(fileName);
        if (template != null)
        {
            return AddTemplateToColumn<T>(col, template);
        }
        else
        {
            LogError("Load", "AddTemplateToColumn");
            return null;
        }
    }

    public T AddTemplateToColumn<T>(Column col, VisualTreeAsset template) where T : VisualElement
    {
        TemplateContainer element = template.Instantiate();
        AddElement(col, element);
        return element.ElementAt(0) as T;
    }

    public T AddTemplateToColumnWithRow<T>(Column col, VisualTreeAsset template, int rowIndex) where T : VisualElement
    {
        TemplateContainer element = template.Instantiate();
        AddElementToRow(col, element, rowIndex);
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