using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NodalToolsV3
{
	abstract public class OperatorNode<T> : BaseNode
	{
		public OperatorNode(int id, Vector2 position, string title = "OperatorNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(220, 80);

			m_inputs.Add(new Input());
			m_inputs.Add(new Input());
			m_inputs[0].m_parentNode = this;
			m_inputs[1].m_parentNode = this;

			m_outputs.Add(new Output());
			m_outputs[0].m_parentNode = this;
		}

		public override bool CompatibleType(object typeToCompare)
		{
			return typeToCompare is T;
		}

		public override string Serialize()
		{
			JObject jnode = new JObject(
				new JProperty("type", GetType().ToString().Substring(13)),
				new JProperty("id", m_id),
				new JProperty("position", new JObject(new JProperty("x", m_rect.position.x), new JProperty("y", m_rect.position.y))),
				new JProperty("inputs", new JArray(
					from input in m_inputs
					where input.m_output != null
					orderby m_inputs.IndexOf(input)
					select new JObject(
						new JProperty("index", m_inputs.IndexOf(input)),
						new JProperty("output", new JObject(
							new JProperty("idNode", input.m_output.m_parentNode.id),
							new JProperty("index", input.m_output.m_parentNode.m_outputs.IndexOf(input.m_output))
							))
						)
					))
				);

			return jnode.ToString();
		}

		protected abstract object Calculate();
	}
}
