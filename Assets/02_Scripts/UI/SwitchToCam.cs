using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SwitchToCam : MonoBehaviour
{
    [SerializeField]
    int indexCam;

    public void Switch()
    {
        CameraManager.instance.GoToCam(indexCam);
    }

    public void NextCamera()
    {
        CameraManager.instance.getNextCamera();
    }

    public void PreviousCamera()
    {
        CameraManager.instance.getPreviousCamera();
    }
}
