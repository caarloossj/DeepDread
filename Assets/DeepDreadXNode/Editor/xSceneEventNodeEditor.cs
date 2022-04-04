using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditorInternal;
using UnityEditor;

namespace Dialogue 
{
    [CustomNodeEditor(typeof(xSceneEventNode))]
    public class xSceneEventNodeEditor : NodeEditor {
        private xSceneEventNode simpleNode;

        public override void OnBodyGUI() {
            if (simpleNode == null) simpleNode = target as xSceneEventNode;

            // Update serialized object's representation
            serializedObject.Update();

            //Puertos
            NodeEditorGUILayout.PortPair(simpleNode.GetPort("entry"), simpleNode.GetPort("exit"));

            //Input
            EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType"));

            //Switch type
            switch (simpleNode.eventType)
            {
                case SceneEventType.PlayAnimation:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("animation"));
                    break;
                case SceneEventType.ChangeFocus:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("focus"));
                    break;
                case SceneEventType.PlotPoint:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("plotPoint"));
                    break;
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