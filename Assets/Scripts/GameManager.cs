using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Healthbar player1HealthBar; // Health bar del giocatore 1
    public Healthbar player2HealthBar; // Health bar del giocatore 2
    public static Healthbar healthBarNemico;

    void Start()
    {
        Debug.Log("GameManager: Start() eseguito.");
        // Non chiama CreatePlayer() qui. Aspetta che il client entri in una stanza.
        CreatePlayer();
        GameManager.healthBarNemico = player2HealthBar; 
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("GameManager: Entrato nella stanza!");
    }

    void CreatePlayer()
    {
        Debug.Log("GameManager: Creazione del giocatore...");
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0, 0); // Posizione casuale

        // Verifica che PhotonNetwork.Instantiate venga eseguito
        Debug.Log("GameManager: Tentativo di istanziare il prefab 'PlayerPrefab'.");
        GameObject player = PhotonNetwork.Instantiate("PlayerPrefab", spawnPosition, Quaternion.identity);

        if (player == null)
        {
            Debug.LogError("GameManager: Errore: Il giocatore non è stato istanziato!");
            return;
        }

        Debug.Log("GameManager: Giocatore istanziato correttamente.");
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("GameManager: Errore: Il componente PlayerController non è presente sul prefab del giocatore.");
            return;
        }

        Debug.Log("GameManager: Assegnazione health bar...");

        if (player.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("GameManager: Assegnazione health bar locale.");
            playerController.AssignHealthBar(player1HealthBar); // Assegna la health bar locale
        }
        else
        {
            
            Debug.Log("GameManager: Assegnazione health bar remota.");
            playerController.AssignHealthBar(player2HealthBar); // Assegnazione health bar remota
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("Un giocatore ha lasciato la stanza: " + otherPlayer.NickName);

        // Trova tutti i giocatori rimasti nella stanza
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PhotonView playerPhotonView = player.GetComponent<PhotonView>();

            // Se il MasterClient non è il proprietario, assume il controllo
            if (!playerPhotonView.IsMine && PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Il MasterClient assume il controllo del giocatore: " + playerPhotonView.ViewID);
                playerPhotonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
        }
    }
}