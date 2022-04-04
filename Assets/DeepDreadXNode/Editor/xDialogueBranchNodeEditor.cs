using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditorInternal;
using UnityEditor;

namespace Dialogue 
{
    [CustomNodeEditor(typeof(xDialogueBranchNode))]
    public class xDialogueBranchNodeEditor : NodeEditor {
        private xDialogueBranchNode simpleNode;

        public override void OnBodyGUI() {
            if (simpleNode == null) simpleNode = target as xDialogueBranchNode;

            // Update serialized object's representation
            serializedObject.Update();

            //Puertos
            NodeEditorGUILayout.PortField(simpleNode.GetPort("entry"));

            //Input
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueText"));
            
            //Iterar a traves de los puertos dinamicos y añadirlos
            foreach (XNode.NodePort dynamicPort in target.DynamicPorts) {
                if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
                    if(dynamicPort.IsList)
                    {
                        NodeEditorGUILayout.DynamicPortList(dynamicPort.fieldName, dynamicPort.ValueType, serializedObject, dynamicPort.direction, dynamicPort.connectionType);
                    } else {
                        EditorGUILayout.BeginHorizontal();
                        if(GUILayout.Button("-")) {
                            simpleNode.RemoveBranch(dynamicPort);
                        };


                        simpleNode.branches[dynamicPort.fieldName] = EditorGUILayout.TextField(simpleNode.branches[dynamicPort.fieldName], GUILayout.ExpandWidth(true));
                        NodeEditorGUILayout.PortField(simpleNode.GetPort(dynamicPort.fieldName), GUILayout.MinWidth(0));
                        EditorGUILayout.EndHorizontal();
                    }
                }
            
            //Action
            if(GUILayout.Button("+"))
            {
                simpleNode.AddBranch();
            }

            // Apply property modifications
            serializedObject.ApplyModifiedProperties();
        }

        void OnCreateReorderableList(ReorderableList list) {
            // Override drawHeaderCallback to display node's name instead
            list.drawHeaderCallback = (Rect rect) => {
                string title = serializedObject.targetObject.ToString();
                EditorGUI.LabelField(rect, "hola");
            };
        }
    }
}