/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    [SerializeField] private float sensitivity = 4.0f;
    [SerializeField] private float pitchClampAngle = 45.0f;

    private Vector2 _lookInput;

    private float _yaw;
    private float _pitch;

    private Vector3 _startingRotation;

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        Vector3 currentRotation = transform.localEulerAngles;
        _startingRotation = currentRotation;
        _yaw = currentRotation.y;
        _pitch = currentRotation.x;
    }

    private void OnEnable()
    {
        _yaw = _startingRotation.y;
        _pitch = _startingRotation.x;

        playerBody.localEulerAngles = new Vector3(0.0f, _yaw, 0.0f);
        transform.localEulerAngles = new Vector3(_pitch, _yaw, 0.0f);
    }

    private void Update()
    {
        if (_lookInput.sqrMagnitude < 0.01f)
        {
            return;
        }

        _yaw += _lookInput.x * sensitivity * Time.deltaTime;
        _pitch -= _lookInput.y * sensitivity * Time.deltaTime;

        // Clamp vertical look range
        _pitch = Mathf.Clamp(_pitch, -pitchClampAngle, pitchClampAngle);

        playerBody.localEulerAngles = new Vector3(0.0f, _yaw, 0.0f);
        transform.localEulerAngles = new Vector3(_pitch, _yaw, 0.0f);
    }
}
