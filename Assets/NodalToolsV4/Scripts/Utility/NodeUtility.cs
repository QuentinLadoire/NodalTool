using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NodalToolsV4
{
	public static class NodeUtility
	{
		public static BaseNode DeserializeNode(JToken jNode, out JObject jInput)
		{
			BaseNode baseNode = null;
			jInput = null;

			if ((string)jNode["type"] == typeof(VariableNode<int>).ToString())
			{
				int id = (int)jNode["id"];
				Vector2 position = new Vector2((float)jNode["position"]["x"], (float)jNode["position"]["y"]);
				string variableName = (string)jNode["variableName"];
				int variable = (int)jNode["variable"];

				baseNode = new VariableNode<int>(id, position);
				((VariableNode<int>)baseNode).variable = variable;
				((VariableNode<int>)baseNode).m_variableName = variableName;
			}
			else if ((string)jNode["type"] == typeof(AdditionNode<int>).ToString())
			{
				int id = (int)jNode["id"];
				Vector2 position = new Vector2((float)jNode["position"]["x"], (float)jNode["position"]["y"]);

				baseNode = new AdditionNode<int>(id, position);

				jInput = new JObject(new JProperty("idParent", id), new JProperty("inputs", jNode["inputs"]));
			}
			else if ((string)jNode["type"] == typeof(SubstractionNode<int>).ToString())
			{
				int id = (int)jNode["id"];
				Vector2 position = new Vector2((float)jNode["position"]["x"], (float)jNode["position"]["y"]);

				baseNode = new SubstractionNode<int>(id, position);

				jInput = new JObject(new JProperty("idParent", id), new JProperty("inputs", jNode["inputs"]));
			}
			else if ((string)jNode["type"] == typeof(MultiplicationNode<int>).ToString())
			{
				int id = (int)jNode["id"];
				Vector2 position = new Vector2((float)jNode["position"]["x"], (float)jNode["position"]["y"]);

				baseNode = new MultiplicationNode<int>(id, position);

				jInput = new JObject(new JProperty("idParent", id), new JProperty("inputs", jNode["inputs"]));
			}
			else if ((string)jNode["type"] == typeof(DivisionNode<int>).ToString())
			{
				int id = (int)jNode["id"];
				Vector2 position = new Vector2((float)jNode["position"]["x"], (float)jNode["position"]["y"]);

				baseNode = new DivisionNode<int>(id, position);

				jInput = new JObject(new JProperty("idParent", id), new JProperty("inputs", jNode["inputs"]));
			}
			else if ((string)jNode["type"] == typeof(DebugLogNode).ToString())
			{
				int id = (int)jNode["id"];
				Vector2 position = new Vector2((float)jNode["position"]["x"], (float)jNode["position"]["y"]);

				baseNode = new DebugLogNode(id, position);

				((DebugLogNode)baseNode).log = (string)jNode["variable"];

				jInput = new JObject(new JProperty("idParent", id), new JProperty("inputs", jNode["inputs"]));
			}
			else if ((string)jNode["type"] == typeof(EventNode).ToString())
			{
				int id = (int)jNode["id"];
				Vector2 position = new Vector2((float)jNode["position"]["x"], (float)jNode["position"]["y"]);

				baseNode = new EventNode(id, position);

				jInput = new JObject(new JProperty("idParent", id), new JProperty("inputs", jNode["inputs"]));
			}

			return baseNode;
		}
	}
}
