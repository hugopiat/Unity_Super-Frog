using UnityEngine;
public class WeakSpot : MonoBehaviour
{
    public GameObject ObjectToDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playercontroller = collision.transform.GetComponent<PlayerController>();
            playercontroller.GetComponent<Rigidbody2D>().velocity = new Vector2(playercontroller.GetComponent<Rigidbody2D>().velocity.x, playercontroller.jumpPower*1.5f);
            //Ennemy_Patrol ennemy_Patrol = collision.transform.GetComponent<Ennemy_Patrol>();
            //ennemy_Patrol.Hit = true;
            Destroy(ObjectToDestroy);
        }
    }
}
