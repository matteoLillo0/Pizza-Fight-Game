using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerController : MonoBehaviourPun
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGrounded;
    public int lives = 4;

    public GameObject pizzaPrefab; // Prefab della pizza
    public float shootForce = 10f; // Forza di lancio
    public Transform shootPoint;  // Punto da cui esce la pizza

    public Healthbar healthBar; // Riferimento alla health bar
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (photonView.IsMine)
        {
            Debug.Log("Questo è il mio giocatore.");
        }
        else
        {
            Debug.Log("Questo è un giocatore remoto.");
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return; // Controlla solo il giocatore locale

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0)
        {
            sr.flipX = false;
            shootPoint.transform.localPosition = new Vector2(0.5f, shootPoint.transform.localPosition.y);
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
            shootPoint.transform.localPosition = new Vector2(-0.5f, shootPoint.transform.localPosition.y);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Spara();
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return; // Controlla solo il giocatore locale

        // Blocca il movimento indesiderato quando il giocatore è fermo
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && isGrounded)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void Spara()
    {
        if (pizzaPrefab == null)
        {
            Debug.LogError("Errore: PizzaPrefab non assegnato!");
            return;
        }

        // Calcola la direzione della pizza
        Vector3 direction = !sr.flipX ? Vector3.right : Vector3.left;

        // Invia una RPC per istanziare la pizza su tutti i client
        photonView.RPC("InstantiatePizza", RpcTarget.All, shootPoint.position, shootPoint.rotation, direction);
    }

    [PunRPC]
    void InstantiatePizza(Vector3 position, Quaternion rotation, Vector3 direction)
    {
        // Istanzia la pizza
        GameObject pizza = Instantiate(pizzaPrefab, position, rotation);

        // Ottieni il Rigidbody2D della pizza
        Rigidbody2D rb = pizza.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Errore: La pizza non ha un componente Rigidbody2D.");
            Destroy(pizza);
            return;
        }

        // Imposta la velocità della pizza
        rb.linearVelocity = direction * shootForce;

        PizzaBehaviour pizzaBehaviour = pizza.GetComponent<PizzaBehaviour>();
        if (pizzaBehaviour != null)
        {
            pizzaBehaviour.SetOwner(this); // Assegna il proprietario
        }

        // Distruggi la pizza dopo un certo tempo
        Destroy(pizza, 2f);
    }
        
    public void AssignHealthBar(Healthbar assignedHealthBar)
    {
        healthBar = assignedHealthBar;
        
        if (healthBar != null)
        {
            Debug.Log("Health bar assegnata correttamente.");
            healthBar.UpdateHearts(lives); // Aggiorna immediatamente la health bar con le vite correnti
            
        }
        else
        {
            Debug.LogError("Errore: Health bar non assegnata!");
        }
    }

    [PunRPC]
    void UpdateLives(int playerId, int newLives)
    {
        GameManager.healthBarNemico.UpdateHearts(newLives);        
         
        if (newLives == 0) 
        {
            Debug.Log("Win panel");
            GameManager.ShowWinPanel();
            

        }

        
    }

    public void TakeDamage()
    {
        if (!photonView.IsMine) return; // Applica il danno solo al giocatore locale
               
        lives--;

        if (healthBar != null)
        {
            Debug.Log("Aggiornamento health bar...");
            healthBar.UpdateHearts(lives); // Aggiorna la health bar
        }

        Debug.Log("Vite rimaste: " + lives);
        photonView.RPC("UpdateLives", RpcTarget.Others, photonView.OwnerActorNr, lives);
        
        if (lives <= 0)
        {
            Die(); // Elimina il giocatore se le vite sono esaurite
            Debug.Log("Hai perso");
            GameManager.ShowLosePanel();
        }     
    }      

    void Die()
    {
        Debug.Log("Giocatore eliminato");

        // Forza il trasferimento del controllo al MasterClient se necessario
        if (!photonView.IsMine && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Trasferimento del controllo al MasterClient...");
            photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }

        // Solo il proprietario o il MasterClient può distruggere il giocatore
        if (photonView.IsMine || PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Eliminazione del giocatore...");
            PhotonNetwork.Destroy(gameObject); // Rimuovi il giocatore dalla scena
        }
        else
        {
            Debug.LogWarning("Tentativo di eliminare il giocatore fallito: Nessun permesso.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}