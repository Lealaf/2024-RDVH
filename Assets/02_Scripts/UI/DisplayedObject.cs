using UnityEngine;
public class ManageObjectClick : MonoBehaviour
{
    [SerializeField]
    public CheckObjectMenu checkObjectMenu;

    private Material[] mat;
    void Start()
    {
        checkObjectMenu.ReportEvent.AddListener(HideMeMaybe);
        mat = gameObject.GetComponentInChildren<MeshRenderer>().materials;
    }
    void OnMouseDown()
    {
        checkObjectMenu.HydrateAndShow(name);
    }

    private void OnMouseOver()
    {
        Debug.Log(" => " + mat.Length);
        for(int i = 0; i < mat.Length; i++)
        {
            mat[i].SetInt("_hover", 1);
        }
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < mat.Length; i++)
        {
            mat[i].SetInt("_hover", 0);
        }
    }

    public void HideMeMaybe(string id)
    {
        if (id == name) {
            gameObject.SetActive(false);
        }
    }
}