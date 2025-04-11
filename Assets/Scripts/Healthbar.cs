using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Healthbar : MonoBehaviourPun
{
    public Image[] hearts; // Array di cuoricini
    public Sprite fullHeart; // Sprite per i cuori pieni
    public Sprite emptyHeart; // Sprite per i cuori vuoti

    private int maxVite = 4; // Numero massimo di vite

    public void UpdateHearts(int currentLives)
    {
        // Assicurati che l'array hearts sia valido
        if (hearts == null || hearts.Length == 0)
        {
            Debug.LogError("Healthbar: Array 'hearts' non assegnato!");
            return;
        }

        // Aggiorna ogni cuore in base alle vite rimanenti
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].sprite = fullHeart; // Cuore pieno
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Cuore vuoto
            }

        }
    }
}