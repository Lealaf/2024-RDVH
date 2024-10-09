using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Viewer : MonoBehaviour
{
    [SerializeField]
    GameObject gameObjectToRotate;

    [SerializeField] RectTransform rectRefForRotate;

    private Vector2 startingMousePosition;

    [SerializeField]
    float speed;


    private void Awake()
    {
        EventManager.Instance.SelectObject.AddListener(switchObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRotate()
    {
        startingMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void Rotate()
    {
        Vector2 anchorPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - startingMousePosition;//new Vector2(rectRefForRotate.position.x, rectRefForRotate.position.y);
        //anchorPos = new Vector2(anchorPos.x / rectRefForRotate.lossyScale.x, anchorPos.y / rectRefForRotate.lossyScale.y);

        var vecRot = new Vector3(anchorPos.y, -anchorPos.x, 0) * Time.deltaTime * speed;
        gameObjectToRotate.transform.Rotate(vecRot, Space.World);
    }


    public void switchObject(DisplayedObject displayeObject)
    {
        var newObject = displayeObject.gameObject;

        gameObjectToRotate.transform.rotation = Quaternion.Euler(0, 0, 0);
        foreach (Transform child in gameObjectToRotate.transform)
        {
            Destroy(child.gameObject);
        }
        

        var newObjectInstance = Instantiate(newObject, gameObjectToRotate.transform);
        var displayObject = newObjectInstance.GetComponent<DisplayedObject>();
        if (displayObject != null)
        {
            displayObject.ChangeMatToNormal();
        }
        newObjectInstance.transform.localPosition = Vector3.zero;

        gameObjectToRotate.transform.localPosition = new Vector3(0,0,displayObject.GetViewerDistance());

        newObjectInstance.transform.rotation = Quaternion.Euler(displayObject.GetViewerRotation());

        newObjectInstance.layer = 10;
        foreach (Transform child in newObjectInstance.transform.GetComponentsInChildren<Transform>(true))
        {
            child.gameObject.layer = 10;
        }


    }
}
