using UnityEditor.UIElements;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class EndNode : BaseNode
{
    private EndNodeType _endNodeType = EndNodeType.End;
    private EnumField _field;
    
    public EndNode() { }

    public EndNode(Vector2 position, WindowGraph editorWindow, DialogueGraphView graphView)
    {
        EditorWindow = editorWindow;
        GraphView = graphView;

        title = "End";
        SetPosition(new Rect(position, defaultNodeSize));
        NodeGuid = Guid.NewGuid().ToString();

        AddInputPort("Input", Port.Capacity.Multi);

        _field = new EnumField()
        {
            value = _endNodeType
        };

        _field.Init(_endNodeType);

        _field.RegisterValueChangedCallback((value) =>
        {
            _endNodeType = (EndNodeType)value.newValue;
        });
        _field.SetValueWithoutNotify(_endNodeType);

        mainContainer.Add(_field);

    }


    public EndNodeType GetEndNodeSetType { get => _endNodeType; set => _endNodeType = value; }


    public override void LoadVlaueInToField()
    {
        _field.SetValueWithoutNotify(_endNodeType);
    }
}
