using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InsurgentSelector : MonoBehaviour
{
    [SerializeField]
    public VisualTreeAsset toggleUI;
    List<int> selectedCharacterAddresses = null;
    List<int> characterAddresses = null;
    private VisualElement root = null;
    private bool isOpen = false;
    private List<Toggle> toggles = new List<Toggle>();

    // Start is called before the first frame update
    void Start()
    {
        characterAddresses = Game.GET_GAME_STATE().GetMailSystem().GetAllMailBoxAddresses().ToList();
        selectedCharacterAddresses = Game.GET_GAME_STATE().GetGameFlags().GetSelectedCharacterAddresses();
        // GameObject toggleGroup = new GameObject("Selector");
        // toggleGroup.gameObject.transform.SetParent(transform);
        // Load();
        root = GetComponent<UIDocument>().rootVisualElement;
        root.AddToClassList("hide");
        // StartCoroutine("BindObject");

    }

    void BindObject()
    {
        do
        {
            root = GetComponent<UIDocument>().rootVisualElement;
        } while (root == null);
        Debug.Log("Got root. " + root);
        // root.Bind(new SerializedObject(this));
        root.AddToClassList("hide");
    }

    void OnEnable()
    {

    }

    public void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    // private ToggleButtonGroup Load()
    // {
    //     // var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(string.Format("Assets/UI/uxml/{0}.uxml", fileName));
    //     // root.Add(xml.Instantiate());
    //     // TemplateContainer container = xml.CloneTree();
    //     // ToggleButtonGroup group = container.Q("Container").Q<ToggleButtonGroup>("AddressList");
    //     // Debug.Log(group);
    //     ToggleButtonGroup group = new ToggleButtonGroup("Toggle Buttons");
    //     group.isMultipleSelection = true;
    //     group.allowEmptySelection = true;
    //     return group;
    // }

    public void Open()
    {
        isOpen = true;
        Game.GET_PLAYER().GetControls().ToggleMovementState(false);
        root.RemoveFromClassList("hide");
        // GUI ui = new GUI();
        // TemplateContainer container = toggleUI.Instantiate();
        // root.style.display = DisplayStyle.Flex;
        // UIDocument ui = gameObject.AddComponent<UIDocument>();
        // ui.panelSettings = settings;
        // root = GetComponent<UIDocument>().rootVisualElement;
        // root.Bind(new SerializedObject(this));
        // root.RemoveFromClassList("hide");
        ToggleButtonGroup element = root.Q<ToggleButtonGroup>("AddressList");
        PopulateInsurgentSelector(element);
    }

    public void Close()
    {
        // root.style.display = DisplayStyle.None;
        Game.GET_PLAYER().GetControls().ToggleMovementState(true);
        Game.GET_GAME_STATE().GetGameFlags().SetSelectedCharacterAddresses(selectedCharacterAddresses);
        root.AddToClassList("hide");
        // root = null;
        isOpen = false;
    }

    private void PopulateInsurgentSelector(ToggleButtonGroup toggleGroup)
    {
        // ToggleButtonGroup AddressList = Load();
        // ToggleButtonGroup AddressList = root.Q<ToggleButtonGroup>("AddressList");
        // AddressList.Clear();
        // AddressList.Add(new Label("Select up to 3 Mailing IDs for possible Insurgents:"));
        // AddressList.BringToFront();
        // Toggle[] characterButton = AddressList.Query<Toggle>("Toggle").ToList().ToArray();
        int index = 0;
        Toggle[] toggles = toggleGroup.Query<Toggle>().ToList().ToArray();
        foreach (int address in characterAddresses)
        {
            toggles[index].label = address.ToString();
            if (selectedCharacterAddresses.Contains(address))
            {
                toggles[index].value = true;
            }
            index++;
        }
        // characterList.Clear();
        // foreach (int address in characterAddresses)
        // {
        //     Button characterButton = new Button(() => SelectCharacter(address));
        //     characterButton.text = "Address " + address;
        //     if (selectedCharacterAddresses.Contains(address))
        //     {
        //         characterButton.style.backgroundColor = Color.green;
        //     }
        //     characterList.Add(characterButton);
        // }
        // var group = new GroupBox("Group Example");
        // group.style.backgroundColor = Color.black;
        // // Must register change events on each radio button.
        // var choice1 = new Toggle("First Choice");
        // choice1.RegisterValueChangedCallback(v => Debug.Log("Choice 1 is : " + v.newValue));
        // var choice2 = new Toggle("Second Choice");
        // choice2.RegisterValueChangedCallback(v => Debug.Log("Choice 2 is : " + v.newValue));
        // var choice3 = new Toggle("Third Choice");
        // choice3.RegisterValueChangedCallback(v => Debug.Log("Choice 3 is : " + v.newValue));
        // group.Add(choice1);
        // group.Add(choice2);
        // group.Add(choice3);
        // VisualElement container = root.Q<VisualElement>("Container");
        // container.Clear();
        // container.Add(group);
    }

    // private void SetButtonDetails(Toggle toggle, int address)
    // {
    //     if (toggle == null)
    //     {
    //         Game.LogError("Edit", "SetButtonDetails");
    //         return;
    //     }
    //     toggle.label = address.ToString();
    //     // toggle.text = "Address: " + address;
    //     // toggle.name = address.ToString();
    //     // if (selectedCharacterAddresses.Count > 0 && selectedCharacterAddresses.Contains(address))
    //     // {
    //     //     characterButton.SetValueWithoutNotify(true);
    //     // }
    //     toggle.RegisterValueChangedCallback(x => {
    //         SelectCharacter(x.newValue, address);
    //         toggle.value = x.newValue;
    //     });
    //     // toggle.BringToFront();
    // }

    public List<int> GetSelectedCharacterAddresses()
    {
        return selectedCharacterAddresses;
    }

    public void DeselectCharacter(int address)
    {
        if (selectedCharacterAddresses.Contains(address))
        {
            selectedCharacterAddresses.Remove(address);
        }
    }

    public void SelectCharacter(bool state, int address)
    {
        if (state)
        {
            if (selectedCharacterAddresses.Count < 3 && !selectedCharacterAddresses.Contains(address) && characterAddresses.Contains(address))
            {
                selectedCharacterAddresses.Add(address);
            }

        }
        else
        {
            DeselectCharacter(address);
        }
    }
}
