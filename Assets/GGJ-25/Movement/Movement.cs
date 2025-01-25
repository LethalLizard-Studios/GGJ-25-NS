/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/ 

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Gravity")]
    [SerializeField] private float gravityMultiplier = 1.5f;

    [Header("Move")]
    [SerializeField] private MoveAttributes moveAttributes;
    [SerializeField] private Transform model;

    private Rigidbody _rigidbody;

    private bool _isGrounded;
    private float _currentFallSpeed = 0f;

    private Vector2 _moveInput;
    private Vector2 _lastDirection = Vector2.zero;
    private float _currentSpeed = 0f;

    private const float ROTATION_SPEED = 78.0f;
    private const float SLOW_DOWN_RATE = 8.0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void Update()
    {
        if (_moveInput.sqrMagnitude < 0.01f)
        {
            _currentSpeed -= SLOW_DOWN_RATE * Time.deltaTime;
        }
        else
        {
            _lastDirection = _moveInput;
        }

        _currentSpeed += moveAttributes.speedUpRate * Time.deltaTime;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0.0f, moveAttributes.maxSpeed);

        // Haptic Vibration
        if (Gamepad.current != null)
        {
            float vibrationMultiplier = _currentSpeed / (float)moveAttributes.maxSpeed;

            if (_isGrounded)
            {
                Gamepad.current.SetMotorSpeeds(0.05f * vibrationMultiplier, 0.02f * vibrationMultiplier);
            }
            else
            {
                Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
            }
        }

        Falling();

        Vector3 movement = new Vector3(_lastDirection.x, 0.0f, _lastDirection.y);
        Vector3 movementDirection = transform.TransformDirection(movement).normalized;

        movementDirection.y = -_currentFallSpeed;
        movementDirection = movementDirection.normalized;

        transform.position += movementDirection * _currentSpeed * Time.deltaTime;

        if (movementDirection != Vector3.zero)
        {
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movementDirection);
            float rotationAmount = ROTATION_SPEED * _currentSpeed * Time.deltaTime;
            model.Rotate(rotationAxis, rotationAmount, Space.World);
        }
    }

    private void Falling()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (_isGrounded)
        {
            _currentFallSpeed = 0f;
        }
        else
        {
            Debug.Log("Not Grounded");
            _currentFallSpeed = gravityMultiplier * Time.deltaTime;
            _currentFallSpeed = Mathf.Clamp(_currentFallSpeed, 0, 0.02f);
        }
    }
}
