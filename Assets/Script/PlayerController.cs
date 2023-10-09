using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB; // Propri�t� qui tiendra en r�f�ence le rigid body de notre player
    SpriteRenderer playerRenderer; // Propri�t� qui tiendra la r�f�ence du sprite rendered de notre player
    public Animator playerAnim; // Propriete qui tiendra la reference a notre composant animator
    public bool facingRight = true; // Par d�faut, notre player regarde � droite
    public float maxSpeed; // Vitesse maximale que notre player peut atteindre en se d�placant
    public bool grounded = false; // Valeur qui traque si l'utilisateur est contre un mur ou non
    bool walled = false;
    public bool Hit = false;
    bool Jump;
    bool DoubleJump;
    public bool canMove = true;
    float groundCheckRadius = 0.2f; // Raduis du cercle pour tester si l'utilisateur est en contact avec le sol (Peut etre change en fonction des assets)
    public LayerMask groundLayer; // R�f�rence au layer avec lequel nous allons checker la colision
    public Transform groundCheck; // R�f�rence au GroundCheckLocation que nous avons d�ini dans le KnightPlayer
    float wallCheckRadius = 0.6f; // Raduis du cercle pour tester si l'utilisateur est en contact avec le sol (Peut etre change en fonction des assets)
    public LayerMask wallLayer; // R�f�rence au layer avec lequel nous allons checker la colision
    public Transform wallCheck; // R�f�rence au GroundCheckLocation que nous avons d�ini dans le KnightPlayer
    public float jumpPower; // Force avec laquelle nous allons projeter notre personnage en l'air
    public float VerticalVelocity;
    public float HorizontaleVelocity;
    public static PlayerController instance;
    public CapsuleCollider2D playerCollider;
    

    // Use this for initialization

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); // On utilise GetComponent car notre Rb se situe au sein du m�me objet
        playerRenderer = GetComponent<SpriteRenderer>();// R�cup�rer le component sprite renderer en dessous de cette ligne
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame


    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Attention, instance > 1 !");
            return;
        }
        instance = this;
    }



    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        walled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);

        Est_au_sol();
        deplacement();
        Saut();
        DoubleSaut();
        mur_saut();
        MAJ();    

        void Est_au_sol()
        {
            if (grounded && (Jump || DoubleJump))
            {
                //playerAnim.SetBool("Jump", false); // On defini le parametre danimation grounded a faux car nous nous aprettons a sauter
                Jump = false;
                //playerAnim.SetBool("DoubleJump", false); // On defini le parametre danimation grounded a faux car nous nous aprettons a sauter
                DoubleJump = false;
            }
            if(grounded)
            {
                canMove = true;
            }
            MAJ();
        }

        void deplacement()
        {
            if (canMove)
            {
                if (moveHorizontal > 0.01f && !facingRight)
                    Flip();
                else if (moveHorizontal < -0.01f && facingRight)
                    Flip();

                playerRB.velocity = new Vector2(moveHorizontal * maxSpeed, playerRB.velocity.y); // On utilise vector 2 car nous sommes dans un contexte 2D
                playerAnim.SetFloat("MoveSpeed", Mathf.Abs(moveHorizontal)); // Defini une valeur pour le float MoveSpeed dans notre animator

            }
            if (!canMove)
            {
               // playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y);
                playerAnim.SetFloat("MoveSpeed", 0f); // Defini une valeur pour le float MoveSpeed dans notre animator
                //Flip();
            }
        }

        void Saut()
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpPower); // On defini la velocite a 0 pour etre sur d'avoir la m�me hauteur quelque soit le contexte
                grounded = false; // On defini notre grounded a false pour garder en memoire l'etat du personnage
                Jump = true;
                MAJ();
            }


            if (Input.GetKeyDown(KeyCode.Space) && !grounded && walled)
            {
                playerRB.velocity = new Vector2(jumpPower, jumpPower ) * 1.5f; // On defini la velocite a 0 pour etre sur d'avoir la m�me hauteur quelque soit le contexte
                Jump = true;
                DoubleJump = false;
                MAJ();
            }
        }

        void DoubleSaut()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !grounded && !DoubleJump && playerRB.velocity.y < 5f)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpPower*1.25f); // On defini la velocite a 0 pour etre sur d'avoir la m�me hauteur quelque soit le contexte
                //playerRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse); // On ajoute de la force sur notre rigidbody afin de le faire s'envoler, on precise bien un forcement a impulse pour avoir toute la force d'un seul coup
                Jump = false;
                DoubleJump = true;
                canMove = true;
                MAJ();
            }
        }

        void mur_saut()
        {
            if (walled && !grounded && Mathf.Abs(playerRB.velocity.x) > 0 && Mathf.Abs(playerRB.velocity.x) <= 5)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, -0.75f);
            }
            if (Input.GetKeyDown(KeyCode.Space) && walled && !grounded)
            {
                Saut();
                if (facingRight)
                    playerRB.velocity = new Vector2(playerRB.velocity.x * -1f, playerRB.velocity.y); // On defini la velocite a 0 pour etre sur d'avoir la m�me hauteur quelque soit le contexte
                canMove = false;
                walled = false;
                Flip();
                MAJ();
            }
        }
    }
    public void Flip()
    {
        facingRight = !facingRight; // On change la valeur du boolen facing right par son contraire, repr�sentant la direction du personnage
        playerRenderer.flipX = !playerRenderer.flipX; // M�me chose ici pour que notre flipx et facingRight soient en phase
    }
    public void cantMove()
    {
        canMove = !canMove;
    }
    public void MAJ()
    {
        if (playerRB.velocity.y > 1)
            grounded = false;
        if(walled)
            Jump = false;
        playerAnim.SetBool("IsGrounded", grounded); // On utilise donc cette information pour mettre a jour note animator
        playerAnim.SetBool("DoubleJump", DoubleJump); // On utilise donc cette information pour mettre a jour note animator
        playerAnim.SetBool("Jump", Jump); // On defini le parametre danimation grounded a faux car nous nous aprettons a sauter
        playerAnim.SetBool("IsWalled", walled); // On utilise donc cette information pour mettre a jour note animator
        playerAnim.SetFloat("VerticalVelocity", playerRB.velocity.y); // On utilise donc cette information pour mettre a jour note animator
        playerAnim.SetFloat("HorizontaleVelocity", playerRB.velocity.x); // On utilise donc cette information pour mettre a jour note animator
        playerAnim.SetBool("Hit", Hit); // On utilise donc cette information pour mettre a jour note animator
    }
}
