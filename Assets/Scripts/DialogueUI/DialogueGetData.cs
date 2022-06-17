using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGetData : MonoBehaviour
{
    [SerializeField] protected DialogueContainerSO _dialogueContainer;
    
    
    protected BaseNodeData GetNodeByGuid(string targetNodeGuid)
    {
        return _dialogueContainer.AllNodes.Find(node => node.NodeGuid == targetNodeGuid);
    }

    protected BaseNodeData GetNodeByNodePort(DialogueNodePort nodePort)
    {
        return _dialogueContainer.AllNodes.Find(node => node.NodeGuid == nodePort.InputGuid);
    }

    protected BaseNodeData GetNextNode(BaseNodeData baseNodeData)
    {
        NodeLinkData nodeLinkData = _dialogueContainer.NodeLinkDatas.Find(edge => edge.BaseNodeGuid == baseNodeData.NodeGuid);

        return GetNodeByGuid(nodeLinkData.TargetNodeGuid);
    }
}
