using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    
    public Image[] hearts; // array di cuoricini
    public Sprite fullHeart; // sprite per i pieni
    public Sprite emptyHeart; // sprite per i vuoti   

    private int maxVite = 4;


    // updata i cuori
    public void UpdateHearts(int currentLives) 
    {
        Debug.Log("Updating hearts. Current lives: " + currentLives);

        for (int i = 0; i < hearts.Length; i++)
        {
            // es: se hai 3 vite, i cicla fino a 3 e riempie, poi non è pù piccolo e quindi assegna i vuoti
            
            if (i < currentLives) hearts[i].sprite = fullHeart; // Cuore pieno

            else hearts[i].sprite = emptyHeart; // Cuore vuoto

            // Attiva/disattiva il cuoricino visivamente in caso abbia vite extra

            hearts[i].enabled = (i < maxVite);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
