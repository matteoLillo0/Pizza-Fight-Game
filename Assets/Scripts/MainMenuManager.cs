using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    public Button createRoomButton;
    public Button joinRoomButton;

    private bool isConnected = false; // Flag per verificare se il client è pronto

    void Start()
    {
        Debug.Log("Connessione a Photon...");
        PhotonNetwork.ConnectUsingSettings(); // Connetti al server Photon

        // Disabilita i pulsanti all'avvio
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connesso al server master.");
        isConnected = true;

        // Abilita i pulsanti quando il client è pronto
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    public void CreateRoom()
    {
        if (!isConnected)
        {
            Debug.LogWarning("Non ancora connesso al server master. Riprovare tra qualche istante.");
            return;
        }

        Debug.Log("Creazione stanza...");
        PhotonNetwork.CreateRoom("Stanza1", new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        if (!isConnected)
        {
            Debug.LogWarning("Non ancora connesso al server master. Riprovare tra qualche istante.");
            return;
        }

        Debug.Log("Unione a stanza...");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Nessuna stanza disponibile. Creazione di una nuova stanza...");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrato nella stanza!");
        Debug.Log("Caricamento della scena MainScene...");
        PhotonNetwork.LoadLevel("MainScene"); // Carica la scena di gioco
    }

    public void QuitGame()
    {
        Debug.Log("Uscita dal gioco...");
        Application.Quit(); // Chiude l'applicazione
    }
}