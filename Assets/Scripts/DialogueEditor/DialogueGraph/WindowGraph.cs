using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEditor.UIElements;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;
using System;

public class WindowGraph : EditorWindow
{
    private DialogueContainerSO currentDialogueContainer;
    private DialogueGraphView graphView;
    private DialogueSaveAndLoad saveAndLoad;

    private Label nameOfDC;
    private ToolbarMenu _toolbarMenu;

    private LanguageType _languageType = LanguageType.English;

    public LanguageType LanguageType { get => _languageType; set => _languageType = value; }

    [OnOpenAsset(1)]
    public static bool ShowWindow(int instanceId, int line)
    {
        UnityEngine.Object item = EditorUtility.InstanceIDToObject(instanceId);

        if(item is DialogueContainerSO)
        {
            WindowGraph window = GetWindow(typeof(WindowGraph)) as WindowGraph;
            window.titleContent = new GUIContent("Dialogue Editor");
            window.currentDialogueContainer = item as DialogueContainerSO;
            window.minSize = new Vector2(500, 250);
            window.Load();
           
        }

        return false;
    }


    private void OnEnable()
    {
        ConstractGraphView();
        GenerateToolbar();
        Load();
    }


    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }


    private void GenerateToolbar()
    {
        StyleSheet styleSheet = Resources.Load<StyleSheet>("GraphView");
        rootVisualElement.styleSheets.Add(styleSheet);
        Toolbar toolBar = new Toolbar();
        
        Button saveButton = new ToolbarButton()
        {
            text = "Save"
        };

        saveButton.clicked += () =>
        {
            Save();
        };
        toolBar.Add(saveButton);

        Button loadButton = new ToolbarButton()
        {
            text = "Load"
        };

        loadButton.clicked += () =>
        {
            Load();
        };
        toolBar.Add(loadButton);

        _toolbarMenu = new ToolbarMenu();
        
        foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            _toolbarMenu.menu.AppendAction(language.ToString(), new Action<DropdownMenuAction>(x => Language(language, _toolbarMenu)));
        }
        toolBar.Add(_toolbarMenu);

        nameOfDC = new Label("");
        toolBar.Add(nameOfDC);
        

        nameOfDC.AddToClassList("nameOfDC");

        rootVisualElement.Add(toolBar);


    }


    private void ConstractGraphView()
    {
        graphView = new DialogueGraphView(this);
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);

        saveAndLoad = new DialogueSaveAndLoad(graphView);
    }


    private void Save()
    {
        if(currentDialogueContainer != null)
        {
            saveAndLoad.Save(currentDialogueContainer);
        }
    }


    private void Load()
    {
        if(currentDialogueContainer != null)
        {
            Language(LanguageType.English, _toolbarMenu);
            nameOfDC.text = "Name: " + currentDialogueContainer.name;
            saveAndLoad.Load(currentDialogueContainer);
        }
    }
    

    private void Language(LanguageType language, ToolbarMenu toolbarMenu)
    {
        toolbarMenu.text = "Language: " + language.ToString();
        _languageType = language;
        graphView.LanguageReload();
    }
}
