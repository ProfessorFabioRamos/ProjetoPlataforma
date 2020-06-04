using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 lastCameraPosition;
    public float parallaxSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement*parallaxSpeed;
        lastCameraPosition = cameraTransform.position;
    }
}
