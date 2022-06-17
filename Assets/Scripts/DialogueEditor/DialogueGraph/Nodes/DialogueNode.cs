using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNode : BaseNode
{
    private List<LanguageGeneric<string>> texts = new List<LanguageGeneric<string>>();

    private string _name = "";
    private Sprite _faceImage;
    private DialogueFaceImageType _faceImageType;

    private List<DialogueNodePort> _dialogueNodePorts = new List<DialogueNodePort>();

    private TextField _textsField;
    private ObjectField _faceImageField;
    private TextField _nameField;
    private EnumField _faceImageTypeField;

    public DialogueNode() { }

    public DialogueNode(Vector2 position, WindowGraph editorWindow, DialogueGraphView graphView)
    {
        EditorWindow = editorWindow;
        GraphView = graphView;

        title = "Dialogue";
        SetPosition(new Rect(position, defaultNodeSize));
        NodeGuid = Guid.NewGuid().ToString();

        AddInputPort("Input", Port.Capacity.Multi);


        foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            texts.Add(new LanguageGeneric<string>
            {
                LanguageType = language,
                LanguageGenericType = ""
            });
        }


        _faceImageField = new ObjectField()
        {
            objectType = typeof(Sprite),
            allowSceneObjects = false,
            value = _faceImage
        };
        _faceImageField.RegisterValueChangedCallback(value =>
        {
            _faceImage = value.newValue as Sprite;
        });
        mainContainer.Add(_faceImageField);


        _faceImageTypeField = new EnumField()
        {
            value = _faceImageType
        };
         
        _faceImageTypeField.Init(_faceImageType);

        _faceImageTypeField.RegisterValueChangedCallback(value =>
        {
            _faceImageType = (DialogueFaceImageType)value.newValue;
        });

        mainContainer.Add(_faceImageField);

        Label labelName = new Label("Name");
        labelName.AddToClassList("LabelName");
        labelName.AddToClassList("Label");
        mainContainer.Add(labelName);


        _nameField = new TextField("");
        _nameField.RegisterValueChangedCallback(value =>
        {
            _name = value.newValue;
        });
        _nameField.SetValueWithoutNotify(_name);
        _nameField.AddToClassList("TextName");
        mainContainer.Add(_nameField);



        Label labelTexts = new Label("TextBox");
        labelTexts.AddToClassList("LabelTexts");
        labelTexts.AddToClassList("Label");
        mainContainer.Add(labelTexts);

        _textsField = new TextField("");
        _textsField.RegisterValueChangedCallback(value =>
        {
            texts.Find(text => text.LanguageType == editorWindow.LanguageType).LanguageGenericType = value.newValue;
        });
        _textsField.SetValueWithoutNotify(texts.Find(text => text.LanguageType == editorWindow.LanguageType).LanguageGenericType);
        _textsField.multiline = true;
        _textsField.AddToClassList("TextBox");

        mainContainer.Add(_textsField);


        Button button = new Button()
        {
            text = "Add Choice"
        };

        button.clicked += () =>
        {
            AddChoicePort(this);
        };

        titleButtonContainer.Add(button);
    }

    public Sprite FaceImage { get => _faceImage; set => _faceImage = value; }
    public string Name { get => _name; set => _name = value; }
    public DialogueFaceImageType FaceImageType { get => _faceImageType; set => _faceImageType = value; }
    public List<DialogueNodePort> DialogueNodePorts { get => _dialogueNodePorts; set => _dialogueNodePorts = value; }
    public List<LanguageGeneric<string>> Texts { get => texts; set => texts = value; }



    public void ReloadLanguage()
    {
        _textsField.RegisterValueChangedCallback(value =>
        {
            texts.Find(text => text.LanguageType == EditorWindow.LanguageType).LanguageGenericType = value.newValue;
        });

        _textsField.SetValueWithoutNotify(texts.Find(text => text.LanguageType == EditorWindow.LanguageType).LanguageGenericType);

        foreach (DialogueNodePort nodePort in _dialogueNodePorts)
        {
            nodePort.TextField.RegisterValueChangedCallback(value =>
            {
                nodePort.TextLanguages.Find(language => language.LanguageType == EditorWindow.LanguageType).LanguageGenericType = value.newValue;
            });
            nodePort.TextField.SetValueWithoutNotify(nodePort.TextLanguages.Find(language => language.LanguageType == EditorWindow.LanguageType).LanguageGenericType);
        }
    }


    public override void LoadVlaueInToField()
    {
        _textsField.SetValueWithoutNotify(texts.Find(language => language.LanguageType == EditorWindow.LanguageType).LanguageGenericType);
        _faceImageField.SetValueWithoutNotify(_faceImage);
        _faceImageTypeField.SetValueWithoutNotify(_faceImageType);
        _nameField.SetValueWithoutNotify(_name);
    }


    public Port AddChoicePort(BaseNode baseNode, DialogueNodePort dialoguePort = null)
    {
        Port port = GetPortInstance(Direction.Output);

        int outputPortCount = baseNode.outputContainer.Query("connector").ToList().Count();
        string outputPortName = $"Continue"; 
        
        DialogueNodePort dialogueNodePort = new DialogueNodePort();

        foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            dialogueNodePort.TextLanguages.Add(new LanguageGeneric<string>()
            {
                LanguageType = language,
                LanguageGenericType = outputPortName
            });
        }

        if(dialoguePort != null)
        {
            dialogueNodePort.InputGuid = dialoguePort.InputGuid;
            dialogueNodePort.OutputGuid = dialoguePort.OutputGuid;


            foreach (LanguageGeneric<string> languageGeneric in dialoguePort.TextLanguages)
            {
                dialogueNodePort.TextLanguages.Find(language => language.LanguageType == languageGeneric.LanguageType).LanguageGenericType = languageGeneric.LanguageGenericType;
            }
        }

        dialogueNodePort.TextField = new TextField();
        dialogueNodePort.TextField.RegisterValueChangedCallback(value =>
        {
            dialogueNodePort.TextLanguages.Find(language => language.LanguageType == EditorWindow.LanguageType).LanguageGenericType = value.newValue;
        });
        dialogueNodePort.TextField.SetValueWithoutNotify(dialogueNodePort.TextLanguages.Find(language => language.LanguageType == EditorWindow.LanguageType).LanguageGenericType);
        port.contentContainer.Add(dialogueNodePort.TextField);


        Button deleteButton = new Button(() => DeletePort(baseNode, port))
        {
            text = "X"
        };
        port.contentContainer.Add(deleteButton);

        dialogueNodePort.Port = port;
        port.portName = "";

        _dialogueNodePorts.Add(dialogueNodePort);

        baseNode.outputContainer.Add(port);

        baseNode.RefreshPorts();
        baseNode.RefreshExpandedState();
        
        return port;
    }
    

    private void DeletePort(BaseNode node, Port port)
    {
        DialogueNodePort tmp = _dialogueNodePorts.Find(requiredPort => requiredPort.Port == port);
        DialogueNodePorts.Remove(tmp);

        IEnumerable<Edge> portEdge = GraphView.edges.ToList().Where(edge => edge.output == port);

        if (portEdge.Any())
        {
            Edge edge = portEdge.First();
            edge.input.Disconnect(edge);
            edge.output.Disconnect(edge);
            GraphView.RemoveElement(edge);
        }

        node.outputContainer.Remove(port);

        node.RefreshPorts();
        node.RefreshExpandedState();
    }

}
