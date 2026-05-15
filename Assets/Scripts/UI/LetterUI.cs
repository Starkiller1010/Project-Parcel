using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Workstation))]
public class LetterUI : MonoBehaviour
{
    [SerializeField]
    public VisualTreeAsset letterUXML;
    private VisualElement page;
    private GUI ui;
    private Letter letter;
    private Button confirmBtn;
    private Button cancelBtn;

    public void Open(GUI gui,Letter letter)
    {
        ui = gui;
        SetLetterDetails(letter); 
    }

    public void Close()
    {
        if (ui != null)
        {
            ui = null;
            letter = null;
            page.RemoveFromHierarchy();
        }
    }

    private void SetLetterDetails(Letter letter)
    {
        this.letter = letter;
        page = ui.AddTemplateToColumn<VisualElement>(GUI.Column.Middle, letterUXML);
        SetText(page.Q<Label>());
        SetButtons(page.Q("Button-Container"));

    }

    private void SetText(Label label)
    {
        label.text = letter.content;
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