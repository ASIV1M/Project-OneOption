using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkZone : MonoBehaviour
{
    [SerializeField] private GameObject _speechBubble;
    [SerializeField] private TMP_Text _keyInputText;
    
    private DialogueTalk _dialogueTalk;

    private Player _player;
    private int _countPush = 0;
    private bool _isTalk;

    public Character TriggerCharacter;

    public bool isTalk => _isTalk;

    private void Awake()
    {
        _speechBubble.SetActive(false);
        TriggerCharacter = GetComponent<Character>();
        _dialogueTalk = GetComponent<DialogueTalk>();
    }


    private void Update()
    {
        if(_player != null)
        {
            if (_player.IsInteract && _speechBubble.activeSelf)
            {
                _dialogueTalk.StartDialogue();
                _isTalk = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            _player = player;
            /*player.Input.cursorInputForLook = false;
            player.Input.cursorLocked = false;*/
            _speechBubble.SetActive(true);
            _isTalk = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            /*player.Input.cursorInputForLook = true;
            player.Input.cursorLocked = true;*/
            _player = null;
           _speechBubble.SetActive(false);
            _dialogueTalk.Controller.ShowDialogue(false);
            _isTalk = false;
        }
    }
}
