using UnityEngine;

public class PickUp_Fruit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth.instance.Heal(10);
            Destroy(gameObject);
        }
    }
}
