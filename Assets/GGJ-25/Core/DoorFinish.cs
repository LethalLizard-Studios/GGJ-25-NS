using UnityEngine;

public class DoorFinish : MonoBehaviour
{
    [SerializeField] private RoundStats roundStats;

    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PLAYER_TAG)
        {
            roundStats.gameObject.SetActive(true);
            roundStats.Won();
        }
    }
}
