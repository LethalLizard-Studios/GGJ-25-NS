/* Programmed by: Leland Carter & Sarah Nguyen
-- @Date: 1/25/2025
*/

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryView : MonoBehaviour
{
    [SerializeField] private Texture2D[] fishTextures;

    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI placeText;
    [SerializeField] private RawImage fishIcon;

    public void Initialize(string username, float time, int place, int fishIndex)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time % 1) * 100);

        timeText.text = $"{minutes}:{seconds:00}.{milliseconds:00}";

        usernameText.text = username;
        placeText.text = place.ToString();
        fishIcon.texture = fishTextures[fishIndex];
    }
}
