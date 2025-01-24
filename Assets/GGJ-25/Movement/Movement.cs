/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/ 

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private MoveAttributes moveAttributes;
    [SerializeField] private Transform model;

    private Rigidbody _rigidbody;

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

        Vector3 movement = new Vector3(_moveInput.x, 0.0f, _moveInput.y).normalized;
        transform.position += movement * moveAttributes.baseSpeed * Time.deltaTime;

        if (movement != Vector3.zero)
        {
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movement);
            float rotationAmount = ROTATION_SPEED * Time.deltaTime;
            model.Rotate(rotationAxis, rotationAmount, Space.World);
        }
    }
}
