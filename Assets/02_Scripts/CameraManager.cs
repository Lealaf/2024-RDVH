using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameraPositions;
    private int index;

    public float smoothTime = 1.5f;
    public float rotationSpeed = 1.5f;

    private Vector3 m_currentVelocity = Vector3.zero;

    private Vector3 targetPos;

    private Quaternion targetRotation;

    private float delta = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var cam in cameraPositions)
        {
            cam.gameObject.layer = 15;
        }
        index = 0;
        targetPos = cameraPositions[index].gameObject.transform.position;
        targetRotation = cameraPositions[index].gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            getNextCamera();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            getPreviousCamera();
        }
        //if (targetPos != Camera.main.transform.position)
        if (Vector3.Distance(targetPos, Camera.main.transform.position) > delta || Quaternion.Angle(transform.rotation, targetRotation) > delta)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPos, ref m_currentVelocity, smoothTime);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void getNextCamera()
    {
        index = (index + 1) % cameraPositions.Length;
        targetPos = cameraPositions[index].gameObject.transform.position;
        targetRotation = cameraPositions[index].gameObject.transform.rotation;
    }

    private void getPreviousCamera()
    {
        index = (index - 1) < 0 ? cameraPositions.Length-1:index-1;
        targetPos = cameraPositions[index].gameObject.transform.position;
        targetRotation = cameraPositions[index].gameObject.transform.rotation;
    }
}