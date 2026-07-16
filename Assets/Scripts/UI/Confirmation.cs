using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Confirmation : MonoBehaviour
{
    public VisualElement ui;
    public Button confirmButton;
    public Button rejectButton;
    private bool onConfirm = false;
    private Action onConfirmAction = null;
    private Action onRejectAction = null;
    private bool opened = false;


    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        
    }

    void Update()
    {
        if (opened)
        {
            if (onConfirm)
            {
                OnConfirmFocus();
            }
            else
            {
                OnRejectFocus();
            }

            if (Game.GET_PLAYER().GetControls().ChangeOption())
            {
                onConfirm = !onConfirm;
            }
        }
    }

    public void OnConfirmFocus()
    {
        confirmButton.Focus();
    }

    public void OnRejectFocus()
    {
        rejectButton.Focus();
    }

    private void SetButtons()
    {
        confirmButton = ui.Q<Button>("ConfirmButton");
        rejectButton = ui.Q<Button>("RejectButton");
        confirmButton.clicked += OnConfirmClicked;
        rejectButton.clicked += OnRejectClicked;
    }

    public void SetActions(Action onConfirm, Action onReject)
    {
        if (confirmButton == null || rejectButton == null)
            SetButtons();
        onConfirmAction = onConfirm;
        onRejectAction = onReject;
        opened = true;  
    }

    private void OnConfirmClicked()
    {
        if (onConfirmAction != null)
        {
            onConfirmAction.Invoke();
        }
        opened = false;
    }

    private void OnRejectClicked()
    {
        if (onRejectAction != null)
        {
            onRejectAction.Invoke();
        }
        opened = false;
    }
}
