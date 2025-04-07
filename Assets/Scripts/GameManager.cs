using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab del giocatore STOCK
    public Healthbar player1HealthBar; // Health bar del giocatore 1
    public Healthbar player2HealthBar; // Health bar del giocatore 2 <- QUESTO LO RICEVO DALL'ONLINE
    
    void Start()
    {
        // Istanzia il giocatore 1
        GameObject player1 = Instantiate(playerPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
        PlayerController player1Controller = player1.GetComponent<PlayerController>();
        player1Controller.AssignHealthBar(player1HealthBar);

    }
}
