using UnityEngine;

public class Letterbox : MonoBehaviour
{
    public float targetAspect = 16f / 9f; // Rapporto di aspetto desiderato (es. 16:9)

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();

        if (mainCamera == null)
        {
            Debug.LogError("Errore: Nessuna telecamera trovata!");
            return;
        }

        ApplyLetterbox();
    }

    void ApplyLetterbox()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect > targetAspect)
        {
            // Schermo più largo del rapporto di aspetto desiderato
            // Aggiungi barre nere sopra e sotto
            float scaleHeight = targetAspect / currentAspect;
            Rect rect = mainCamera.rect;
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
            rect.width = 1f;
            rect.x = 0f;
            mainCamera.rect = rect;
        }
        else
        {
            // Schermo più stretto del rapporto di aspetto desiderato
            // Aggiungi barre nere ai lati
            float scaleWidth = currentAspect / targetAspect;
            Rect rect = mainCamera.rect;
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
            rect.height = 1f;
            rect.y = 0f;
            mainCamera.rect = rect;
        }
    }
}