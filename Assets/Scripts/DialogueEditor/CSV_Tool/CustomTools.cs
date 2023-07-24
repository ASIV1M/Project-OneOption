using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomTools
{
    [MenuItem("Custom Tools/Dialogue/Update Dialogue Languges")]
    public static void UpdataDialogueLanguage()
    {
        UpdateLanguageType updateLanguageType = new UpdateLanguageType();
        updateLanguageType.UpdateLanguage();

        EditorApplication.Beep();
        Debug.Log("<color=green>Update languges successfully </color>");
    }
}
