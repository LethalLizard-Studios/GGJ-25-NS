/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/ 

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject poppedUI;
    [SerializeField] private GameObject popEffect;
    [SerializeField] private Animation popAnimation;
    [SerializeField] private MeshRenderer bubbleRenderer;

    [Header("Ground")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask breakLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Gravity")]
    [SerializeField] private float gravityMultiplier = 1.5f;

    [Header("Move")]
    [SerializeField] private MoveAttributes moveAttributes;
    [SerializeField] private Transform model;
    [SerializeField] private Image speedFillImage;
    [SerializeField] private RectTransform speedIconImage;

    private Rigidbody _rigidbody;

    private bool _isGrounded;
    private bool _canMove = true;
    private float _currentFallSpeed = 0f;
    private float _jumpHeight = 0f;

    private Vector2 _moveInput;
    private Vector2 _lastDirection = Vector2.zero;
    private float _currentSpeed = 0f;

    private const float ROTATION_SPEED = 78.0f;
    private const float SLOW_DOWN_RATE = 6.0f;
    private const float JUMP_BOOST = 1.1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void OnEnable()
    {
        _rigidbody.useGravity = true;
        popEffect.SetActive(false);
        popAnimation.clip = popAnimation.GetClip("Anim_Unpop");
        popAnimation.Play();
        bubbleRenderer.enabled = true;
        _lastDirection = Vector2.zero;
        _moveInput = Vector2.zero;
        _currentSpeed = 0f;
        _currentFallSpeed = 0f;

        _canMove = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void SetJumpHeight(float heldTime)
    {
        if (!_isGrounded && !moveAttributes.hasDoubleJump)
        {
            return;
        }

        float jumpForce = Mathf.Sqrt(2 * moveAttributes.jumpHeight * heldTime * -Physics.gravity.y);
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        _currentSpeed *= JUMP_BOOST;
    }

    private void OnApplicationQuit()
    {
        Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
    }

    public void FanHit()
    {
        _canMove = false;
        _rigidbody.useGravity = false;
        Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
    }

    private void Died()
    {
        _canMove = false;
        Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);

        if (!poppedUI.activeSelf)
        {
            popAnimation.clip = popAnimation.GetClip("Anim_Pop-Effect");
            popAnimation.Play();
            poppedUI.SetActive(true);
            bubbleRenderer.enabled = false;
        }
    }

    public void Update()
    {
        if (!_canMove)
        {
            return;
        }

        if (Physics.CheckSphere(groundCheck.position, 1.0f, breakLayer))
        {
            Died();
        }

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

        float currentSpeedMultiplier = _currentSpeed / ((float)moveAttributes.maxSpeed);

        speedFillImage.fillAmount = currentSpeedMultiplier;
        speedIconImage.anchoredPosition = new Vector2(0.0f, currentSpeedMultiplier * 380);

        // Haptic Vibration
        if (Gamepad.current != null)
        {
            if (_isGrounded)
            {
                Gamepad.current.SetMotorSpeeds(0.05f * currentSpeedMultiplier, 0.02f * currentSpeedMultiplier);
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

        if (_jumpHeight > 0.0f)
        {
            _jumpHeight -= Time.deltaTime * 3;
        }

        if (!_canMove)
        {
            Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
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
            _currentFallSpeed = gravityMultiplier * Time.deltaTime;
            _currentFallSpeed = Mathf.Clamp(_currentFallSpeed, 0, 0.02f);
        }
    }
}
