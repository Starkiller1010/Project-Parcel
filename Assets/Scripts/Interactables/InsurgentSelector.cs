using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsurgentSelector : MonoBehaviour
{
    List<int> selectedCharacterAddresses = null;
    List<int> characterAddresses = null;

    // Start is called before the first frame update
    void Start()
    {
        characterAddresses = Game.GET_GAME_STATE().GetMailSystem().GetAllMailBoxAddresses().ToList();
        selectedCharacterAddresses = Game.GET_GAME_STATE().GetGameFlags().GetSelectedCharacterAddresses();
    }

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

    public void SelectCharacter(int address)
    {
        if (selectedCharacterAddresses.Count < 3 && !selectedCharacterAddresses.Contains(address) && characterAddresses.Contains(address))
        {
            selectedCharacterAddresses.Add(address);
        }
    }
}
