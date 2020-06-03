using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV3
{
	enum MainMenuType
	{
		None = -1,
		New,
		Save,
		SaveAs,
		Load
	}

	public class GraphEditor : EditorWindow
	{
		public static GraphEditor m_window;
		[MenuItem("Tools/GraphEditorV3")]
		public static void ShowWindow()
		{
			m_window = GetWindow<GraphEditor>("GraphEditorV3");
			m_window.Show();
		}

		//All
		public Event m_event = null;
		public Vector2 m_mousePosition = default;

		public static Event currentEvent
		{
			get
			{
				return m_window.m_event;
			}
		}
		public static Vector2 mousePosition
		{
			get
			{
				return m_window.m_mousePosition;
			}
		}

		string[] m_mainMenu = new string[] 
		{
			"New",
			"Save",
			"Save As...",
			"Load"
		};
		void NewGraph()
		{
			m_nodes = null;
			m_graph = null;
		}
		void SaveGraph()
		{

		}
		void SaveAsGraph()
		{
			string path = EditorUtility.SaveFilePanel("Save Graph as...", Application.dataPath, "", "graph");
			Graph.SaveToFile(m_graph, path);
		}
		void LoadGraph()
		{
			string path = EditorUtility.OpenFilePanelWithFilters("Load graph", Application.dataPath, new string[] { "", "graph" });
			m_graph = Graph.LoadFromFile(path);
			if (m_graph != null)
			{
				m_nodes = m_graph.m_nodes;
			}
		}
		void DrawMainMenu()
		{
			switch ((MainMenuType)GUILayout.Toolbar(-1, m_mainMenu))
			{
				case MainMenuType.New:
					NewGraph();
					break;

				case MainMenuType.Save:
					Debug.Log("Save");
					SaveGraph();
					break;

				case MainMenuType.SaveAs:
					SaveAsGraph();
					break;

				case MainMenuType.Load:
					LoadGraph();
					break;
			}
		}

		Vector2 m_offSetView = Vector2.zero;

		public static void GetLastRect(ref Rect rect, Vector2 offSet)
		{
			if (m_window.m_event.type == EventType.Repaint)
			{
				rect = GUILayoutUtility.GetLastRect();
				rect.position += offSet;
			}
		}

		public Input m_clickedInput = null;
		public Output m_clickedOutput = null;

		public static Input clickedInput
		{
			set
			{
				m_window.m_clickedInput = value;
			}
			get
			{
				return m_window.m_clickedInput;
			}
		}
		public static Output clickedOutput
		{
			set
			{
				m_window.m_clickedOutput = value;
			}
			get
			{
				return m_window.m_clickedOutput;
			}
		}
		//

		
		//NodeMenu
		GenericMenu m_createNodesMenu = new GenericMenu();
		void CreateIntNode()
		{
			m_nodes.Add(new IntNode(m_nodes.Count, m_mousePosition - m_offSetView));
		}
		void CreateAdditionIntNode()
		{
			m_nodes.Add(new AdditionIntNode(m_nodes.Count, m_mousePosition - m_offSetView));
		}
		void CreateSubstractionIntNode()
		{
			m_nodes.Add(new SubstractionIntNode(m_nodes.Count, m_mousePosition - m_offSetView));
		}
		void CreateMultiplicationIntNode()
		{
			m_nodes.Add(new MultiplicationIntNode(m_nodes.Count, m_mousePosition - m_offSetView));
		}
		void CreateDivisionIntNode()
		{
			m_nodes.Add(new DivisionIntNode(m_nodes.Count, m_mousePosition - m_offSetView));
		}

		GenericMenu m_nodeMenu = new GenericMenu();
		void DeleteNode()
		{
			if (m_clickedNode != null)
			{
				m_clickedNode.Remove();
				m_nodes.Remove(m_clickedNode);

				m_clickedNode = null;
			}
		}
		//

		//Nodes
		Graph m_graph = new Graph();
		List<BaseNode> m_nodes = new List<BaseNode>();
		BaseNode m_clickedNode = null;
		void DrawNodes()
		{
			if (m_nodes != null)
			{
				BeginWindows();
				foreach (var node in m_nodes)
				{
					node.position += m_offSetView;

					node.rect = GUI.Window(node.id, node.rect, node.Draw, node.title);
					node.DrawCurve(m_offSetView);

					node.position -= m_offSetView;
				}
				EndWindows();
			}
		}
		//


		BaseNode GetClickedNode()
		{
			if (m_nodes != null)
			{
				BaseNode clickedNode = null;
				foreach (var node in m_nodes)
				{
					Rect tmp = node.rect;
					tmp.position += m_offSetView;
					if (tmp.Contains(m_mousePosition))
					{
						clickedNode = node;
						break;
					}
				}

				return clickedNode;
			}

			return null;
		}
		void DeconnectInputOutput()
		{
			if (m_clickedInput.m_output != null)
			{
				m_clickedInput.m_output.m_inputs.Remove(m_clickedInput);
				m_clickedOutput = m_clickedInput.m_output;
				m_clickedInput.m_output = null;
			}
		}

		private void Awake()
		{
			m_createNodesMenu.AddItem(new GUIContent("Int Node"), false, CreateIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Addition Int Node"), false, CreateAdditionIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Substraction Int Node"), false, CreateSubstractionIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Multiplication Int Node"), false, CreateMultiplicationIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Division Int Node"), false, CreateDivisionIntNode);

			m_nodeMenu.AddItem(new GUIContent("Delete Node"), false, DeleteNode);
		}
		private void OnGUI()
		{
			m_event = Event.current;
			m_mousePosition = m_event.mousePosition;

			DrawNodes();

			DrawMainMenu();

			Repaint();

			//Move view
			if (m_event.button == 0 && m_event.type == EventType.MouseDrag)
			{
				if (GetClickedNode() == null)
				{
					m_offSetView += m_event.delta;
				}
			}

			//Opening a creating node menu
			if (m_event.button == 1 && m_event.type == EventType.MouseDown)
			{
				m_clickedNode = GetClickedNode();
				if (m_clickedNode == null)
				{
					m_createNodesMenu.ShowAsContext();
				}
				else
				{
					m_nodeMenu.ShowAsContext();
				}
			}

			//Connect a output/input
			if (m_clickedOutput != null && m_clickedInput != null)
			{
				NodeUtility.ConnectNode(m_clickedInput, m_clickedOutput);

				m_clickedOutput = null;
				m_clickedInput = null;
			}

			//Remove de current m_clickedOutput
			if (m_event.button == 0 && m_event.type == EventType.MouseDown)
			{
				if (GetClickedNode() == null && m_clickedOutput != null)
				{
					m_clickedOutput = null;
				}
			}

			//Deconnect a input/output
			if (m_clickedInput != null)
			{
				DeconnectInputOutput();

				m_clickedInput = null;
			}

			//Display the current curve
			if (m_clickedOutput != null)
			{
				Rect rect = m_clickedOutput.m_rect;

				Vector3 startPosition = rect.position + Vector2.Scale(rect.size, new Vector2(1.0f, 0.5f)) + m_offSetView;
				Vector3 endPosition = m_mousePosition;

				Vector3 startTangente = startPosition + Vector3.right * 50;
				Vector3 endTangente = endPosition + Vector3.left * 50;

				Handles.DrawBezier(startPosition, endPosition, startTangente, endTangente, Color.black, null, 2);
			}
		}
	}
}
