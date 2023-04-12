using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    Camera sceneCamera;
   
    Vector3 desiredPosition;
    Vector3 desiredAngles;
    Vector3 initialCameraPos;
    [SerializeField] float initialFOV = 60f;
    [SerializeField] float FOVIncrement = 5f;
    [SerializeField] float FOVLimit = 100f;
    [SerializeField] Vector3 defaultEulerAngles = new Vector3(60f, 0f, 0f);
    [SerializeField] Vector3 zoomedEulerAngles= new Vector3(60f, 0f, 0f);
    [SerializeField] Vector3 zoomedPosOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] float zoomedFov = 60f;
    [SerializeField] InputAction zoomButton;
    public bool isZoomOn = false;
    bool isZoomingToSpecificPoint = false;
    [SerializeField] Vector3 zoomOffsetForABTest;
    [SerializeField] float[] timers;

    private void Awake()
    {
        sceneCamera = Camera.main;
        initialCameraPos = transform.position;
    }

    private void OnEnable()
    {
        zoomButton.Enable();
    }

    private void OnDisable()
    {
        zoomButton.Disable();
    }

    private void Start()
    {
        zoomButton.performed += (ctx) =>
        {
            isZoomOn = !isZoomOn;
        };
    }

    void LateUpdate()
    {
        float desiredFOV;
        if (isZoomingToSpecificPoint)
        {
            return;
        }
       
        
        desiredPosition = target.position + offset;
        desiredAngles = defaultEulerAngles;
      
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        Vector3 smoothedAngle = Vector3.Lerp(transform.eulerAngles, desiredAngles, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.eulerAngles = smoothedAngle;
    }

   }
