using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV3
{
	public class DivisionIntNode : OperatorNode<int>
	{
		public DivisionIntNode(int id, Vector2 position, string title = "DivisionIntNode") : base(id, position, title)
		{
			m_outputs[0].onGetValue = Calculate;
		}

		protected override object Calculate()
		{
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

			if (variable2 == 0)
			{
				return 0;
			}
			return variable1 / variable2;
		}

		public override void Draw(int id)
		{
			base.Draw(id);

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("O"))
			{
				if (GraphEditor.clickedInput == null)
				{
					GraphEditor.clickedInput = m_inputs[0];
				}
			}
			GraphEditor.GetLastRect(ref m_inputs[0].m_rect, m_rect.position);
			GUILayout.Label("Variable1");
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			if (GUILayout.Button("O"))
			{
				if (GraphEditor.clickedInput == null)
				{
					GraphEditor.clickedInput = m_inputs[1];
				}
			}
			GraphEditor.GetLastRect(ref m_inputs[1].m_rect, m_rect.position);
			GUILayout.Label("Variable2");
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Result : " + (int)m_outputs[0].GetValue());
			if (GUILayout.Button("O"))
			{
				if (GraphEditor.clickedOutput == null)
				{
					GraphEditor.clickedOutput = m_outputs[0];
				}
			}
			GraphEditor.GetLastRect(ref m_outputs[0].m_rect, m_rect.position);
			GUILayout.EndHorizontal();

			GUILayout.EndHorizontal();
		}
	}
}
