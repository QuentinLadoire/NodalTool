using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV4
{
	public class AdditionNode<T> : OperatorNode<T>
	{
		public AdditionNode(int id, Vector2 position, string title = "AdditionIntNode") : base(id, position, title)
		{
		}

		protected override void Calculate()
		{
			dynamic variable1 = default;
			dynamic variable2 = default;

			var input1 = m_inputs[0] as InputPin<T>;
			var input2 = m_inputs[1] as InputPin<T>;
			if (input1.m_output != null)
			{
				variable1 = ((OutputPin<T>)input1.m_output).GetPinValue();
			}
			if (input2.m_output != null)
			{
				variable2 = ((OutputPin<T>)input2.m_output).GetPinValue();
			}

			((OutputPin<T>)m_outputs[0]).SetPinValue(variable1 + variable2);
		}
	}
}
