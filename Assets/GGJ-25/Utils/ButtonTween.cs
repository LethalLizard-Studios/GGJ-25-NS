using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(AudioSource))]
public class ButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private AudioClip hover;
    [SerializeField] private AudioClip pressed;

    private RectTransform _rectTransform;
    private Image _image;
    private TextMeshProUGUI _text;
    private AudioSource _source;

    private Color _originalColor;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _source = GetComponent<AudioSource>();

        if (TryGetComponent<Image>(out _image))
            _originalColor = _image.color;
        else if (TryGetComponent<TextMeshProUGUI>(out _text))
            _originalColor = _text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _source.clip = hover;
        _source.Play();

        _rectTransform.DOScale(Vector3.one * 1.05f, 0.1f).SetEase(Ease.OutQuad).SetUpdate(true);

        Color color = new Color(_originalColor.r + 0.05f, _originalColor.g + 0.05f, _originalColor.b + 0.05f, _originalColor.a);

        if (_image != null)
            _image.DOColor(color, 0.1f).SetUpdate(true);
        else if (_text != null)
            _text.DOColor(color, 0.1f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce).SetUpdate(true);

        if (_image != null)
            _image.DOColor(_originalColor, 0.3f).SetUpdate(true);
        else if (_text != null)
            _text.DOColor(_originalColor, 0.3f).SetUpdate(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _source.clip = pressed;
        _source.Play();
    }
}