using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV1
{
	public class GraphEditor : EditorWindow
	{
		static GraphEditor m_window;
		[MenuItem("Tools/GraphEditorV1")]
		public static void ShowWindow()
		{
			m_window = GetWindow<GraphEditor>("GraphEditorV1");
			m_window.Show();
		}

		//All
		Event m_event = null;
		Vector2 m_mousePosition = default;
		//

		//Debug
		bool m_makeTransition = false;
		//

		//NodeMenu
		GenericMenu m_createNodesMenu = new GenericMenu();
		void CreateIntNode()
		{
			m_nodes.Add(new IntNode(m_nodes.Count, m_event.mousePosition));
		}
		void CreateAdditionIntNode()
		{
			m_nodes.Add(new AdditionIntNode(m_nodes.Count, m_event.mousePosition));
		}

		GenericMenu m_nodesMenu = new GenericMenu();
		void MakeTransition()
		{
			m_makeTransition = true;
		}
		void DeleteNode()
		{

		}
		
		void ShowMenu()
		{
			if (m_event.button == 1 && m_event.type == EventType.MouseDown)
			{
				bool isContains = false;
				foreach (var node in m_nodes)
				{
					if (node.rect.Contains(m_mousePosition))
					{
						m_selectedNode = node;
						isContains = true;
						break;
					}
				}

				if (isContains)
				{
					m_nodesMenu.ShowAsContext();
				}
				else
				{
					m_createNodesMenu.ShowAsContext();
				}
			}
		}
		//

		//Nodes
		BaseNode m_selectedNode = null;
		List<BaseNode> m_nodes = new List<BaseNode>();
		void DrawNodes()
		{
			BeginWindows();
			foreach (var node in m_nodes)
			{
				node.rect = GUI.Window(node.id, node.rect, node.Draw, node.title);
			}
			EndWindows();
		}
		//

		private void Awake()
		{
			m_createNodesMenu.AddItem(new GUIContent("IntNode"), false, CreateIntNode);
			m_createNodesMenu.AddItem(new GUIContent("AdditionIntNode"), false, CreateAdditionIntNode);

			m_nodesMenu.AddItem(new GUIContent("Make Transition"), false, MakeTransition);
		}
		private void OnGUI()
		{
			m_event = Event.current;
			m_mousePosition = m_event.mousePosition;

			ShowMenu();
			DrawNodes();

			if (m_makeTransition)
			{
				if (m_event.button == 0 && m_event.type == EventType.MouseDown)
				{
					foreach (var node in m_nodes)
					{
						if (node.rect.Contains(m_mousePosition))
						{
							var iInput = node as IInput;
							iInput.SetInput(m_selectedNode, iInput.GetIndexInput(m_mousePosition));
							m_makeTransition = false;
							break;
						}
					}
				}

				Repaint();
			}
		}
	}
}
