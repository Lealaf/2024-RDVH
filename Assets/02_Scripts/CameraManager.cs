using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    public CameraPosition[] CameraPositions;
    private int index;

    public float smoothTime = 1.5f;
    public float rotationSpeed = 1.5f;

    private Vector3 m_currentVelocity = Vector3.zero;

    private Vector3 targetPos, aimPos;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        targetPos = CameraPositions[index].position;
        aimPos = CameraPositions[index].aim;
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

        if (targetPos != transform.position)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref m_currentVelocity, smoothTime);
            
            Vector3 relativePos = aimPos - transform.position;
            Quaternion q = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed * Time.deltaTime);
        }
    }

    private void getNextCamera()
    {
        index = (index + 1) % CameraPositions.Length;
        targetPos = CameraPositions[index].position;
        aimPos = CameraPositions[index].aim;
    }

    private void getPreviousCamera()
    {
        index = (index - 1) < 0 ? CameraPositions.Length-1:index-1;
        targetPos = CameraPositions[index].position;
        aimPos = CameraPositions[index].aim;
    }
}