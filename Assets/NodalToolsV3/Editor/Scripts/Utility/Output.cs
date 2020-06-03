using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV3
{
	public class Output
	{
		public delegate object OnGetValue();
		public OnGetValue onGetValue = () => { return null; };

		public BaseNode m_parentNode = null;

		public Rect m_rect;

		public List<Input> m_inputs = new List<Input>();

		public object GetValue()
		{
			return onGetValue();
		}
	}
}
