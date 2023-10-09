using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy_Patrol: MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public int damage = 10;
    public bool Hit = false;

    private Transform target;
    private int desPoint = 0;

    SpriteRenderer EnnemiRenderer; // Propri�t� qui tiendra la r�f�ence du sprite rendered de notre player

    PlayerHealth playerHealth;
    PlayerController playercontroller;
    PlayerController playerannim;


    // Start is called before the first frame update
    void Start()
    {
        EnnemiRenderer = GetComponent<SpriteRenderer>();// R�cup�rer le component sprite renderer en dessous de cette ligne
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            desPoint = (desPoint + 1) % waypoints.Length;
            target = waypoints[desPoint]; 
            EnnemiRenderer.flipX = !EnnemiRenderer.flipX; // M�me chose ici pour que notre flipx et facingRight soient en phase
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            int cote;
            playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playercontroller = collision.transform.GetComponent<PlayerController>();
            playerannim = collision.transform.GetComponent<PlayerController>();
            playerHealth.TakeDamage(damage);
            
            if (!playerHealth.isdie)
            {
                if (playercontroller.facingRight)
                    cote = -1;      
                else
                    cote = 1;

                playercontroller.playerRB.velocity = new Vector2(cote*playercontroller.jumpPower, playercontroller.jumpPower);
                playercontroller.canMove = false;
                playerannim.Hit = true;
                playercontroller.MAJ();
            }
        }
    }
}
