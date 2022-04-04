using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue 
{
    [CreateNodeMenu("Dialogue/Start")]
    public class xStartNode : xBaseNode {

        [Output] public int exit;
        public override string GetString()
        {
            return "Start" ;
        }
    }
}