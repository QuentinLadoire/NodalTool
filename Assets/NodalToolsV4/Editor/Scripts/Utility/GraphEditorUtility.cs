using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV4
{
	public static class GraphEditorUtility
	{
		public static void DrawCurve(this OutputPin outputPin, Vector2 offSet)
		{
			Rect outputPinRect = outputPin.m_rect;
			Vector3 startPosition = outputPinRect.position + Vector2.Scale(outputPinRect.size, new Vector2(1.0f, 0.5f)) + offSet;
			Vector3 startTangente = startPosition + Vector3.right * 50;

			foreach (var inputPin in outputPin.m_inputs)
			{
				Rect inputPinRect = inputPin.m_rect;
				Vector3 endPosition = inputPinRect.position + Vector2.Scale(inputPinRect.size, new Vector2(0.0f, 0.5f)) + offSet;
				Vector3 endTangente = endPosition + Vector3.left * 50;

				Handles.DrawBezier(startPosition, endPosition, startTangente, endTangente, Color.black, null, 2);
			}
		}
		public static void DrawCurve(this OutputPin outputPin, Vector2 offSet, Vector2 targetPosition)
		{
			Rect rect = outputPin.m_rect;

			Vector3 startPosition = rect.position + Vector2.Scale(rect.size, new Vector2(1.0f, 0.5f)) + offSet;
			Vector3 endPosition = targetPosition;

			Vector3 startTangente = startPosition + Vector3.right * 50;
			Vector3 endTangente = endPosition + Vector3.left * 50;

			Handles.DrawBezier(startPosition, endPosition, startTangente, endTangente, Color.black, null, 2);
		}

		public static void DrawNode(this BaseNode baseNode, int id)
		{
			GUI.DragWindow(new Rect(Vector2.zero, new Vector2(baseNode.rect.width, 15)));
			
			if (baseNode is VariableNode<int>)
			{
				var node = baseNode as VariableNode<int>;

				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Name :");
				node.m_variableName = EditorGUILayout.TextField(node.m_variableName);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Value : ");
				node.variable = EditorGUILayout.IntField(node.variable);

				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedOutput == null)
					{
						GraphEditor.clickedOutput = node.m_outputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_outputs[0].m_rect, node.rect.position);
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();
			}
			else if (baseNode is AdditionNode<int>)
			{
				var node = baseNode as AdditionNode<int>;

				GUILayout.BeginHorizontal();

				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[0].m_rect, node.rect.position);
				GUILayout.Label("Variable1");
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[1];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[1].m_rect, node.rect.position);
				GUILayout.Label("Variable2");
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();

				GUILayout.BeginHorizontal();
				//GUILayout.Label("Result : " + ((OutputPin<int>)node.m_outputs[0]).GetValue());
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedOutput == null)
					{
						GraphEditor.clickedOutput = node.m_outputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_outputs[0].m_rect, node.rect.position);
				GUILayout.EndHorizontal();

				GUILayout.EndHorizontal();
			}
			else if (baseNode is SubstractionNode<int>)
			{
				var node = baseNode as SubstractionNode<int>;

				GUILayout.BeginHorizontal();

				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[0].m_rect, node.rect.position);
				GUILayout.Label("Variable1");
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[1];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[1].m_rect, node.rect.position);
				GUILayout.Label("Variable2");
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();

				GUILayout.BeginHorizontal();
				//GUILayout.Label("Result : " + ((OutputPin<int>)node.m_outputs[0]).GetValue());
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedOutput == null)
					{
						GraphEditor.clickedOutput = node.m_outputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_outputs[0].m_rect, node.rect.position);
				GUILayout.EndHorizontal();

				GUILayout.EndHorizontal();
			}
			else if (baseNode is MultiplicationNode<int>)
			{
				var node = baseNode as MultiplicationNode<int>;

				GUILayout.BeginHorizontal();

				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[0].m_rect, node.rect.position);
				GUILayout.Label("Variable1");
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[1];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[1].m_rect, node.rect.position);
				GUILayout.Label("Variable2");
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();

				GUILayout.BeginHorizontal();
				//GUILayout.Label("Result : " + ((OutputPin<int>)node.m_outputs[0]).GetValue());
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedOutput == null)
					{
						GraphEditor.clickedOutput = node.m_outputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_outputs[0].m_rect, node.rect.position);
				GUILayout.EndHorizontal();

				GUILayout.EndHorizontal();
			}
			else if (baseNode is DivisionNode<int>)
			{
				var node = baseNode as DivisionNode<int>;

				GUILayout.BeginHorizontal();

				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[0].m_rect, node.rect.position);
				GUILayout.Label("Variable1");
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[1];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[1].m_rect, node.rect.position);
				GUILayout.Label("Variable2");
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();

				GUILayout.BeginHorizontal();
				//GUILayout.Label("Result : " + ((OutputPin<int>)node.m_outputs[0]).GetValue());
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedOutput == null)
					{
						GraphEditor.clickedOutput = node.m_outputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_outputs[0].m_rect, node.rect.position);
				GUILayout.EndHorizontal();

				GUILayout.EndHorizontal();
			}
			else if (baseNode is DebugLogNode)
			{
				var node = baseNode as DebugLogNode;

				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("=>"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[0].m_rect, node.rect.position);

				if (GUILayout.Button("=>"))
				{
					if (GraphEditor.clickedOutput == null)
					{
						GraphEditor.clickedOutput = node.m_outputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_outputs[0].m_rect, node.rect.position);
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				if (GUILayout.Button("O"))
				{
					if (GraphEditor.clickedInput == null)
					{
						GraphEditor.clickedInput = node.m_inputs[1];
					}
				}
				GraphEditor.GetLastRect(ref node.m_inputs[1].m_rect, node.rect.position);

				node.log = GUILayout.TextField(node.log);
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();
			}
			else if (baseNode is EventNode)
			{
				var node = baseNode as EventNode;

				GUILayout.BeginVertical();

				GUILayout.BeginHorizontal();
				GUILayout.Label("Event :");
				if (GUILayout.Button("=>"))
				{
					if (GraphEditor.clickedOutput == null)
					{
						GraphEditor.clickedOutput = node.m_outputs[0];
					}
				}
				GraphEditor.GetLastRect(ref node.m_outputs[0].m_rect, node.rect.position);
				GUILayout.EndHorizontal();

				GUILayout.EndVertical();
			}
		}
	}
}
