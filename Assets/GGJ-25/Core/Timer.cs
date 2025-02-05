/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/24/2025
*/

using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float _timer;
    private bool _isRunning;

    private void Start()
    {
        _timer = 0.0f;
        StartTimer();
    }

    private void Update()
    {
        if (_isRunning)
        {
            _timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(_timer / 60f);
            int seconds = Mathf.FloorToInt(_timer % 60);
            int milliseconds = Mathf.FloorToInt((_timer % 1) * 100);

            timerText.text = $"{minutes}:{seconds:00}.{milliseconds:00}";
        }
    }

    public void StartTimer()
    {
        _isRunning = true;
    }

    public float StopTimer()
    {
        _isRunning = false;
        return _timer;
    }

    public void ResetTimer()
    {
        _timer = 0f;
        timerText.text = "0.00";
    }
}
