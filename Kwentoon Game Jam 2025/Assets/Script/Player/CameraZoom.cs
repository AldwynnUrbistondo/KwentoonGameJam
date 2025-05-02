using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    private Cinemachine.CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float newSize = vcam.m_Lens.OrthographicSize - scroll * zoomSpeed;
            vcam.m_Lens.OrthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}
