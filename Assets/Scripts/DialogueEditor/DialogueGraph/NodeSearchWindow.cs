using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private WindowGraph _editorWindow;
    private DialogueGraphView _graphView;


    private Texture2D _picture;

    public void Configure(WindowGraph editorGraph,DialogueGraphView graphView)
    {
        _editorWindow = editorGraph;
        _graphView = graphView;

        _picture = new Texture2D(1, 1);
        _picture.SetPixel(0,0,new Color(0,0,0,0));
        _picture.Apply();
    }


    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> tree = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Dialogue Node"), 0),
            new SearchTreeGroupEntry(new GUIContent("Dialogue"), 1),

            AddNodeSearch("Start Node", new StartNode()),
            AddNodeSearch("Doalogue Node", new DialogueNode()),
            AddNodeSearch("Event Node", new EventNode()),
            AddNodeSearch("End Node", new EndNode()),
        };

        return tree;
    }


    private SearchTreeEntry AddNodeSearch(string name, BaseNode baseNode)
    {
        SearchTreeEntry tmp = new SearchTreeEntry(new GUIContent(name, _picture))
        {
            level = 2,
            userData = baseNode
        };
        
        return tmp;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        Vector2 mousePosition = _editorWindow.rootVisualElement.ChangeCoordinatesTo
            (
            _editorWindow.rootVisualElement.parent, context.screenMousePosition - _editorWindow.position.position
            );

        Vector2 graphMousePosition = _graphView.contentViewContainer.WorldToLocal(mousePosition);
        return CheckForNodeType(SearchTreeEntry, graphMousePosition);
    }

    private bool CheckForNodeType(SearchTreeEntry searchTreeEntry, Vector2 position)
    {
        switch (searchTreeEntry.userData)
        {
            case StartNode node:
                _graphView.AddElement(_graphView.CreateStartNode(position));
                return true;
            case DialogueNode node:
                _graphView.AddElement(_graphView.CreateDialogueNode(position));
                return true;
            case EventNode node:
                _graphView.AddElement(_graphView.CreateEventNode(position));
                return true;
            case EndNode node:
                _graphView.AddElement(_graphView.CreateEndNode(position));
                return true;
            default:
                break;
        }
        return false;
    }
}
