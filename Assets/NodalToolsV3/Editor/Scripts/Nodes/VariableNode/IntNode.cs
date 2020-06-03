using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV3
{
	public class IntNode : VariableNode<int>
	{
		public IntNode(int id, Vector2 position, string title = "IntNode") : base(id, position, title)
		{
			m_outputs[0].onGetValue = () => { return m_variable; };
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

			if (GUILayout.Button("O"))
			{
				if (GraphEditor.clickedOutput == null)
				{
					GraphEditor.clickedOutput = m_outputs[0];
				}
			}
			GraphEditor.GetLastRect(ref m_outputs[0].m_rect, m_rect.position);
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();
		}
	}
}