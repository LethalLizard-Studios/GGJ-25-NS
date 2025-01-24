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
    [SerializeField] private float fallAcceleration = 0.2f;

    [Header("Move")]
    [SerializeField] private MoveAttributes moveAttributes;
    [SerializeField] private Transform model;

    private Rigidbody _rigidbody;

    private bool _isGrounded;
    private float _currentFallSpeed = 0f;
    private Vector2 _moveInput;

    private const float ROTATION_SPEED = 360.0f;

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
        if (_moveInput == null)
        {
            return;
        }

        Falling();

        Vector3 movement = new Vector3(_moveInput.x, -_currentFallSpeed, _moveInput.y).normalized;
        transform.position += movement * moveAttributes.baseSpeed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movement);
            float rotationAmount = ROTATION_SPEED * Time.deltaTime;
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
            _currentFallSpeed = gravityMultiplier * fallAcceleration * Time.fixedDeltaTime;
            _currentFallSpeed = Mathf.Clamp(_currentFallSpeed, 0, 2);
        }
    }
}
