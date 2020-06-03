using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NodalToolsV4
{
	public class VariableNode<T> : BaseNode
	{
		public string m_variableName = "VariableName";
		T m_variable;
		public T variable
		{
			get => m_variable;
			set
			{
				m_variable = value;
			}
		}

		public VariableNode(int id, Vector2 position, string title = "VariableNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(200, 80);
			
			m_outputs.Add(new OutputPin<T>());
			m_outputs[0].m_parentNode = this;
			((OutputPin<T>)m_outputs[0]).onGetValue = SetPinValue;
		}

		void SetPinValue()
		{
			((OutputPin<T>)m_outputs[0]).SetPinValue(m_variable);
		}

		public override string Serialize()
		{
			JObject jnode = new JObject(
				new JProperty("type", GetType().ToString()),
				new JProperty("id", m_id),
				new JProperty("position", new JObject(new JProperty("x", m_rect.position.x), new JProperty("y", m_rect.position.y))),
				new JProperty("variableName", m_variableName),
				new JProperty("variable", m_variable)
				);

			return jnode.ToString();
		}
	}
}
