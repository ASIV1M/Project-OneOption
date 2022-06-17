using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;

public class StartNode : BaseNode
{
    

    public StartNode() { }

    public StartNode(Vector2 position, WindowGraph editorWindow, DialogueGraphView graphView)
    {
        EditorWindow = editorWindow;
        GraphView = graphView;

        title = "Start";
        SetPosition(new Rect(position, defaultNodeSize));
        NodeGuid = Guid.NewGuid().ToString();

        AddOutputPort("Output", Port.Capacity.Single);

        RefreshExpandedState();
        RefreshPorts();
    }
}
