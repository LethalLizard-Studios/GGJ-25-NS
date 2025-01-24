/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 1.0f;

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.position;

        transform.position = new Vector3(
            Mathf.Lerp(currentPosition.x, targetPosition.x, followSpeed * Time.deltaTime),
            targetPosition.y,
            Mathf.Lerp(currentPosition.z, targetPosition.z, followSpeed * Time.deltaTime)
        );
    }
}
