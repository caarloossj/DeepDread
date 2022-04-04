using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditorInternal;
using UnityEditor;

namespace Dialogue 
{
    [CustomNodeEditor(typeof(xDialogueNode))]
    public class xDialogueNodeEditor : NodeEditor {
        private xDialogueNode simpleNode;
        private CharacterSheet currentCharacter = null;

        public override void OnBodyGUI() {
            if (simpleNode == null) simpleNode = target as xDialogueNode;

            // Update serialized object's representation
            serializedObject.Update();

            //Puertos
            NodeEditorGUILayout.PortField(simpleNode.GetPort("entry"));
            NodeEditorGUILayout.PortField(simpleNode.GetPort("exit"));

            //Input
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("character"));
            EditorGUILayout.LabelField("Dialogue");
            simpleNode.dialogueText = EditorGUILayout.TextArea(simpleNode.dialogueText, GUILayout.ExpandWidth(true), GUILayout.Height(60));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("isSkippable"));
            
            //Iterar a traves de los puertos dinamicos y añadirlos
            foreach (XNode.NodePort dynamicPort in target.DynamicPorts) {
                    if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                    if(dynamicPort.IsList)
                    {
                        NodeEditorGUILayout.DynamicPortList(dynamicPort.fieldName, dynamicPort.ValueType, serializedObject, dynamicPort.direction, dynamicPort.connectionType);
                    } else {
                        NodeEditorGUILayout.PortField(simpleNode.GetPort(dynamicPort.fieldName));
                    }
                }

            //Color
            if(currentCharacter != simpleNode.character)
            {
                currentCharacter = simpleNode.character;
                if(currentCharacter != null){
                    Debug.Log("Change");
                    SetTint(Color.Lerp(currentCharacter.characterColor, Color.black, .6f));
                }
            }

            //Action
            if(simpleNode.GetPort("Event") == null)
            {
                if(GUILayout.Button("Add Events"))
                {
                    simpleNode.AddEvents();
                }
            } else 
            {
                if(GUILayout.Button("Remove Events"))
                {
                    simpleNode.RemoveEvents();
                }
            }


            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }
    }
}