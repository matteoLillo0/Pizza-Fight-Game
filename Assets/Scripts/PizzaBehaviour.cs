using UnityEngine;

public class PizzaBehaviour : MonoBehaviour
{
    private PlayerController owner; // Proprietario della pizza

    public void SetOwner(PlayerController player)
    {
        owner = player; // Assegna il proprietario
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // Distruggi la pizza se colpisce il terreno
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            // Applica il danno solo al giocatore avversario
            if (player != null && player != owner)
            {
                Debug.Log("Pizza ha colpito un giocatore!");

                // Ignora le forze fisiche durante la collisione
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());

                player.TakeDamage(); // Applica il danno
            }

            Destroy(gameObject); // Distruggi la pizza dopo aver colpito un giocatore
        }
    }
}