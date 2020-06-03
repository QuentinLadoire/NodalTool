using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV2
{
	public class Output
	{
		public BaseNode m_parentNode = null;

		public Rect m_rect;

		public List<Input> m_inputs = new List<Input>();

		public object m_value = null;
		public object GetValue()
		{
			return m_value;
		}
	}
}
