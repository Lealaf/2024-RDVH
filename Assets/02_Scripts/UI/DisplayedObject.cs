using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class ManageObjectClick : MonoBehaviour
{
    private List<Material> mats = new List<Material>();
    public bool interactible = false;
    void Start()
    {
        EventManager.Instance.CollectObject.AddListener(HideMeMaybe);
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            foreach (Material mat in meshRenderer.materials)
            {
                mats.Add(mat);
            }
        }
    }
    void OnMouseDown()
    {
        if (!interactible) return;
        ChangeMatToNormal();
        EventManager.Instance.SelectObject.Invoke(gameObject);
    }

    private void OnMouseOver()
    {
        if (!interactible) return;
        foreach (Material mat in mats) {
            mat.SetInt("_hover", 1);
        }
    }

    private void OnMouseExit()
    {
        if (!interactible) return;
        ChangeMatToNormal();
    }

    public void ChangeMatToNormal()
    {
        foreach (Material mat in mats)
        {
            mat.SetInt("_hover", 0);
        }
    }

    public void HideMeMaybe(string id)
    {
        if (id == name) {
            gameObject.SetActive(false);
        }
    }
}