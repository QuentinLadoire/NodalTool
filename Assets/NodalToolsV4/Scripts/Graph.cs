using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace NodalToolsV4
{
	[System.Serializable]
	public class Graph
	{
		public static string project = "NodalTools";
		public static string version = "4.2";

		public List<BaseNode> m_nodes = new List<BaseNode>();

		public OnEvent GetEvent()
		{
			foreach (var node in m_nodes)
			{
				if (node is EventNode)
				{
					return ((EventNode)node).OnEvent;
				}
			}

			return null;
		}

		/// <summary>
		/// returns false if graph is null, path is null or empty, otherwise, true.
		/// </summary>
		public static bool SaveToFile(Graph graph, string path)
		{
			if (graph != null && !string.IsNullOrEmpty(path))
			{
				JObject allData = new JObject(
					new JProperty("project", project),
					new JProperty("version", version),
					new JProperty("nodes", 
						new JArray(
							from node in graph.m_nodes
							orderby node.id
							select JObject.Parse(node.Serialize())
						)
					)
				);

				StreamWriter sw = File.CreateText(path);
				sw.Write(allData.ToString());
				sw.Close();

				return true;
			}

			return false;
		}
		/// <summary>
		/// return null if path is null or empty, file doesnt exists, otherwise, a new graph. 
		/// </summary>
		public static Graph LoadFromFile(string path)
		{
			if (!string.IsNullOrEmpty(path) && File.Exists(path))
			{
				//get data in file 
				StreamReader sr = File.OpenText(path);
				string allData = sr.ReadToEnd();
				sr.Close();

				Graph newGraph = LoadFromText(allData);
				if (newGraph != null)
				{
					return newGraph;
				}
			}

			return null;
		}
		/// <summary>
		/// return null if text is null or empty, otherwise, a new graph.
		/// </summary>
		public static Graph LoadFromText(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				JObject allData = JObject.Parse(text);

				JToken jProject = allData["project"];
				JToken jVersion = allData["version"];
				if (jProject.ToString() != project || jVersion.ToString() != version)
				{
					Debug.Log("This text is not compatible with this version of nodalTools !!!");
					return null;
				}

				Graph newGraph = new Graph();

				//data processing
				List<JObject> jInputs = new List<JObject>();
				foreach (var node in allData["nodes"])
				{
					JObject jInput;
					BaseNode newNode = NodeUtility.DeserializeNode(node, out jInput);
					if (newNode != null)
					{
						newGraph.m_nodes.Add(newNode);

						//if input exist, save the value for connection after
						if (jInput != null)
						{
							jInputs.Add(jInput);
						}
					}
				}

				//connect all nodes
				foreach (var jinput in jInputs)
				{
					int idParent = (int)jinput["idParent"];
					foreach (var input in jinput["inputs"])
					{
						int indexInput = (int)input["index"];

						int idNode = (int)input["output"]["idNode"];
						int indexOutput = (int)input["output"]["index"];

						newGraph.m_nodes[idParent].m_inputs[indexInput].Connect(newGraph.m_nodes[idNode].m_outputs[indexOutput]);
					}
				}

				return newGraph;
			}

			return null;
		}
	}
}
