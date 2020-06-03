using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV2
{
	abstract public class OperatorNode<T> : BaseNode
	{
		public OperatorNode(int id, Vector2 position, string title = "OperatorNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(220, 80);

			m_inputs.Add(new Input());
			m_inputs.Add(new Input());
			m_inputs[0].m_parentNode = this;
			m_inputs[1].m_parentNode = this;

			m_outputs.Add(new Output());
			m_outputs[0].m_parentNode = this;
		}

		public override bool CompatibleType(object typeToCompare)
		{
			return typeToCompare is T;
		}
	}
}
