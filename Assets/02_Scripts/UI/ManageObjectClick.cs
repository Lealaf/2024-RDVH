using UnityEngine;
public class Clicker : MonoBehaviour
{
    [SerializeField]
    public CheckObjectMenu checkObjectMenu;
    void OnMouseDown()
    {
        checkObjectMenu.HydrateAndShow(name);
    }
}