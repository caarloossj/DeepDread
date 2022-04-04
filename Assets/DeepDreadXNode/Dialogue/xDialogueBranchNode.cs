using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue 
{
    [CreateNodeMenu("Dialogue/Branch")]
    public class xDialogueBranchNode : xBaseNode {

        [Input] public int entry;
        public string dialogueText;
        [SerializeField]
        private NodePort test;

        [SerializeField] public BranchDictionary branches = new BranchDictionary();

        protected override void Init()
        {

        }

        public void AddBranch() {  
            NodePort newBranch = this.AddDynamicOutput(typeof(int), fieldName: "Branch_" + System.Guid.NewGuid().ToString());
            test = newBranch;
            branches.Add(newBranch.fieldName, "Empty Branch");
            UpdatePorts();
        }

        public void RemoveBranch(NodePort port) {  
            this.RemoveDynamicPort(port);
            branches.Remove(port.fieldName);
            UpdatePorts();
        }

        public override object GetValue(NodePort port) {
            return 1;
        }
    }

    [System.Serializable]
    public class BranchDictionary : Dictionary<string, string>, ISerializationCallbackReceiver {
        [SerializeField] private List<string> keys = new List<string>();
        [SerializeField] private List<string> values = new List<string>();

        public void OnBeforeSerialize() {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<string, string> pair in this) {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize() {
            this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception("there are " + keys.Count + " keys and " + values.Count + " values after deserialization. Make sure that both key and value types are serializable.");

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);
        }
    }
}