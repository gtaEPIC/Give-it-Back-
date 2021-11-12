using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = target.transform.position;
        cameraTransform.position = new Vector3(targetPosition.x, targetPosition.y, -10);
    }
}
