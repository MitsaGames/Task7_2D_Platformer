using UnityEngine;

public class Knight : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Enemy>())
        {
            Destroy(this.gameObject);
        }
    }
}
