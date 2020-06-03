using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV1
{
	public abstract class VariableNode<T> : BaseNode, IOutput<T>
	{
		public VariableNode(int id, Vector2 position, string title = "VariableNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(200, 80);
			m_variableName = "NewVariable";
			m_variable = default;
		}

		protected Rect m_inputRect = default;

		protected string m_variableName;
		protected T m_variable;

		public abstract T GetValue();
	}
}
