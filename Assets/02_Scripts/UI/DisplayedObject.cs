using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayedObject : MonoBehaviour
{
    public AudioClip pickupNoise;
    private List<Material> mats = new List<Material>();
    public bool interactible = false;
    public string objectName = NOT_DEFINED;
    const string NOT_DEFINED = "";

    public Vector3 rotation = Vector3.zero;

    void Start()
    {
        EventManager.Instance.CollectObject.AddListener(HideMeMaybe);
        EventManager.Instance.ShowAllObjects.AddListener(Show);
        getMats();
    }
    void OnMouseDown()
    {
        if (!interactible || ExtendedStandaloneInputModule.IsHoveringAnyVisibleUI()) return;
        ChangeMatToNormal();
        AudioManager.Instance.takeObjectNoise(pickupNoise);
        EventManager.Instance.SelectObject.Invoke(this);
    }

    private void OnMouseOver()
    {
        if (!interactible || ExtendedStandaloneInputModule.IsHoveringAnyVisibleUI()) return;
        foreach (Material mat in mats) {
            mat.SetInt("_hover", 1);
        }
    }

    private void OnMouseExit()
    {
        if (!interactible) return;
        ChangeMatToNormal();
    }

    public void getMats()
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            foreach (Material mat in meshRenderer.materials)
            {
                mats.Add(mat);
            }
        }
    }

    public void ChangeMatToNormal()
    {
        if(mats.Count == 0) getMats();
        foreach (Material mat in mats)
        {
            mat.SetInt("_hover", 0);
        }
    }

    public void HideMeMaybe(string id)
    {
        if (!interactible) return;
        if (objectName != NOT_DEFINED && id == objectName) {
            gameObject.SetActive(false);
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // C'est laid mais ca devrais fonctionner pour l'instant.
    // A voir si il est possible de le calculer en fonction des tailles de mesh
    public Vector3 GetViewerRotation()
    {
        return rotation;
    }
}