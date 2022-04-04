using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Story 
{
    [CreateNodeMenu("Story/Plot Point")]
    [NodeTint("#ff30e3c5")]
    public class PlotPoint : xBaseNode {

        [Output] public int exit;
        public object ScheduleOverride;

        public override string GetString()
        {
            return "Start" ;
        }
    }
}