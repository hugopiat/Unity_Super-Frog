using UnityEngine;




public class trap : MonoBehaviour
{
    PlayerHealth PlayerHealth;
     public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            PlayerController playercontroller = collision.transform.GetComponent<PlayerController>();
            
            if (!playerHealth.isdie)
            {
            if (playercontroller.facingRight)
            {
                print("te soul�ve a gauche");
                playercontroller.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f*(playercontroller.jumpPower), playercontroller.jumpPower) * 0.5f;
                playercontroller.cantMove();
                playercontroller.MAJ();               
            }

            if (!playercontroller.facingRight)
            {
                print("te soul�ve a droite");
                playercontroller.GetComponent<Rigidbody2D>().velocity = new Vector2(playercontroller.jumpPower, playercontroller.jumpPower) * 0.5f;
                playercontroller.cantMove();
                playercontroller.MAJ();
            }
            }
        }
    }
}
