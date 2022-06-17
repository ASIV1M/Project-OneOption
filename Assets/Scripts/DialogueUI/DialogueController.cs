using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueUI;

    [SerializeField] private TMP_Text _textName;
    [SerializeField] private TMP_Text _textBox;

    [SerializeField] private Image _leftImage;
    [SerializeField] private GameObject _leftImageGO;
    [SerializeField] private Image _rigthImage;
    [SerializeField] private GameObject _rigthImageGO;

    [Space]
    [SerializeField] private Button _button01;
    [SerializeField] private TMP_Text _buttonText01;
    [Space]
    [SerializeField] private Button _button02;
    [SerializeField] private TMP_Text _buttonText02;
    [Space]
    [SerializeField] private Button _button03;
    [SerializeField] private TMP_Text _buttonText03;
    [Space]
    [SerializeField] private Button _button04;
    [SerializeField] private TMP_Text _buttonText04;

    private List<Button> _buttons = new List<Button>();
    private List<TMP_Text> _buttonsTexts = new List<TMP_Text>();


    private void Awake()
    {
        ShowDialogue(false);

        _buttons.Add(_button01);
        _buttons.Add(_button02);
        _buttons.Add(_button03);
        _buttons.Add(_button04);

        _buttonsTexts.Add(_buttonText01);
        _buttonsTexts.Add(_buttonText02);
        _buttonsTexts.Add(_buttonText03);
        _buttonsTexts.Add(_buttonText04);

    }


    public void ShowDialogue(bool show)
    {
        _dialogueUI.SetActive(show);
    }


    public void SetText(string name, string textBox)
    {
        _textName.text = name;
        _textBox.text = textBox;
    }

    public void SetImage(Sprite image, DialogueFaceImageType dialogueFaceImageType)
    {
        _leftImageGO.SetActive(false);
        _rigthImageGO.SetActive(false);

        if(image != null)
        {
            if(dialogueFaceImageType == DialogueFaceImageType.Left)
            {
                _leftImage.sprite = image;
                _leftImageGO.SetActive(true);
            }
            else
            {
                _rigthImage.sprite = image;
                _rigthImageGO.SetActive(true);
            }
        }
    }

    public void SetButtons(List<string> texts, List<UnityAction> actions)
    {
        _buttons.ForEach(button => button.gameObject.SetActive(false));

        for (int i = 0; i < texts.Count; i++)
        {
            _buttonsTexts[i].text = texts[i];
            _buttons[i].gameObject.SetActive(true);
            _buttons[i].onClick = new Button.ButtonClickedEvent();
            _buttons[i].onClick.AddListener(actions[i]);
        }
    }
    
}
