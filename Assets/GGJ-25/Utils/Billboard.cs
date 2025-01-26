/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/26/2025
*/

using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool onlyYAxis = false;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (_mainCamera == null)
        {
            return;
        }

        if (onlyYAxis)
        {
            Vector3 targetPosition = transform.position + _mainCamera.transform.rotation * Vector3.forward;
            targetPosition.y = transform.position.y;
            transform.LookAt(targetPosition);
        }
        else
        {
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                             _mainCamera.transform.rotation * Vector3.up);
        }
    }
}
