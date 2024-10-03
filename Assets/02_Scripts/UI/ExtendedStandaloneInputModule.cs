using UnityEngine;
using UnityEngine.EventSystems;

public class ExtendedStandaloneInputModule : StandaloneInputModule
{
    public static PointerEventData GetPointerEventData(int pointerId = -1)
    {
        PointerEventData eventData = null;
        if(_instance!=null) _instance.GetPointerData(pointerId, out eventData, true);
        return eventData;
    }

    public static bool IsHoveringAnyVisibleImage()
    {
        // On regarde si le curseur ne survole pas déjà un objet 2D au premier plan
        PointerEventData ped = ExtendedStandaloneInputModule.GetPointerEventData(); // Données du pointeur de la souris
        if (ped == null)
        {
            return false;
        }
        foreach (GameObject go in ped.hovered) // Tous les objets survolés par la souris
        {
            UnityEngine.UI.Image image = go.GetComponent<UnityEngine.UI.Image>();
            UnityEngine.UI.RawImage rawImage = go.GetComponent<UnityEngine.UI.RawImage>();
            if (image != null && image.color.a>0 || rawImage != null && rawImage.color.a > 0) // Si l'un d'eux est une image visible
            {
                var parentsCanvasGroup = go.GetComponentsInParent<CanvasGroup>();
                if (parentsCanvasGroup == null)
                {
                    return true;
                }

                foreach (var canvasGroup in parentsCanvasGroup)
                {
                    if (canvasGroup.alpha > 0 )
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }

    private static ExtendedStandaloneInputModule _instance;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
}