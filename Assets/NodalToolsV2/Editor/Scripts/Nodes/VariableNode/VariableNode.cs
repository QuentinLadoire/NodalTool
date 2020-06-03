using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV2
{
	abstract public class VariableNode<T> : BaseNode
	{
		protected string m_variableName = "VariableName";
		protected T m_variable;

		public VariableNode(int id, Vector2 position, string title = "VariableNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(200, 80);

			m_outputs.Add(new Output());
			m_outputs[0].m_parentNode = this;
		}

		public override bool CompatibleType(object typeToCompare)
		{
			return typeToCompare is T;
		}
	}
}
