using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    static List<Letter> collectedLetter = new List<Letter>();
    Movement controls = new Movement();

    void Start()
    {
        controls.SetBody(this.GetComponent<Rigidbody2D>());
    }

    // public bool hasMail = false;
    // public int mailCount = 0;
    // private Inventory inventory;

    // public Text mailCountText = null;

    // public Image mailIcon = null;
    // public bool outside = false;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     if (mailIcon != null)
    //     {
    //         mailIcon.enabled = false;
    //     }

    //     inventory = GetComponent<Inventory>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     mailCount = inventory.letterCount();
    //     hasMail = mailCount > 0;
    //     if (mailIcon != null && hasMail)
    //     {
    //         mailIcon.enabled = true;
    //         mailCountText.enabled = true;
    //     }
    //     else
    //     {
    //         mailIcon.enabled = false;
    //         mailCountText.enabled = false;
    //     }
    //     mailCountText.text = mailCount.ToString();

    //     if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
    //     {
    //         inventory.showInventory();
    //     }

    // }

    // public void DeliverMail(int amount)
    // {
    //     mailCount -= amount;
    //     if (mailCount < 0)
    //     {
    //         mailCount = 0;
    //     }
    //     inventory.removeLetter(inventory.getLetters()[inventory.getLetters().Count - 1]);
    // }

    // public void CollectLetters(params Letter[] letters)
    // {
    //     foreach (Letter letter in letters)
    //     {
    //         inventory.addLetter(letter);
    //     }
    // }
}
