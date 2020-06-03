using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace NodalToolsV4
{
	public class GraphUser : MonoBehaviour
	{
		public OnEvent onStart = () => { };
		public OnEvent onUpdate = () => { };

		[SerializeField] TextAsset m_graphAsset = null;
		Graph m_graph = null;

		private void Awake()
		{
			m_graph = Graph.LoadFromText(m_graphAsset.text);

			if (m_graph != null)
			{
				onStart = m_graph.GetEvent();
			}
		}
		private void Start()
		{
			onStart();
		}
		private void Update()
		{
			onUpdate();
		}
	}
}
