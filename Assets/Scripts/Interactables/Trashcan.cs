using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if(boxCollider == null)
        {
            Debug.LogError("Trashcan requires a BoxCollider2D component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
