using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerControls _controls;

    [SerializeField] private float _rotationSens = 0.6f;
    [SerializeField] private float _moveSpeed = 0.3f;
    [SerializeField] private float _zoomSens = 0.01f;
    [SerializeField] private float _zoomSmooth = 0.3f;
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float _maxZoom = 10f;
    [SerializeField] AnimationCurve _zoomCurve;

    private Vector2 _moveInput;
    private float _rotationAxis;
    private bool _rotate;
    private float _zoom;
    private float _dist;

    Transform cam;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Enable();
        cam = Camera.main.transform;
        _dist = Vector3.Distance(cam.position, transform.position);
    }

    private void Update()
    {
        Inputs();

        if(Input.GetKey(KeyCode.E))
            transform.Rotate(0, _rotationSens * Time.deltaTime, 0);


        if(Input.GetKey(KeyCode.Q))
            transform.Rotate(0, -_rotationSens * Time.deltaTime, 0);
        
        if (_rotate)
        {
            transform.eulerAngles += new Vector3(0, _rotationAxis / 50, 0);
        }

        transform.position += _moveInput.y * transform.forward + _moveInput.x * transform.right;

        //cam.LookAt(transform);
        _dist -= _zoom * _zoomSens;
        _dist = Mathf.Clamp(_dist, minZoom, _maxZoom);
        float y = (_zoomCurve.Evaluate(_dist / (_maxZoom - minZoom)) * (_maxZoom - minZoom)) + minZoom;
        Vector3 target = (_dist * (cam.position - transform.position).normalized) + transform.position;
        target.y = y;
        cam.position = Vector3.Lerp(cam.position, target, _zoomSmooth);
    }

    private void Inputs()
    {
        _moveInput = _controls.Cam.Move.ReadValue<Vector2>() * _moveSpeed;
        _zoom = _controls.Cam.Zoom.ReadValue<float>();
        _rotationAxis = _controls.Cam.Rotation.ReadValue<float>() * _rotationSens;
        _rotate = _controls.Cam.RotateToggle.inProgress;
        
    }
}
