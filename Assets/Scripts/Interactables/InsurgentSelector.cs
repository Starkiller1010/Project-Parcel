using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InsurgentSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject togglePrefab;
    List<int> selectedCharacterAddresses = null;
    List<int> characterAddresses = null;
    // private VisualElement root = null;
    private bool isOpen = false;
    private List<Toggle> toggles = new List<Toggle>();

    // Start is called before the first frame update
    void Start()
    {
        characterAddresses = Game.GET_GAME_STATE().GetMailSystem().GetAllMailBoxAddresses().ToList();
        selectedCharacterAddresses = Game.GET_GAME_STATE().GetGameFlags().GetSelectedCharacterAddresses();
        GameObject toggleGroup = new GameObject("Selector");
        toggleGroup.gameObject.transform.SetParent(transform);
        // root = GetComponent<UIDocument>().rootVisualElement;
        // root.Bind(new SerializedObject(this));
        // Load();
        // root.AddToClassList("hide");
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
        // root.style.display = DisplayStyle.Flex;
        Game.GET_PLAYER().GetControls().ToggleMovementState(false);
        // root.RemoveFromClassList("hide");
        PopulateInsurgentSelector();
        isOpen = true;
    }

    public void Close()
    {
        // root.style.display = DisplayStyle.None;
        Game.GET_PLAYER().GetControls().ToggleMovementState(true);
        Game.GET_GAME_STATE().GetGameFlags().SetSelectedCharacterAddresses(selectedCharacterAddresses);
        // root.AddToClassList("hide");
        // root = null;
        isOpen = false;
    }

    private void PopulateInsurgentSelector()
    {
        // ToggleButtonGroup AddressList = Load();
        // ToggleButtonGroup AddressList = root.Q<ToggleButtonGroup>("AddressList");
        // AddressList.Clear();
        // AddressList.Add(new Label("Select up to 3 Mailing IDs for possible Insurgents:"));
        // AddressList.BringToFront();
        // Toggle[] characterButton = AddressList.Query<Toggle>("Toggle").ToList().ToArray();
        // int index = 0;
        foreach (int address in characterAddresses)
        {
            GameObject toggleObject = Instantiate(togglePrefab, transform);
            
            // AddressList.Add(new Toggle() { text = address.ToSafeString(), tooltip = "Help Me"});
            // Toggle characterButton = new Toggle();
            // SetButtonDetails(characterButton[index], address);
            // index++;
            // AddressList.Add(characterButton);
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

        } else
        {
            DeselectCharacter(address);
        }
    }
}
