using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditorInternal;
using UnityEditor;

namespace Dialogue 
{
    [CustomNodeEditor(typeof(xEventNode))]
    public class xEventNodeEditor : NodeEditor {
        private xEventNode simpleNode;
        private Color currentColor;
        public override void OnBodyGUI() {
            if (simpleNode == null) simpleNode = target as xEventNode;

            // Update serialized object's representation
            serializedObject.Update();

            //Puertos
            NodeEditorGUILayout.PortField(simpleNode.GetPort("entry"));

            //Input
            EditorGUILayout.PropertyField(serializedObject.FindProperty("eventType"));

            //Switch type
            switch (simpleNode.eventType)
            {
                case EventType.PlayAudio:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("audioClip"));
                    break;
                case EventType.ShakeCamera:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("shakeAmount"));
                    break;
                case EventType.ChangeFont:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("font"));
                    break;
                case EventType.Emotion:
                    simpleNode.emotionIndex = EditorGUILayout.Popup(simpleNode.emotionIndex, simpleNode.emotions);
                    break;
            }

            //Color
            if(simpleNode.characterColor != currentColor)
            {
                currentColor = simpleNode.characterColor;
                if(currentColor != default(Color)){
                    Debug.Log("Change");
                    SetTint(Color.Lerp(currentColor, Color.black, .6f));
                }
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