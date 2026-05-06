using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Letter> letters;
    public GameObject inventoryUI;
    private Vector3 targetPosition;
    private float movementSpeed = 20f;
    private bool isVisible = false;
    private bool isMoving = false;
    private Transform[] letterSlots;
    // Start is called before the first frame update
    void Start()
    {
        letters = new List<Letter>();
        inventoryUI.transform.position = new Vector3(1400f, inventoryUI.transform.position.y, inventoryUI.transform.position.z);
        letterSlots = new Transform[inventoryUI.transform.childCount];
        for (int i = 0; i < inventoryUI.transform.childCount; i++)
        {
            letterSlots[i] = inventoryUI.transform.GetChild(i).transform;
        }
    }

    public void addLetter(Letter letter)
    {
        letters.Add(letter);
        letter.gameObject = new GameObject("Letter for " + letter);
        letter.gameObject.transform.position = letterSlots[letters.Count - 1].GetChild(1).transform.position; // Position the letter in the corresponding slot in the inventory UI

    }

    public void removeLetter(Letter letter)
    {
        letters.Remove(letter);
    }

    public List<Letter> getLetters()
    {
        return letters;
    }

    public int letterCount()
    {
        return letters.Count;
    }

    public void showInventory()
    {
        if (isMoving)
        {
            return;
        }

        if (isVisible)
        {
            targetPosition = new Vector3(1400f, inventoryUI.transform.position.y, inventoryUI.transform.position.z);
            // inventoryUI.transform.position = moveInventory(targetPosition);
            // moveInventory(targetPosition);
            isMoving = true;
            isVisible = false;
        }
        else
        {
            targetPosition = new Vector3(950f, inventoryUI.transform.position.y, inventoryUI.transform.position.z);
            // inventoryUI.transform.position = moveInventory(targetPosition);
            // moveInventory(targetPosition);
            isMoving = true;
            isVisible = true;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            inventoryUI.transform.position = Vector3.Lerp(inventoryUI.transform.position, targetPosition, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(inventoryUI.transform.position, targetPosition) < 0.1f)
            {
                inventoryUI.transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    private Vector3 moveInventory(Vector3 position)
    {
        // MoveToPosition(inventoryUI.transform.position, position, 100f);
        float timeElapsed = 0;
        while (inventoryUI.transform.position != position && timeElapsed < 30f)
        {
            Debug.Log("Moving inventory: " + timeElapsed);
            inventoryUI.transform.position = Vector3.Lerp(inventoryUI.transform.position, position, movementSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
        }
        return position;
    }

    public IEnumerator MoveToPosition(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            Debug.Log("Moving inventory: " + timeElapsed);
            inventoryUI.transform.position = Vector3.Lerp(startPosition, endPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        inventoryUI.transform.position = endPosition; // Ensure it reaches the exact end position
    }
}
