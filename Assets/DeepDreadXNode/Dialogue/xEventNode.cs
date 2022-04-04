using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue 
{
    public enum EventType
    {
        PlayAudio,
        ShakeCamera,
        ChangeFont,
        Emotion
    }

    [CreateNodeMenu("Dialogue/Event")]
    public class xEventNode : xBaseNode {

        [Input] public int entry;
        public EventType eventType;
        public AudioClip audioClip;
        public float shakeAmount;
        public Font font;
        public Color characterColor;
        public string[] emotions;
        public int emotionIndex = 0;

        protected override void Init() 
        {
            if(this.GetPort("entry").IsConnected)
                RefreshCharacter();
        }

        public override void OnCreateConnection(NodePort from, NodePort to) {
            RefreshCharacter();
        }

        private void RefreshCharacter()
        {
            //xDialogueNode previousNode = from.node as xDialogueNode;
            xDialogueNode previousNode = this.GetPort("entry").Connection.node as xDialogueNode;
            if(previousNode.character != null)
            {
                characterColor = previousNode.character.characterColor;
                emotions = previousNode.character.emotionAnims;
            }
        }

        public override object GetValue(NodePort port) {
            return 1;
        }
    }
}