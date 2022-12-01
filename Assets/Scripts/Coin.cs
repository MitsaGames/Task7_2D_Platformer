using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Knight>())
        {
            gameObject.SetActive(false);
        }
    }
}
