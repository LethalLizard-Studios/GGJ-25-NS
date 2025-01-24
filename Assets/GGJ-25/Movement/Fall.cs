using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fall : MonoBehaviour
{


    [Header("Gravity")]
    [SerializeField] private float gravityMultiplier = 1.5f;
    [SerializeField] private float fallAcceleration = 0.2f;

    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private float _currentFallSpeed = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (_isGrounded)
        {
            // Reset fall speed when grounded
            _currentFallSpeed = 0f;
        }
        else
        {
            // Increase fall speed progressively when airborne
            _currentFallSpeed += gravityMultiplier * fallAcceleration * Time.fixedDeltaTime;
        }

        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.y = -_currentFallSpeed;
        _rigidbody.linearVelocity = velocity;
    }
}
