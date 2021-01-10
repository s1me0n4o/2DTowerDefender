using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorMessageText : MonoBehaviour
{
    public static ErrorMessageText Instance { get; private set; }

    [SerializeField] private RectTransform canvastRectTransform;

    private TextMeshProUGUI _text;
    private RectTransform _backgraoundRectTransform;
    private RectTransform _mouseRectTransform;
    private RectTransform _textRectTransform; 

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        #endregion

        _text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        _backgraoundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        _textRectTransform = _text.GetComponent<RectTransform>();
        _mouseRectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void SetErrorUIPosition()
    {
        var anchoredPos = _mouseRectTransform.anchoredPosition = Input.mousePosition / canvastRectTransform.localScale.x;

        if (anchoredPos.x + _backgraoundRectTransform.rect.width > canvastRectTransform.rect.width)
        {
            anchoredPos.x = canvastRectTransform.rect.width - _backgraoundRectTransform.rect.width;
        }
        if (anchoredPos.y + _backgraoundRectTransform.rect.height > canvastRectTransform.rect.height)
        {
            anchoredPos.y = canvastRectTransform.rect.height - _backgraoundRectTransform.rect.height;
        }

        _mouseRectTransform.anchoredPosition = anchoredPos;
    }

    private void SetText(string text)
    {
        _text.text = text;
        _text.ForceMeshUpdate();

        var textSize = _text.GetRenderedValues(false);
        var padding = new Vector2(16, 16);
        _textRectTransform.sizeDelta = textSize + padding;
        _backgraoundRectTransform.sizeDelta = textSize + new Vector2(16, 8);
    }

    public IEnumerator Show(string text)
    {
        SetErrorUIPosition();

        gameObject.SetActive(true);
        SetText(text);
        yield return new WaitForSeconds(2);
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
