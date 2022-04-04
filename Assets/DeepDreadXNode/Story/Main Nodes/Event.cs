using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Story 
{
	public class Event : Node {
		[Input] public int entry;
		[Output] public int exit;
		[Output(dynamicPortList = true)] public int[] events;

		// Use this for initialization
		protected override void Init() {
			base.Init();
			
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port) {
			return null; // Replace this
		}
	}
}