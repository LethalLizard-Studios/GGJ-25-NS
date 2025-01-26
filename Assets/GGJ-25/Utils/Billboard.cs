/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/26/2025
*/

using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool onlyYAxis = false;
    [SerializeField] private Transform target;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        if (onlyYAxis)
        {
            Vector3 targetPosition = transform.position + target.rotation * Vector3.forward;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
        else
        {
            transform.LookAt(transform.position + target.rotation * Vector3.forward,
                             target.rotation * Vector3.up);
        }
    }
}
