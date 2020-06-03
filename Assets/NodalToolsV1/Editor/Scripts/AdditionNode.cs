using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV1
{
	public abstract class AdditionNode<T> : OperatorNode<T>
	{
		public AdditionNode(int id, Vector2 position, string title = "AdditionNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(200, 80);

			IOutput<T> input1 = null;
			IOutput<T> input2 = null;

			m_inputs.Add(input1);
			m_inputs.Add(input2);

			m_inputRects.Add(new Rect());
			m_inputRects.Add(new Rect());
		}
	}

	public class AdditionIntNode : AdditionNode<int>
	{
		public AdditionIntNode(int id, Vector2 position, string title = "AdditionIntNode") : base(id, position, title)
		{
		}

		public override int GetValue()
		{
			int variable1 = 0;
			int variable2 = 0;
			if (m_inputs[0] != null)
			{
				variable1 = m_inputs[0].GetValue();
			}
			if (m_inputs[1] != null)
			{
				variable2 = m_inputs[1].GetValue();
			}

			return variable1 + variable2;
		}

		public override void SetInput(object input, int index)
		{
			if (index < m_inputRects.Count && index > -1)
			{
				m_inputs[index] = input as IOutput<int>;
			}
		}

		public override int GetIndexInput(Vector3 position)
		{
			for (int i = 0; i < m_inputRects.Count; i++)
			{
				Rect newRect = new Rect(rect.position + m_inputRects[i].position, m_inputRects[i].size);
				if (newRect.Contains(position))
				{
					return i;
				}
			}

			return -1;
		}

		public override void Draw(int id)
		{
			base.Draw(id);

			GUILayout.BeginVertical();

			int variable1 = 0;
			int variable2 = 0;
			if (m_inputs[0] != null)
			{
				variable1 = m_inputs[0].GetValue();
			}
			if (m_inputs[1] != null)
			{
				variable2 = m_inputs[1].GetValue();
			}

			GUILayout.Label("Variable1 : " + variable1);
			m_inputRects[0] = GUILayoutUtility.GetLastRect();

			GUILayout.Label("Variable2 : " + variable2);
			m_inputRects[1] = GUILayoutUtility.GetLastRect();

			GUILayout.Label("Result : " + GetValue());

			GUILayout.EndVertical();
		}
	}
}
