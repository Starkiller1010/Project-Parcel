using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Trashcan : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.GetLetters().Clear();
        }
    }
}
