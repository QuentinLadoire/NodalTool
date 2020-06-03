using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV1
{
	public class IntNode : VariableNode<int>
	{
		public IntNode(int id, Vector2 position) : base(id, position, "IntNode")
		{
		}

		public override int GetValue()
		{
			return m_variable;
		}

		public override void Draw(int id)
		{
			base.Draw(id);

			GUILayout.BeginVertical();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Name :");
			m_variableName = EditorGUILayout.TextField(m_variableName);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Value : ");
			m_variable = EditorGUILayout.IntField(m_variable);
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}
	}
}
