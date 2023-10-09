using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Coins : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Inventory.instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
}
