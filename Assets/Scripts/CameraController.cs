using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//maybe change the offset's y value instead of the currentZoom
public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float characterHeight = Constants.CHARACTER_HEIGHT;

    public float zoomSpeed = 5f;
    public Vector2 zoomPositionRange = new Vector2(5, 10);

    public float rotationSpeed = 100f;

    private float currentZoom = 5f;
    private float currentRotation = 0f;

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, zoomPositionRange.x, zoomPositionRange.y);

        currentRotation -= Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * characterHeight);

        transform.RotateAround(target.position, Vector3.up, currentRotation);
    }
}
