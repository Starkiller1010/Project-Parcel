using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(Workstation))]
public class LetterUI : MonoBehaviour
{
    [SerializeField]
    public VisualTreeAsset letterUXML;
    private GameObject letterUI;
    // private VisualElement page;
    private GameObject panel;
    private GUI ui;
    private Letter letter;
    private Button confirmBtn;
    private Button cancelBtn;
    private TextMeshProUGUI letterText;
    private Puzzle puzzle = null;

    public void Open(Letter letter)
    {
        if (letter != null && letter.GetContent() != "")
        {
            SetLetter(letter);
        }
        else
        {
            Debug.LogError("Invalid letter provided to LetterUI.Open");
        }
    }

    public void Open(Puzzle puzzle)
    {
        if (puzzle != null && puzzle.GetPuzzleText() != "")
        {
            SetPuzzle(puzzle);
        }
        else
        {
            Debug.LogError("Invalid puzzle provided to LetterUI.Open");
        }
    }

    public void Close()
    {
        // page.RemoveFromHierarchy();
        Destroy(letterUI);
        if (ui != null)
        {
            ui = null;
            letter = null;
        }
        else if (panel != null)
        {
            Destroy(panel);
            panel = null;
        }
        
        if (puzzle != null)
        {
            // puzzle.EndPuzzle();
            puzzle = null;
        }
    }

    private void SetLetter(Letter letter)
    {
        this.letter = letter;
        ShowLetter();
    }

    private void SetPuzzle(Puzzle puzzle)
    {
        this.puzzle = puzzle;
        ShowLetter(puzzle.GetPuzzleText());
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
            panelRect.localPosition = Vector3.zero; // Position the background at the center of the screen
        }
    }

    private void ShowLetter(string content = "")
    {
        CreateBackground();
        CreateLetterUI();
        // CreateButtons();
        if (content != "")
        {
            letterText.text = content;
        } else
        {
            letterText.text = FileManager.GetLetterContent(letter);
        }
    }

    private void CreateLetterUI()
    {
        letterUI = CreateUIObject(panel.transform, "LetterUI");
        // Set the position and size of the letter UI as needed
        RectTransform rectTransform = letterUI.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 225);
        rectTransform.anchoredPosition = Vector2.zero; // Center the letter UI
        letterUI.layer = LayerMask.NameToLayer("UI");
        CreateLetterText(letterUI.transform);
    }

    private void CreateLetterText(Transform parent)
    {
        letterUI = CreateUIObject(parent.transform, "LetterText");
        DestroyImmediate(letterUI.GetComponent<Image>());// Remove the Image component since we only want to display text  
        letterText = letterUI.AddComponent<TextMeshProUGUI>();
        letterText.fontSize = 14;
        letterText.alignment = TextAlignmentOptions.Center;
        letterText.color = Color.black;
        RectTransform rectTransform = letterUI.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(180, 200);
        rectTransform.anchoredPosition = Vector2.up; // Position the text slightly above the center of the letter UI
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

    // private void ShowLetter(VisualElement container)
    // {
    //     page = letterUXML.CloneTree();
    //     container.Add(page);
    // }

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