using UnityEngine;
using UnityEngine.UI;

public class Confirmation : MonoBehaviour
{
    public GameObject cursor;
    private bool onConfirm = false;
    private Vector3 rejectPosition = Vector3.zero;
    private Text promptText = null;
    // Start is called before the first frame update
    void Start()
    {
        cursor = this.transform.GetChild(1).gameObject;
        rejectPosition = cursor.transform.localPosition;
        this.promptText = GameObject.FindGameObjectWithTag("PromptText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (onConfirm)
            {
                cursor.transform.localPosition = AddCursorOffset(cursor.transform.localPosition);
            }
            else
            {
                cursor.transform.localPosition = SubtractCursorOffset(cursor.transform.localPosition);
            }
            onConfirm = !onConfirm;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!onConfirm)
            {
                cursor.transform.localPosition = SubtractCursorOffset(cursor.transform.localPosition);
            }
            else
            {
                cursor.transform.localPosition = AddCursorOffset(cursor.transform.localPosition);
            }
            onConfirm = !onConfirm;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (onConfirm)
            {

            }
            else
            {

            }

            this.gameObject.SetActive(false);

        }
    }

    void OnEnable()
    {
        onConfirm = false;
        cursor.transform.localPosition = rejectPosition;
    }

    public void SetPromptText(string text)
    {
        this.promptText.text = text;
    }

    private Vector3 AddCursorOffset(Vector3 position)
    {
        return position + new Vector3(400, 0, 0);
    }

    private Vector3 SubtractCursorOffset(Vector3 position)
    {
        return position - new Vector3(400, 0, 0);
    }
}
