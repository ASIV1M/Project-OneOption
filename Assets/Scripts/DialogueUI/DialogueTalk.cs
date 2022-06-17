using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTalk : DialogueGetData
{
    [SerializeField] private DialogueController _dialogueController;
    [SerializeField] private DialogueData _dialogueData;
    


    private DialogueNodeData _currentDialogueNodeData;
    private DialogueNodeData _lastDialogueNodeData;
  
    

    public DialogueController Controller => _dialogueController;
    private void Awake()
    {
        _dialogueController = FindObjectOfType<DialogueController>();
    }


    public void StartDialogue()
    {
        CheckNodeType(GetNextNode(_dialogueContainer.StartNodeDatas[0]));
        _dialogueController.ShowDialogue(true);
    }


    private void CheckNodeType(BaseNodeData baseNodeData)
    {
        switch (baseNodeData)
        {
            case StartNodeData nodeData:
                RunNode(nodeData);
                break;
            case EndNodeData nodeData:
                RunNode(nodeData);
                break;
            case EventNodeData nodeData:
                RunNode(nodeData);
                break;
            case DialogueNodeData nodeData:
                RunNode(nodeData);
                break;
            default:
                break;
        }
    }

    private void RunNode(StartNodeData nodeData)
    {
        CheckNodeType(GetNextNode(_dialogueContainer.StartNodeDatas[0]));
    }


    private void RunNode(EndNodeData nodeData)
    {
        switch (nodeData.EndNodeType)
        {
            case EndNodeType.End:
                _dialogueController.ShowDialogue(false);
                break;
            case EndNodeType.Repeat:
                CheckNodeType(GetNodeByGuid(_currentDialogueNodeData.NodeGuid));
                break;
            case EndNodeType.Goback:
                CheckNodeType(GetNodeByGuid(_lastDialogueNodeData.NodeGuid));
                break;
            case EndNodeType.ReturnToStart:
                CheckNodeType(GetNextNode(_dialogueContainer.StartNodeDatas[0]));
                break;
            default:
                break;
        }
    }


    private void RunNode(EventNodeData nodeData)
    {
        if(nodeData.EventSO != null)
        {
            nodeData.EventSO.RunEvent();
        }

        CheckNodeType(GetNextNode(nodeData));
    }


    private void RunNode(DialogueNodeData nodeData)
    {
        if(_currentDialogueNodeData != nodeData)
        {
            _lastDialogueNodeData = _currentDialogueNodeData;
            _currentDialogueNodeData = nodeData;
        }

        _dialogueController.SetText(nodeData.Name, nodeData.TextType.Find(text => text.LanguageType == LanguageController.Instance.LanguageType).LanguageGenericType);
        _dialogueController.SetImage(nodeData.Sprite, nodeData.DialogueFaseImageType);
        
        MakeButtons(nodeData.DialogueNodePorts);
    }


    private void MakeButtons(List<DialogueNodePort> nodePorts)
    {
        List<string> texts = new List<string>();
        List<UnityAction> actions = new List<UnityAction>();
       
        foreach (DialogueNodePort port in nodePorts)
        {
            texts.Add(port.TextLanguages.Find(text => text.LanguageType == LanguageController.Instance.LanguageType).LanguageGenericType);
            UnityAction tempAction = null;
            tempAction += () =>
            {
                CheckNodeType(GetNodeByGuid(port.InputGuid));
            };
            actions.Add(tempAction);
        }
        _dialogueController.SetButtons(texts, actions);
    }
}
