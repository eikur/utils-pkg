using TMPro;
using UnityEngine;


public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string _key;
    TMP_Text _textToLocalize;

    private void Awake()
    {
        if (string.IsNullOrEmpty(_key))
        {
            return;
        }

        _textToLocalize = GetComponent<TextMeshProUGUI>();
        if (_textToLocalize == null)
        {
            _textToLocalize = GetComponent<TextMeshPro>();
        }

        if (_textToLocalize != null)
        {
            _textToLocalize.text = Services.Instance.Resolve<LocalizationService>().Localize(_key);
        }
    }
}
