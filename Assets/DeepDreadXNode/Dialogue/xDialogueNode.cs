using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue 
{
    [NodeWidth(324)]
    [CreateNodeMenu("Dialogue/Dialogue")]
    public class xDialogueNode : xBaseNode {

        [Input] public int entry;
        [Output] public int exit;
        public CharacterSheet character;
        public string dialogueText;
        public bool isSkippable = true;

        [ContextMenu("Add New Event")]
        public void AddEvents() {  
            this.AddDynamicOutputList(typeof(int), fieldName: "Event");
            UpdatePorts();
        }

        public void RemoveEvents() { 
            foreach (NodePort port in Ports)
            {
                if(port.fieldName.Contains("Event"))
                {
                    this.RemoveDynamicPort(port);
                    UpdatePorts();
                }
            }
        }

        public string[] GetCharacterEmotions()
        {
            return character.emotionAnims;
        }

        public override object GetValue(NodePort port) {
            return 1;
        }
    }
}