using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue 
{
	[CreateAssetMenu(menuName = "Graphs/Dialogue Graph")]
	[RequireNode(typeof(xStartNode))]
	public class xDialogueGraph : NodeGraph { 
		public xBaseNode current;
	}
}