using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerControls controls;

    [SerializeField] private float rotationSens = 0.6f;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private float zoomSens = 0.01f;
    [SerializeField] private float zoomSmooth = 0.3f;
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] AnimationCurve zoomCurve;
    private Vector2 moveInput;
    private float rotationAxis;
    private bool rotate;
    private float zoom;
    private float dist;

    Transform cam;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();
        cam = Camera.main.transform;
        dist = Vector3.Distance(cam.position, transform.position);
    }

    void Update()
    {
        Inputs();

        if (rotate)
        {
            transform.eulerAngles += new Vector3(0, rotationAxis, 0);
        }

        transform.position += moveInput.y * transform.forward + moveInput.x * transform.right;

        cam.LookAt(transform);
        dist -= zoom * zoomSens;
        dist = Mathf.Clamp(dist, minZoom, maxZoom);
        float y = (zoomCurve.Evaluate(dist / (maxZoom - minZoom)) * (maxZoom - minZoom)) + minZoom;
        Vector3 target = (dist * (cam.position - transform.position).normalized) + transform.position;
        target.y = y;
        cam.position = Vector3.Lerp(cam.position, target, zoomSmooth * Time.deltaTime);
    }

    void Inputs()
    {
        moveInput = controls.Cam.Move.ReadValue<Vector2>() * moveSpeed;
        rotationAxis = controls.Cam.Rotate.ReadValue<float>() * rotationSens;
        rotate = controls.Cam.RotateToggle.inProgress;
        zoom = controls.Cam.Zoom.ReadValue<float>();
    }
}
