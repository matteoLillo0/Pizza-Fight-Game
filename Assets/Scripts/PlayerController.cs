/*
 * Matteo Angiolillo 4�H 2024-03-22 Script per gestire movimento giocatore
*/

using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    // variabili giocatore

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGrounded;
    public int lives = 4;

    // riferimenti e variabili alle pizze da sparare

    public GameObject pizzaPrefab; // Prefab della pizza
    public float shootForce = 10f; // Forza di lancio
    public Transform shootPoint;  // Punto da cui esce la pizza

    // riferimento alla healtbar

    public Healthbar healthBar; // Riferimento alla health bar

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // prende il riferimento al componente
        sr = GetComponent<SpriteRenderer>(); // prnde il riferimento al render
       
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal"); // prende il movimento orizzontale 
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY); // assegna il vettore alla propriet� velocit� del nostro rigidbody

        // per flippare il player

        if (moveInput > 0) // se va a destra
        {
            sr.flipX = false;
            shootPoint.transform.localPosition = new Vector2(0.5f, shootPoint.transform.localPosition.y); // per sparare a destra
        }
        else if (moveInput < 0) // sinistra
        {
            sr.flipX = true; // propriet� dello sprite render
            shootPoint.transform.localPosition = new Vector2(-0.5f, shootPoint.transform.localPosition.y); // per sparare a sinitra
        }

        // salto

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        // sparo

        if (Input.GetKeyDown(KeyCode.F))
        {
            Spara();
        }
    }

    void Spara() // funzione base per sparare le pizze
    {
        
        GameObject pizza = Instantiate(pizzaPrefab, shootPoint.position, shootPoint.rotation); // istanziare il prefab

        Vector2 direction = !sr.flipX ? Vector2.right : Vector2.left;         // Direzione del giocatore (destra o sinistra) 

        // Applica forza nella direzione corrente

        Rigidbody2D rb = pizza.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(direction.x * shootForce, 1f); // assegna un vettore per la forza in x e in y

    }


    public void AssignHealthBar(Healthbar assignedHealthBar)
    {
        healthBar = assignedHealthBar; // Assegna la health bar
        
        if (healthBar != null)
        {
            healthBar.UpdateHearts(lives); // Inizializza la health bar
        }
    }

    public void TakeDamage()
    {
        lives--;
        Debug.Log("Vite rimaste: " + lives);

        // Aggiorna la health bar
        if (healthBar != null)
        {
            healthBar.UpdateHearts(lives);
        }

        if (lives <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Debug.Log("Giocatore eliminato");
        Destroy(gameObject);
    }


    #region Collisioni

    // per gestire le collisioni con i tag -> Ground | Wall

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        
    }

    #endregion

}

