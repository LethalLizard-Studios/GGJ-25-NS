using UnityEngine;
using UnityEngine.UI;

public class AttributePreview : MonoBehaviour
{
    [SerializeField] private Image[] circles;

    [SerializeField] private Color highlighted;
    [SerializeField] private Color normal;

    public void SetValue(int value)
    {
        for (int i = 0; i < circles.Length; i++)
        {
            if (i < value)
            {
                circles[i].color = highlighted;
            }
            else
            {
                circles[i].color = normal;
            }    
        }
    }
}
