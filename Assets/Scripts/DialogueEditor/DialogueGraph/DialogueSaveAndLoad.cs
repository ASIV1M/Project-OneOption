using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSaveAndLoad
{
    private DialogueGraphView _graphView;
    private List<Edge> _edges => _graphView.edges.ToList();
    private List<BaseNode> _nodes => _graphView.nodes.ToList().Where(node => node is BaseNode).Cast<BaseNode>().ToList();

    public DialogueSaveAndLoad(DialogueGraphView graphView)
    {
        _graphView = graphView;
    }


    public void Save(DialogueContainerSO dialogueContainer)
    {
        SaveEdges(dialogueContainer);
        SaveNodes(dialogueContainer);

        EditorUtility.SetDirty(dialogueContainer);
        AssetDatabase.SaveAssets();
    }


    public void Load(DialogueContainerSO dialogueContaincerSO)
    {
        ClearGraph();
        GenerateNodes(dialogueContaincerSO);
        ConnectNodes(dialogueContaincerSO);
    }


    private void SaveEdges(DialogueContainerSO dialogueContaincerSO)
    {
        dialogueContaincerSO.NodeLinkDatas.Clear();

        Edge[] connectedEdges = _edges.Where(edge => edge.input.node != null).ToArray();

        for (int i = 0; i < connectedEdges.Count(); i++)
        {
            BaseNode outputNode = (BaseNode)connectedEdges[i].output.node;
            BaseNode inputNode = (BaseNode)connectedEdges[i].input.node as BaseNode;

            dialogueContaincerSO.NodeLinkDatas.Add(new NodeLinkData
            {
                BaseNodeGuid = outputNode.NodeLeader,
                TargetNodeGuid = inputNode.NodeLeader,

            });
        }
    }

    #region Save
    private void SaveNodes(DialogueContainerSO dialogueContaincerSO)
    {
        dialogueContaincerSO.DialogueNodeDatas.Clear();
        dialogueContaincerSO.EventNodeDatas.Clear();
        dialogueContaincerSO.EndNodeDatas.Clear();
        dialogueContaincerSO.StartNodeDatas.Clear();


        _nodes.ForEach(node =>
        {
            switch (node)
            {
                case DialogueNode dialogueNode:
                    dialogueContaincerSO.DialogueNodeDatas.Add(SaveNodeData(dialogueNode));
                    break;
                case EventNode eventNode:
                    dialogueContaincerSO.EventNodeDatas.Add(SaveNodeData(eventNode));
                    break;
                case EndNode endNode:
                    dialogueContaincerSO.EndNodeDatas.Add(SaveNodeData(endNode));
                    break;
                case StartNode startNode:
                    dialogueContaincerSO.StartNodeDatas.Add(SaveNodeData(startNode));
                    break;
                default:
                    break;
            }
        });
    }


    private DialogueNodeData SaveNodeData(DialogueNode node)
    {
        DialogueNodeData dialogueNodeData = new DialogueNodeData
        {
            NodeGuid = node.NodeLeader,
            Position = node.GetPosition().position,
            TextType = node.Texts,
            Name = node.Name,
            DialogueFaseImageType = node.FaceImageType,
            Sprite = node.FaceImage,
            DialogueNodePorts = new List<DialogueNodePort>(node.DialogueNodePorts)
        };

        foreach (DialogueNodePort nodePort in dialogueNodeData.DialogueNodePorts)
        {
            nodePort.OutputGuid = string.Empty;
            nodePort.InputGuid = string.Empty;

            foreach (Edge edge in _edges)
            {
                if(edge.output == nodePort.Port)
                {
                    nodePort.OutputGuid = (edge.output.node as BaseNode).NodeLeader;
                    nodePort.InputGuid = (edge.input.node as BaseNode).NodeLeader;
                }
            }
        }

        return dialogueNodeData;
    }

    
    private StartNodeData SaveNodeData(StartNode node)
    {
        StartNodeData startNodeData = new StartNodeData()
        {
            NodeGuid = node.NodeLeader,
            Position= node.GetPosition().position,
        };

        return startNodeData;
    }


    private EndNodeData SaveNodeData(EndNode node)
    {
        EndNodeData endNodeData = new EndNodeData()
        {
            NodeGuid = node.NodeLeader,
            Position = node.GetPosition().position,
            EndNodeType = node.GetEndNodeSetType
        };

        return endNodeData;
    }


    private EventNodeData SaveNodeData(EventNode node)
    {
        EventNodeData eventNodeData = new EventNodeData()
        {
            NodeGuid = node.NodeLeader,
            Position = node.GetPosition().position,
            EventSO = node.DialogueEvent
        };

        return eventNodeData;
    }

    #endregion

    #region Load

    private void ClearGraph()
    {
        _edges.ForEach(edge => _graphView.RemoveElement(edge));

        foreach (BaseNode node in _nodes)
        {
            _graphView.RemoveElement(node);
        }
    }


    private void GenerateNodes(DialogueContainerSO dialogueContaincer)
    {
        foreach (StartNodeData startNode in dialogueContaincer.StartNodeDatas)
        {
            StartNode tempNode = _graphView.CreateStartNode(startNode.Position);
            tempNode.NodeLeader = startNode.NodeGuid;

            _graphView.AddElement(tempNode);
        }

        foreach (EndNodeData endNode in dialogueContaincer.EndNodeDatas)
        {
            EndNode tempNode = _graphView.CreateEndNode(endNode.Position);
            tempNode.NodeLeader = endNode.NodeGuid;
            tempNode.GetEndNodeSetType = endNode.EndNodeType;

            tempNode.LoadVlaueInToField();
            _graphView.AddElement(tempNode);
        }

        foreach (EventNodeData eventNode in dialogueContaincer.EventNodeDatas)
        {
            EventNode tempNode = _graphView.CreateEventNode(eventNode.Position);
            tempNode.NodeLeader = eventNode.NodeGuid;
            tempNode.DialogueEvent = eventNode.EventSO;

            tempNode.LoadVlaueInToField();
            _graphView.AddElement(tempNode);
        }

        foreach (DialogueNodeData dialogueNode in dialogueContaincer.DialogueNodeDatas)
        {
            DialogueNode tempNode = _graphView.CreateDialogueNode(dialogueNode.Position);
            tempNode.NodeLeader = dialogueNode.NodeGuid;
            tempNode.Name = dialogueNode.Name;
            tempNode.FaceImage = dialogueNode.Sprite;
            tempNode.FaceImageType = dialogueNode.DialogueFaseImageType;

            foreach (LanguageGeneric<string> languageGeneric in dialogueNode.TextType)
            {
                tempNode.Texts.Find(language => language.LanguageType == languageGeneric.LanguageType).LanguageGenericType = languageGeneric.LanguageGenericType;
            }

            foreach (DialogueNodePort nodePort in dialogueNode.DialogueNodePorts)
            {
                tempNode.AddChoicePort(tempNode,nodePort);
            }

            tempNode.LoadVlaueInToField();
            _graphView.AddElement(tempNode);
        }
    }


    private void ConnectNodes(DialogueContainerSO dialogueContainer)
    {
        for (int i = 0; i < _nodes.Count; i++)
        {
            List<NodeLinkData> connections = dialogueContainer.NodeLinkDatas.Where(edge => edge.BaseNodeGuid == _nodes[i].NodeLeader).ToList();
            
            for (int j = 0; j < connections.Count; j++)
            {
                string targetNodeGuid = connections[j].TargetNodeGuid;
                BaseNode targetNode = _nodes.First(node => node.NodeLeader == targetNodeGuid);

                if((_nodes[i] is DialogueNode) == false)
                {
                    LinkNodeTogether(_nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
                }
            }
        }

        List<DialogueNode> dialogueNodes = _nodes.FindAll(node => node is DialogueNode).Cast<DialogueNode>().ToList();

        foreach (DialogueNode dialogueNode in dialogueNodes)
        {
            foreach (DialogueNodePort nodePort in dialogueNode.DialogueNodePorts)
            {
                if(nodePort.InputGuid != string.Empty)
                {
                    BaseNode targetNode = _nodes.First(node => node.NodeLeader == nodePort.InputGuid);
                    LinkNodeTogether(nodePort.Port, (Port)targetNode.inputContainer[0]);
                }
            }
        }
    }


    private void LinkNodeTogether(Port outputPort, Port inputPort)
    {
        Edge tempEdge = new Edge()
        {
            output = outputPort,
            input = inputPort,
        };

        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        _graphView.Add(tempEdge);

    }
    #endregion

}
