using UnityEngine;

public class PizzaBehaviour : MonoBehaviour
{
    public float lifeTime = 3f; // tempo di vita in secondi

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {

    }

    // quando collide con un player o qualcosa
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground")) Destroy(gameObject);
        else if (collision.gameObject.CompareTag("Player"))
        {
            

            // toglie la vita al player

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(); // Riduci le vite del giocatore colpito
            }

            Destroy(gameObject); // Distruggi la pizza dopo aver colpito
        }


    }

    


}
