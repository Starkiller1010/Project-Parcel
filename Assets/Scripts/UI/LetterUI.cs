using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(Workstation))]
public class LetterUI : MonoBehaviour
{
    [SerializeField]
    public VisualTreeAsset letterUXML;
    private VisualElement page;
    private GameObject panel;
    private GUI ui;
    private Letter letter;
    private Button confirmBtn;
    private Button cancelBtn;

    public void Open(GUI gui,Letter letter)
    {
        // ui = gui;
        SetLetter(letter); 
    }

    public void Close()
    {
        if (ui != null)
        {
            ui = null;
            letter = null;
            page.RemoveFromHierarchy();
        } else if (panel != null)
        {
            Destroy(panel);
            panel = null;
        }
         else
        {
            Debug.LogError("No UI to close.");
        }
    }

    private void SetLetter(Letter letter)
    {
        this.letter = letter;
        ShowLetter();
        // SetText(page.Q<Label>());
        // SetButtons(page.Q("Button-Container"));

    }

    private void CreateBackground()
    {
        GameObject obj = GameObject.Find("Canvas");
        if (obj == null)
        {
            Debug.LogError("Canvas GameObject not found in the scene.");
        }
        else
        {
            panel = CreateUIObject(obj.transform, "LetterBackground");
            RectTransform panelRect = panel.GetComponent<RectTransform>();
            panelRect.sizeDelta = new Vector2(Screen.width, Screen.height); // Make the background cover the entire screen
            // panelRect.anchoredPosition = Vector2.zero; // Position the background at the center of the screen
            panelRect.localScale = Vector3.one; // Ensure the background is not scaled
            Image bg = panel.GetComponent<Image>();
            bg.sprite = Resources.Load<Sprite>("Sprite/Square");
            bg.color = new Color(0, 0, 0, 0.5f); // Semi-transparent black background
            // panelRect.position = Vector3.zero; // Position the background at the center of the screen
        }
    }

    private void ShowLetter()
    {
        CreateBackground();
        CreateLetterUI();
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.localPosition = Vector3.zero; // Position the background at the center of the screen
    }

    private void CreateLetterUI()
    {
        GameObject letterUI = CreateUIObject(panel.transform, "LetterUI");
        // Set the position and size of the letter UI as needed
        RectTransform rectTransform = letterUI.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 225);
        rectTransform.anchoredPosition = Vector2.zero; // Center the letter UI
        letterUI.layer = LayerMask.NameToLayer("UI");
        CreateLetterText(letterUI.transform);
    }

    private void CreateLetterText(Transform parent)
    {
        GameObject textObj = CreateUIObject(parent.transform, "LetterText");
        DestroyImmediate(textObj.GetComponent<Image>());// Remove the Image component since we only want to display text  
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = FileManager.GetLetterContent(letter);
        // text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 14;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.black;
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(180, 200);
        rectTransform.anchoredPosition = Vector2.up; // Position the text slightly above the center of the letter UI
        // CreateButtons(parent);
    }

    private GameObject CreateUIObject(Transform parent, string name = "uiElement")
    {
        GameObject ui = new GameObject(name);
        ui.transform.SetParent(parent);
        ui.AddComponent<CanvasRenderer>();
        ui.AddComponent<RectTransform>().localScale = Vector3.one;
        ui.AddComponent<Image>();
        return ui;
    }

    private void CreateButtons(Transform parent)
    {
        GameObject buttonContainer = CreateUIObject(parent, "ButtonContainer");
        RectTransform rectTransform = buttonContainer.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(180, 50);
        rectTransform.anchoredPosition = Vector2.down * 75; // Position the button container below the text
        // CreateConfirmButton(buttonContainer.transform);
        // CreateCancelButton(buttonContainer.transform);
    }

    private void ShowLetter(VisualElement container)
    {
        page = letterUXML.CloneTree();
        container.Add(page);
    }

    private void SetText(Label label)
    {
        label.text = FileManager.GetLetterContent(letter);
    }

    private void SetButtons(VisualElement container)
    {
        container.Q<Button>(className: "confirm").clicked += delegate { Confirm(letter); };
        container.Q<Button>(className: "cancel").clicked += Cancel;
    }

    void Confirm(Letter letter)
    {
        Game.GET_GAME_STATE().GetGameFlags().AddCompletedLetter(letter);
        Close();
        GetComponent<Workstation>().EnableWorkTable();
    }
    
    void Cancel()
    {
        Close();
        GetComponent<Workstation>().EnableWorkTable();
    }
}