using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV2
{
	public class GraphEditor : EditorWindow
	{
		public static GraphEditor m_window;
		[MenuItem("Tools/GraphEditorV2")]
		public static void ShowWindow()
		{
			m_window = GetWindow<GraphEditor>("GraphEditorV2");
			m_window.Show();
		}

		//All
		public static Event m_event = null;
		public static Vector2 m_mousePosition = default;
		Vector2 m_offSetView = Vector2.zero;

		public static void GetLastRect(ref Rect rect, Vector2 offSet)
		{
			if (m_event.type == EventType.Repaint)
			{
				rect = GUILayoutUtility.GetLastRect();
				rect.position += offSet;
			}
		}
		//

		public static Input m_clickedInput = null;
		public static Output m_clickedOutput = null;

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
		List<BaseNode> m_nodes = new List<BaseNode>();
		BaseNode m_clickedNode = null;
		void DrawNodes()
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
		//

		
		BaseNode GetClickedNode()
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
		void ConnectInputOutput()
		{
			if (m_clickedOutput.m_parentNode != m_clickedInput.m_parentNode)
			{
				if (m_clickedInput.m_parentNode.CompatibleType(m_clickedOutput.GetValue()))
				{
					if (m_clickedInput.m_output != null)
					{
						m_clickedInput.m_output.m_inputs.Remove(m_clickedInput);
						m_clickedInput.m_output = null;
					}

					m_clickedInput.m_output = m_clickedOutput;
					m_clickedOutput.m_inputs.Add(m_clickedInput);
				}
			}
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

			Repaint();

			//Move view
			if (m_event.button == 0 && m_event.type == EventType.MouseDrag)
			{
				if (GetClickedNode() == null)
				{
					m_offSetView += m_event.delta;
				}
			}

			//opening a creating node menu
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
				ConnectInputOutput();

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
