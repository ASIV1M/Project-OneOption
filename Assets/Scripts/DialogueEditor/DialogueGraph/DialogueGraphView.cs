using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    private string _styleSheetsName = "GraphView";
    private WindowGraph _editorWindow;
    private NodeSearchWindow _nodeSearchWindow;

    public DialogueGraphView(WindowGraph editorWindow)
    {
        _editorWindow = editorWindow;
        StyleSheet tmpStyleSheet = Resources.Load<StyleSheet>(_styleSheetsName);
        styleSheets.Add(tmpStyleSheet);

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());

        GridBackground grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddSearchWindow();
    }


    private void AddSearchWindow()
    {
        _nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _nodeSearchWindow.Configure(_editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _nodeSearchWindow);

    }


    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        Port startPortView = startPort;
        ports.ForEach(port =>
        {
            Port portView = port;

            if (startPortView != portView && startPortView.node != portView.node && startPortView.direction != port.direction)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }


    public void LanguageReload()
    {
        List<DialogueNode> dialogueNodes = nodes.ToList().Where(node => node is DialogueNode).Cast<DialogueNode>().ToList();

        foreach (DialogueNode dialogueNode in dialogueNodes)
        {
            dialogueNode.ReloadLanguage();
        }
    }


    public StartNode CreateStartNode(Vector2 position)
    {
        StartNode tmp = new StartNode(position, _editorWindow, this);

        return tmp;
    }


    public DialogueNode CreateDialogueNode(Vector2 position)
    {
        DialogueNode tmp = new DialogueNode(position, _editorWindow, this);

        return tmp;
    }


    public EventNode CreateEventNode(Vector2 position)
    {
        EventNode tmp = new EventNode(position, _editorWindow, this);

        return tmp;
    }


    public EndNode CreateEndNode(Vector2 position)
    {
        EndNode tmp = new EndNode(position, _editorWindow, this);

        return tmp;
    }
}
