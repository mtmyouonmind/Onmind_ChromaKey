using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    
    public RawImage display;
    WebCamTexture camTexture;
    public int cameraSelect = 0;
    private int currentIndex = 0;
    public string playKeyCode = "a";
    public string stopKeyCode = "s";
    public Transform webcamCamera = null;
    public Vector3 webcamCamPos = new Vector3(0, 0, 0);
    public Vector3 webcamResetPos = new Vector3(0, 0, 0);
    public Transform virtualCamera = null;
    public Vector3 virtualCamPos = new Vector3(0, 0, 0);
    public Vector3 virtualResetPos = new Vector3(0, 0, 0);
    public GameObject matchBox = null;
    public Vector3 matchBoxPos = new Vector3(0, 0, 0);
    public Vector3 matchResetPos = new Vector3(0, 0, 0);
    public Vector3 matchBoxScl= new Vector3(1, 1, 1);
    public Vector3 matchResetScl = new Vector3(1, 1, 1);

    private void Start()
    {
        CamTest();
    }
    public void Update()
    {

        if (Input.GetKeyDown(playKeyCode) )
        {
            StartCam();
        }
        if (Input.GetKeyDown(stopKeyCode))
        {
            StopCam();
        }

    }

    public void CamTest()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }
    }

    public void StartCam()
    {
        if (camTexture != null)
        {
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }

        WebCamDevice device = WebCamTexture.devices[currentIndex+cameraSelect];
        camTexture = new WebCamTexture(device.name);
        display.texture = camTexture;
        camTexture.Play();
    }
    public void StopCam()
    {
        if (camTexture != null)
        {
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }
    }


}
