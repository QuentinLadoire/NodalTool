using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NodalToolsV4
{
	public class DebugLogNode : EventNode
	{
		string m_string = "";
		public string log
		{
			get => m_string;
			set => m_string = value;
		}

		public DebugLogNode(int id, Vector2 position, string title = "DebugLogNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(200, 110);

			m_inputs.Add(new InputPin<OnEvent>());
			m_inputs[0].m_parentNode = this;

			m_inputs.Add(new InputPin<int>());
			m_inputs[1].m_parentNode = this;
		}

		public override void OnEvent()
		{
			if (m_inputs[1].m_output != null)
			{
				m_string = ((OutputPin<int>)m_inputs[1].m_output).GetPinValue().ToString();
			}
			Debug.Log(m_string);

			base.OnEvent();
		}

		public override string Serialize()
		{
			JObject jnode = new JObject(
				new JProperty("type", GetType().ToString()),
				new JProperty("id", m_id),
				new JProperty("position", new JObject(new JProperty("x", m_rect.position.x), new JProperty("y", m_rect.position.y))),
				new JProperty("variable", m_string),
				new JProperty("inputs",
					new JArray(
						from input in m_inputs
						where input.m_output != null
						orderby m_inputs.IndexOf(input)
						select new JObject(
									new JProperty("index", m_inputs.IndexOf(input)),
									new JProperty("output", new JObject(
																new JProperty("idNode", input.m_output.m_parentNode.id),
																new JProperty("index", input.m_output.m_parentNode.m_outputs.IndexOf(input.m_output))
															)
									)
								)
					)
				)
				);

			return jnode.ToString();
		}
	}
}
