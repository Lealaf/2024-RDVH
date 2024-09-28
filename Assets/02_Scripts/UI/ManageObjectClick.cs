using UnityEngine;
public class ManageObjectClick : MonoBehaviour
{
    [SerializeField]
    public CheckObjectMenu checkObjectMenu;
    void Start()
    {
        checkObjectMenu.ReportEvent.AddListener(HideMeMaybe);
    }
    void OnMouseDown()
    {
        checkObjectMenu.HydrateAndShow(name);
    }
    public void HideMeMaybe(string id)
    {
        if (id == name) {
            gameObject.SetActive(false);
        }
    }
}