/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/

using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    private Vector3 _originalPosition;

    private const float CHANGE_SPEED = 12.0f;
    private const float MIN_DISTANCE_TO_PLAYER = 1.5f;
    private const float CHECK_RADIUS = 0.5f;

    private void Awake()
    {
        _originalPosition = transform.localPosition;
    }

    private void Update()
    {
        bool isInWall = Physics.CheckSphere(transform.position, CHECK_RADIUS);

        if (isInWall)
        {
            Vector3 directionToPlayer = (playerBody.position - transform.position).normalized;
            Vector3 closerPosition = playerBody.position - directionToPlayer * MIN_DISTANCE_TO_PLAYER;

            transform.position = Vector3.Lerp(transform.position, closerPosition, CHANGE_SPEED * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                _originalPosition, CHANGE_SPEED * Time.deltaTime);
        }
    }
}
