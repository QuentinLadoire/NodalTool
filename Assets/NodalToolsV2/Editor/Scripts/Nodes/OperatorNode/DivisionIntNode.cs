using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV2
{
	public class DivisionIntNode : OperatorNode<int>
	{
		public DivisionIntNode(int id, Vector2 position, string title = "DivisionIntNode") : base(id, position, title)
		{
		}

		public override void Draw(int id)
		{
			base.Draw(id);

			int variable1 = 0;
			int variable2 = 0;
			if (m_inputs[0].m_output != null)
			{
				variable1 = (int)m_inputs[0].m_output.GetValue();
			}
			if (m_inputs[1].m_output != null)
			{
				variable2 = (int)m_inputs[1].m_output.GetValue();
			}

			int m_result = 0;
			if (variable2 != 0)
			{
				m_result = variable1 / variable2;
			}
			m_outputs[0].m_value = m_result;

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("O"))
			{
				if (GraphEditor.m_clickedInput == null)
				{
					GraphEditor.m_clickedInput = m_inputs[0];
				}
			}
			GraphEditor.GetLastRect(ref m_inputs[0].m_rect, m_rect.position);
			GUILayout.Label("Variable1 : " + variable1);
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("O"))
			{
				if (GraphEditor.m_clickedInput == null)
				{
					GraphEditor.m_clickedInput = m_inputs[1];
				}
			}
			GraphEditor.GetLastRect(ref m_inputs[1].m_rect, m_rect.position);
			GUILayout.Label("Variable2 : " + variable2);
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Result : " + m_result);
			if (GUILayout.Button("O"))
			{
				if (GraphEditor.m_clickedOutput == null)
				{
					GraphEditor.m_clickedOutput = m_outputs[0];
				}
			}
			GraphEditor.GetLastRect(ref m_outputs[0].m_rect, m_rect.position);
			GUILayout.EndHorizontal();

			GUILayout.EndHorizontal();
		}

	}
}
