/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/25/2025
*/

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoundStats : MonoBehaviour
{
    [SerializeField] private UserManager userManager;
    [SerializeField] private Timer timer;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI timeText;

    public void Won()
    {
        float time = timer.StopTimer();

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time % 1) * 100);

        userManager.UpdateUserInfo(time);
        int rank = userManager.GetPosition(time) - 1;

        titleText.text = "You Escaped!";
        timeText.text = $"{minutes}:{seconds:00}.{milliseconds:00} (#{rank})";
    }

    public void OnEnable()
    {
        float time = timer.StopTimer();

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time % 1) * 100);

        titleText.text = "You Popped :(";
        timeText.text = $"{minutes}:{seconds:00}.{milliseconds:00}";

        Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
    }
}
