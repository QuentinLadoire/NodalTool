using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NodalToolsV3
{
	public class Graph
	{
		public List<BaseNode> m_nodes = new List<BaseNode>();

		/// <summary>
		/// returns false if graph is null, path is null or empty, otherwise, true.
		/// </summary>
		static public bool SaveToFile(Graph graph, string path)
		{
			if (graph != null && !string.IsNullOrEmpty(path))
			{
				JObject allData = new JObject(
					new JProperty("nodes", new JArray(
						from node in graph.m_nodes
						orderby node.id
						select JObject.Parse(node.Serialize())
						))
					);

				StreamWriter sw = File.CreateText(path);
				sw.Write(allData.ToString());
				sw.Close();

				return true;
			}

			return false;
		}
		/// <summary>
		/// return null if path is null or empty, file doesnt exists, otherwise, true. 
		/// </summary>
		static public Graph LoadFromFile(string path)
		{
			if (!string.IsNullOrEmpty(path) && File.Exists(path))
			{
				Graph newGraph = new Graph();

				StreamReader sr = File.OpenText(path);
				JObject allData = JObject.Parse(sr.ReadToEnd());
				sr.Close();

				List<JObject> jinputs = new List<JObject>();
				foreach (var node in allData["nodes"])
				{
					if ((string)node["type"] == "IntNode")
					{
						int id = (int)node["id"];
						Vector2 position = new Vector2((float)node["position"]["x"], (float)node["position"]["y"]);
						string variableName = (string)node["variableName"];
						int variable = (int)node["variable"];

						IntNode intNode = new IntNode(id, position);
						intNode.m_variable = variable;
						intNode.m_variableName = variableName;
						newGraph.m_nodes.Add(intNode);
					}
					else if ((string)node["type"] == "AdditionIntNode")
					{
						int id = (int)node["id"];
						Vector2 position = new Vector2((float)node["position"]["x"], (float)node["position"]["y"]);

						AdditionIntNode additionIntNode = new AdditionIntNode(id, position);
						newGraph.m_nodes.Add(additionIntNode);

						jinputs.Add(new JObject(new JProperty("idParent", id), new JProperty("inputs", node["inputs"])));
					}
					else if ((string)node["type"] == "SubstractionIntNode")
					{
						int id = (int)node["id"];
						Vector2 position = new Vector2((float)node["position"]["x"], (float)node["position"]["y"]);

						SubstractionIntNode additionIntNode = new SubstractionIntNode(id, position);
						newGraph.m_nodes.Add(additionIntNode);

						jinputs.Add(new JObject(new JProperty("idParent", id), new JProperty("inputs", node["inputs"])));
					}
					else if ((string)node["type"] == "MultiplicationIntNode")
					{
						int id = (int)node["id"];
						Vector2 position = new Vector2((float)node["position"]["x"], (float)node["position"]["y"]);

						MultiplicationIntNode additionIntNode = new MultiplicationIntNode(id, position);
						newGraph.m_nodes.Add(additionIntNode);

						jinputs.Add(new JObject(new JProperty("idParent", id), new JProperty("inputs", node["inputs"])));
					}
					else if ((string)node["type"] == "DivisionIntNode")
					{
						int id = (int)node["id"];
						Vector2 position = new Vector2((float)node["position"]["x"], (float)node["position"]["y"]);

						DivisionIntNode additionIntNode = new DivisionIntNode(id, position);
						newGraph.m_nodes.Add(additionIntNode);

						jinputs.Add(new JObject(new JProperty("idParent", id), new JProperty("inputs", node["inputs"])));
					}
				}

				foreach (var jinput in jinputs)
				{
					int idParent = (int)jinput["idParent"];
					foreach (var input in jinput["inputs"])
					{
						int indexInput = (int)input["index"];

						int idNode = (int)input["output"]["idNode"];
						int indexOutput = (int)input["output"]["index"];

						NodeUtility.ConnectNode(newGraph.m_nodes[idParent].m_inputs[indexInput], newGraph.m_nodes[idNode].m_outputs[indexOutput]);
					}
				}

				return newGraph;
			}

			return null;
		}
	}
}
