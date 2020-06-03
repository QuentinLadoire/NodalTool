using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV4
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
		[MenuItem("Tools/GraphEditorV4")]
		public static void ShowWindow()
		{
			m_window = GetWindow<GraphEditor>("GraphEditorV4");
			m_window.Show();
		}

		//All
		string m_extensionFile = "json";

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
			m_graph = new Graph();
			m_nodes = m_graph.m_nodes;
		}
		void SaveGraph()
		{

		}
		void SaveAsGraph()
		{
			string path = EditorUtility.SaveFilePanel("Save Graph as...", Application.dataPath, "", m_extensionFile);
			Graph.SaveToFile(m_graph, path);
		}
		void LoadGraph()
		{
			string path = EditorUtility.OpenFilePanelWithFilters("Load graph", Application.dataPath, new string[] { "", m_extensionFile });
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

		public Vector2 m_offSetView = Vector2.zero;

		public static void GetLastRect(ref Rect rect, Vector2 offSet)
		{
			if (m_window.m_event.type == EventType.Repaint)
			{
				rect = GUILayoutUtility.GetLastRect();
				rect.position += offSet;
			}
		}

		public BasePin m_clickedInput = null;
		public BasePin m_clickedOutput = null;

		public static BasePin clickedInput
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
		public static BasePin clickedOutput
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
			m_nodes.Add(new VariableNode<int>(tmpIdCount++, m_mousePosition - m_offSetView));
		}
		void CreateAdditionIntNode()
		{
			m_nodes.Add(new AdditionNode<int>(tmpIdCount++, m_mousePosition - m_offSetView));
		}
		void CreateSubstractionIntNode()
		{
			m_nodes.Add(new SubstractionNode<int>(tmpIdCount++, m_mousePosition - m_offSetView));
		}
		void CreateMultiplicationIntNode()
		{
			m_nodes.Add(new MultiplicationNode<int>(tmpIdCount++, m_mousePosition - m_offSetView));
		}
		void CreateDivisionIntNode()
		{
			m_nodes.Add(new DivisionNode<int>(tmpIdCount++, m_mousePosition - m_offSetView));
		}
		void CreateEventNode()
		{
			m_nodes.Add(new EventNode(tmpIdCount++, m_mousePosition - m_offSetView));
		}
		void CreateEventTestNode()
		{
			m_nodes.Add(new DebugLogNode(tmpIdCount++, m_mousePosition - m_offSetView));
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
		int tmpIdCount = 0;
		Graph m_graph = null;
		List<BaseNode> m_nodes = null;
		BaseNode m_clickedNode = null;
		void DrawNodes()
		{
			if (m_nodes != null)
			{
				BeginWindows();
				for (int i = 0; i < m_nodes.Count; i++)
				{
					BaseNode node = m_nodes[i];
					node.id = i;

					node.position += m_offSetView;
					
					node.rect = GUI.Window(node.id, node.rect, node.DrawNode, node.title);

					//draw curve for all outputPin
					foreach (OutputPin output in node.m_outputs)
					{
						output.DrawCurve(m_offSetView);
					}

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

		private void Awake()
		{
			m_graph = new Graph();
			m_nodes = m_graph.m_nodes;

			m_createNodesMenu.AddItem(new GUIContent("Int Node"), false, CreateIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Addition Int Node"), false, CreateAdditionIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Substraction Int Node"), false, CreateSubstractionIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Multiplication Int Node"), false, CreateMultiplicationIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Division Int Node"), false, CreateDivisionIntNode);
			m_createNodesMenu.AddItem(new GUIContent("Event Node"), false, CreateEventNode);
			m_createNodesMenu.AddItem(new GUIContent("DebugLog Node"), false, CreateEventTestNode);

			m_nodeMenu.AddItem(new GUIContent("Delete Node"), false, DeleteNode);
		}
		private void OnGUI()
		{
			m_event = Event.current;
			m_mousePosition = m_event.mousePosition;


			//Move view
			if (m_event.button == 0 && m_event.type == EventType.MouseDrag)
			{
				if (GetClickedNode() == null)
				{
					m_offSetView += m_event.delta;
				}
			}

			//Opening a creating node menu
			if (m_event.button == 1 && m_event.type == EventType.MouseUp)
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
				if (m_clickedOutput.IsCompatiblePin(m_clickedInput.GetPinType()))
				{
					m_clickedOutput.Connect(m_clickedInput);

					m_clickedOutput = null;
					m_clickedInput = null;
				}
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
				m_clickedInput.Deconnect(out m_clickedOutput);
				m_clickedInput = null;
			}

			//Display the current curve
			if (m_clickedOutput != null)
			{
				((OutputPin)m_clickedOutput).DrawCurve(m_offSetView, m_mousePosition);
			}

			DrawNodes();

			DrawMainMenu();

			Repaint();
		}
	}
}
