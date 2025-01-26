/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/26/2025
*/

using UnityEngine;

public class RotatingFan : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private float yRotationMin = 0.0f;
    [SerializeField] private float yRotationMax = 90.0f;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float moveThreshold = 0.1f;

    private bool _isRotating = true;
    private float _rotationDirection = 1.0f;
    private Transform _player;

    private const string PLAYER_TAG = "Player";

    private void Update()
    {
        if (_isRotating)
        {
            RotateObject();
        }

        if (_player != null)
        {
            MovePlayerToTarget();
        }
    }

    private void RotateObject()
    {
        float currentYRotation = transform.localEulerAngles.y;

        if (currentYRotation > yRotationMax && currentYRotation < 180.0f)
        {
            _rotationDirection = -1.0f; // Switch to rotating back
        }
        else if (currentYRotation < yRotationMin || currentYRotation > 180.0f && currentYRotation < 360.0f - yRotationMin)
        {
            _rotationDirection = 1.0f; // Switch to rotating forward
        }

        float newYRotation = currentYRotation + _rotationDirection * rotationSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newYRotation, transform.localEulerAngles.z);
    }

    private void MovePlayerToTarget()
    {
        _player.GetComponent<Movement>().FanHit();
        _player.position = Vector3.MoveTowards(_player.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(_player.position, target.position) <= moveThreshold)
        {
            _player = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            _player = other.transform;
        }
    }
}
