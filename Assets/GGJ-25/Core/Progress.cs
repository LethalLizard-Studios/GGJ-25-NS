using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [SerializeField] private Transform fish; 
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private Transform door;
    [SerializeField] private Transform cat;

    [SerializeField] private RectTransform catIconImage;
    [SerializeField] private Image progressImage; 

    private void FixedUpdate()
    {
        float totalDistance = Vector3.Distance(spawnPoint.position, door.position);
        float currentDistance = Vector3.Distance(fish.position, door.position);
        float progress = Mathf.Clamp01(1 - (currentDistance / totalDistance));

        float currentDistanceCat = Vector3.Distance(cat.position, door.position);
        float progressCat = Mathf.Clamp01(1 - (currentDistanceCat / totalDistance));

        progressImage.fillAmount = progress;
        catIconImage.anchoredPosition = new Vector2(progressCat * 480, 0.0f);
    }
}
