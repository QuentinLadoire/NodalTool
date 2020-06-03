using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV4
{
	public class DivisionNode<T> : OperatorNode<T>
	{
		public DivisionNode(int id, Vector2 position, string title = "DivisionIntNode") : base(id, position, title)
		{
		}

		protected override void Calculate()
		{
			dynamic variable1 = 0;
			dynamic variable2 = 0;

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

			//if division by zero return 0
			if (variable2 == 0)
			{
				((OutputPin<T>)m_outputs[0]).SetPinValue(variable2);
			}
			else
			{
				((OutputPin<T>)m_outputs[0]).SetPinValue(variable1 / variable2);
			}
		}
	}
}
