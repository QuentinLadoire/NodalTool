using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NodalToolsV3
{
	abstract public class VariableNode<T> : BaseNode
	{
		public string m_variableName = "VariableName";
		public T m_variable;

		public VariableNode(int id, Vector2 position, string title = "VariableNode") : base(id, position, title)
		{
			m_rect.size = new Vector2(200, 80);

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
				new JProperty("variableName", m_variableName),
				new JProperty("variable", m_variable)
				);

			return jnode.ToString();
		}
	}
}
