using UnityEngine;
public class ManageObjectClick : MonoBehaviour
{
    [SerializeField]
    public CheckObjectMenu checkObjectMenu;

    private Material mat;
    void Start()
    {
        checkObjectMenu.ReportEvent.AddListener(HideMeMaybe);
        mat = gameObject.GetComponent<MeshRenderer>().material;
        Debug.Log("mat = "+ mat+"  "+ gameObject.GetComponent<MeshRenderer>());
    }
    void OnMouseDown()
    {
        checkObjectMenu.HydrateAndShow(name);
    }

    private void OnMouseOver()
    {
        mat.SetInt("_hover", 1);
    }

    private void OnMouseExit()
    {

        mat.SetInt("_hover", 0);
    }

    public void HideMeMaybe(string id)
    {
        if (id == name) {
            gameObject.SetActive(false);
        }
    }
}