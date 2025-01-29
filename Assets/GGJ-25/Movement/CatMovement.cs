/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/26/2025
*/

using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField] private Movement playerMovement;

    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 2f;

    private int _currentPointIndex = 0;
    private bool _canKill = false;

    private const string PLAYER_TAG = "Player";

    private void OnEnable()
    {
        _currentPointIndex = 0;
        _canKill = false;
        transform.position = points[0].position;
        _canKill = true;
    }

    private void Update()
    {
        if (points.Length == 0)
            return;

        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (_currentPointIndex >= points.Length)
        {
            gameObject.SetActive(false);
            return;
        }

        Transform targetPoint = points[_currentPointIndex];
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            _currentPointIndex++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG) && _canKill)
        {
            playerMovement.Died();
        }
    }
}
