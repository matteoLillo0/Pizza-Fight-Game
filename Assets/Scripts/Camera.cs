using UnityEngine;

public class DynamicCameraScaler : MonoBehaviour
{
    public float designWidth = 1920f; // Larghezza di progettazione
    public float designHeight = 1080f; // Altezza di progettazione

    public float gameWorldWidth = 20f; // Larghezza del mondo di gioco (in unità Unity)
    public float gameWorldHeight = 10f; // Altezza del mondo di gioco (in unità Unity)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();

        if (mainCamera == null)
        {
            Debug.LogError("Errore: Nessuna telecamera trovata!");
            return;
        }

        ScaleCamera();
    }

    void Update()
    {
        // Aggiorna la scala della telecamera se la risoluzione cambia
        ScaleCamera();
    }

    void ScaleCamera()
    {
        // Calcola il rapporto di aspetto di progettazione
        float designAspect = designWidth / designHeight;

        // Calcola il rapporto di aspetto corrente dello schermo
        float currentAspect = (float)Screen.width / Screen.height;

        // Calcola la scala della telecamera in base alle dimensioni del mondo di gioco
        if (currentAspect > designAspect)
        {
            // Schermo più largo: la scala dipende dall'altezza del mondo di gioco
            mainCamera.orthographicSize = gameWorldHeight / 2f;
        }
        else
        {
            // Schermo più stretto: la scala dipende dalla larghezza del mondo di gioco
            mainCamera.orthographicSize = gameWorldWidth / (2f * currentAspect);
        }
    }
}