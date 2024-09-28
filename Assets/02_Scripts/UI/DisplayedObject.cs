using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class ManageObjectClick : MonoBehaviour
{
    [SerializeField]
    public CheckObjectMenu checkObjectMenu;

    private List<Material> mats = new List<Material>();
    public bool interactible = false;
    void Start()
    {
        checkObjectMenu.ReportEvent.AddListener(HideMeMaybe);
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
        checkObjectMenu.HydrateAndShow(name);
    }

    private void OnMouseOver()
    {
        if (!interactible) return;
        foreach (Material mat in mats) {
            //Debug.Log("AA"+ mat);
            mat.SetInt("_hover", 1);
        }
    }

    private void OnMouseExit()
    {
        if (!interactible) return;
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