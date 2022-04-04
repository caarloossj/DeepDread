using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Story 
{
	[CreateAssetMenu(menuName = "Graphs/Story Graph")]
	[RequireNode(typeof(PlotPoint))]
	public class MainStoryGraph : NodeGraph { 
		public xBaseNode current;
	}
}