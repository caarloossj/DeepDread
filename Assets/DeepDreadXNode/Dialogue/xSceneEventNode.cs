using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue 
{
    public enum SceneEventType
    {
        PlayAnimation,
        ChangeFocus,
        PlotPoint
    }

    [CreateNodeMenu("Dialogue/Scene Event")]
    public class xSceneEventNode : xBaseNode {

        [Input] public int entry;
        [Output] public int exit;
        public SceneEventType eventType;
        //TODO: Timeline and wait until end
        public AnimationClip animation;
        public Vector3 focus;
        public string plotPoint;

        public override void OnCreateConnection(NodePort from, NodePort to) {
            xDialogueNode previousNode = from.node as xDialogueNode;
            //foreach (var emotion in previousNode.GetCharacterEmotions())
            //{
            //    Debug.Log(emotion);
            //}
        }

        public override object GetValue(NodePort port) {
            return 1;
        }
    }
}