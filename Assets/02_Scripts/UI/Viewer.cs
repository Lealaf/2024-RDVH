using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    [SerializeField]
    GameObject gameObjectToRotate;

    [SerializeField] RectTransform rectRefForRotate;



    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Rotate()
    {
        Vector2 anchorPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y) - new Vector2(rectRefForRotate.position.x, rectRefForRotate.position.y);
        anchorPos = new Vector2(anchorPos.x / rectRefForRotate.lossyScale.x, anchorPos.y / rectRefForRotate.lossyScale.y);

        var vecRot = new Vector3(anchorPos.y, -anchorPos.x, 0) * Time.deltaTime * speed;
        gameObjectToRotate.transform.Rotate(vecRot, Space.World);
    }
}
