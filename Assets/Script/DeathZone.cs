
using UnityEngine;

public class DeathZone : MonoBehaviour
{

     public int damage = 10;

   private void OnTriggerEnter2D(Collider2D collision)
   {
       if(collision.CompareTag("Player"))
       {
                PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damage);
                PlayerController playercontroller = collision.transform.GetComponent<PlayerController>();
           if(!playerHealth.isdie)
           {
                collision.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;

           }
       }
   }
}
