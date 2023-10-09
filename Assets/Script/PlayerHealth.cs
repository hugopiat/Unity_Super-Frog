using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float InvincibilityDelayTime = 2f;
    public bool Invincible = false;
    public bool isdie = false;


    public static PlayerHealth instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Attention, instance > 1 !");
            return;
        }
        instance = this;
    }


    PlayerController playerController;

    SpriteRenderer EnnemiRenderer;
    public HealthBar HealthBar;

    void Start()
    {
        EnnemiRenderer = GetComponent<SpriteRenderer>();// R�cup�rer le component sprite renderer en dessous de cette ligne
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(60);
        if (Input.GetKeyDown(KeyCode.U))
            Heal(10);
    }

    //Inflige des d�g�ts
    public void TakeDamage(int damages)
    {
        if (!Invincible)
        {
            currentHealth -= damages;
            HealthBar.SetHealth(currentHealth);


        if(currentHealth <= 0)
        {
            isdie = true;
            Die();
            return;
        }

            Invincible = true;
            playerController.Hit = true;
            playerController.canMove = false;
            StartCoroutine(InvincibilityDelay());
        }

    }

    public void Die()
    {
        Debug.Log("Le joueur est éliminé");
        PlayerController.instance.enabled = false;
        PlayerController.instance.playerAnim.SetBool("mort",isdie);
        PlayerController.instance.playerRB.bodyType = RigidbodyType2D.Kinematic;
        PlayerController.instance.playerRB.velocity = Vector3.zero;
        PlayerController.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Heal(int heal)
    {
        int x = 0;
        while (x < heal && HealthBar.instance.slider.value + 1 <= 100)
        {
            currentHealth += 1;
            HealthBar.SetHealth(currentHealth);
            x += 1;
        }
    }

    public IEnumerator InvincibilityDelay()
    {
        yield return new WaitForSeconds(InvincibilityDelayTime / 2);
        Invincible = false;
        playerController.Hit = false;
    }
}
