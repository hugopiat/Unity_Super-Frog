using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Coins2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Inventory.instance.AddCoins(2);
            Destroy(gameObject);
        }
    }
}
