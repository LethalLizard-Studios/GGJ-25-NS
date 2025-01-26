/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/25/2025
*/

using TMPro;
using UnityEngine;

public class RoundStats : MonoBehaviour
{
    [SerializeField] private bool hasWon = false;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] private Timer timer;

    public void OnEnable()
    {
        float time = timer.StopTimer();

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time % 1) * 100);

        if (hasWon)
        {
            int rank = 1;
            titleText.text = "You Escaped!";
            timeText.text = $"{minutes}:{seconds:00}.{milliseconds:00} (#{rank})";
        }
        else
        {
            titleText.text = "You Popped!";
            timeText.text = $"{minutes}:{seconds:00}.{milliseconds:00}";
        }
    }
}
