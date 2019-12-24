using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheGamedevGuru
{
public class TextUpdater : MonoBehaviour
{
    private static readonly string[] Texts = new[] {"Spawn ", "Despawn "};
    private Button _button = null;
    private TextMeshProUGUI _textToUpdate;
    private int _currentTxtIndex = 0;
        
    private void Awake()
    {
        _button = GetComponent<Button>();
        _textToUpdate = _button.GetComponentInChildren<TextMeshProUGUI>();
        UpdateTextInternal();
        _button.onClick.AddListener(SetNextText);
    }
    
    private void SetNextText()
    {
        _currentTxtIndex++;
        UpdateTextInternal();
    }

    private void UpdateTextInternal()
    {
        _textToUpdate.text = $"{Texts[_currentTxtIndex % Texts.Length]}\n{_button.name}";
    }
}
}