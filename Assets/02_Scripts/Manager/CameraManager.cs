using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject[] cameraPositions;
    private int index;

    public float smoothTime = 1.5f;
    public float rotationSpeed = 1.5f;

    private Vector3 m_currentVelocity = Vector3.zero;

    private Vector3 targetPos;

    private Quaternion targetRotation;

    private float delta = 0.02f;


    private void Awake()
    {
        Debug.Log("Awake game manager");
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var cam in cameraPositions)
        {
            cam.gameObject.layer = 15;
        }

        if (cameraPositions != null && cameraPositions.Length != 0)
        {
            index = 0;
            targetPos = cameraPositions[index].gameObject.transform.position;
            targetRotation = cameraPositions[index].gameObject.transform.rotation;
        }
        else
        {
            targetPos = Camera.main.transform.position;
            targetRotation = Camera.main.transform.rotation;
        }
            
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

    public void getNextCamera()
    {
        if (cameraPositions == null || cameraPositions.Length == 0)
        {
            Debug.LogError("You try to go to camera but the camera list is empty or null");
            return;
        }
        GoToCam((index + 1) % cameraPositions.Length);
    }

    public void getPreviousCamera()
    {
        if (cameraPositions == null || cameraPositions.Length == 0)
        {
            Debug.LogError("You try to go to camera but the camera list is empty or null");
            return;
        }
        GoToCam((index - 1) < 0 ? cameraPositions.Length - 1 : index - 1);
    }

    public void GoToCam(int indexTarget)
    {
        if (indexTarget < cameraPositions.Length)
        {
            targetPos = cameraPositions[indexTarget].gameObject.transform.position;
            targetRotation = cameraPositions[indexTarget].gameObject.transform.rotation;
            index = indexTarget;
        }
        else
        {
            Debug.LogError("You try to go to camera:" + indexTarget + "but is out of range");
        }
    }
}