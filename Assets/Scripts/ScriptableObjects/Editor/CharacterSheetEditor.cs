using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterSheet))]
public class CharacterSheetEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();
    }
}
