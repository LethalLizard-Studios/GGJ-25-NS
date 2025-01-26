/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/25/2025
*/

using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    private void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.World);
    }
}
