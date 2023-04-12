using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool disablePlayerControl = false;
    CharacterController charController;
    [SerializeField] InputAction joystick;
    [SerializeField] float speed = 1f;
    [SerializeField] float turnSmoothCoef = 0.1f;
    float speedIncreaseCoefficient = 0.3f;
    public int SpeedUpgrade { get; set; }
    private float turnSmoothVelocity;
    public float vertical = 0f;
    public float horizontal = 0f;
    Vector3 direction;
    Vector3 gravityForce;
    Vector3 currentGravityForce = Vector3.zero;
    [SerializeField] float ABTestExtraSpeed = 1.5f;
    public float PlayerSpeed
    {
        get
        {
            return direction.magnitude;
        }
    }


    private void Awake()
    {
       
        gravityForce = Physics.gravity;
        charController = GetComponent<CharacterController>();
        SpeedUpgrade = PlayerPrefs.GetInt("Speed", 0);
    }

    private void OnEnable()
    {
        joystick.Enable();
    }

    private void OnDisable()
    {
        joystick.Disable();
    }

    private void Update()
    {
        ControlCharacter();
        HandleGravity();
    }

    private void ControlCharacter()
    {
        horizontal = joystick.ReadValue<Vector2>().x;
        vertical = joystick.ReadValue<Vector2>().y;
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (disablePlayerControl)
        {
            horizontal = 0f;
            vertical = 0f;
            direction = Vector3.zero;
        }
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = (Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg) + 20f;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothCoef);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            charController.Move(moveDir.normalized * (ABTestExtraSpeed + speed + (SpeedUpgrade * speedIncreaseCoefficient)) * Time.deltaTime);
        }
    }
    public void UpdateSpeed()
    {
        SpeedUpgrade = PlayerPrefs.GetInt("Speed", 0);

    }
    private void HandleGravity()
    {
        if (!charController.isGrounded)
        {
            currentGravityForce += gravityForce * Time.deltaTime;
            charController.Move(currentGravityForce * Time.deltaTime);
        }
        else
        {
            currentGravityForce = Vector3.zero;
        }
    }
}
