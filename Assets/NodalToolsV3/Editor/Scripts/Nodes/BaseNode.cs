using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV3
{
	public abstract class BaseNode : ISerializable
	{
		protected int m_id;
		protected string m_title;
		protected Rect m_rect;

		public int id
		{
			get
			{
				return m_id;
			}
		}
		public string title
		{
			get
			{
				return m_title;
			}
		}
		public Rect rect
		{
			get
			{
				return m_rect;
			}
			set
			{
				m_rect.position = value.position;
			}
		}
		public Vector2 position
		{
			set
			{
				m_rect.position = value;
			}
			get
			{
				return m_rect.position;
			}
		}
		public Vector2 size
		{
			set
			{
				m_rect.size = value;
			}
			get
			{
				return m_rect.size;
			}
		}

		public List<Input> m_inputs = new List<Input>();
		public List<Output> m_outputs = new List<Output>();

		public BaseNode(int id, Vector2 position, string title = "BaseNode")
		{
			m_id = id;
			m_title = title;
			m_rect.position = position;
			m_rect.size = new Vector2(100, 100);
		}

		public virtual void Remove()
		{
			//Deconnect output
			foreach (var output in m_outputs)
			{
				for (int i = 0; i < output.m_inputs.Count; i++)
				{
					if (output.m_inputs[i] != null)
					{
						output.m_inputs[i].m_output = null;
						output.m_inputs[i] = null;
					}
				}

				output.m_inputs.Clear();
			}

			//Deconnect input
			foreach (var input in m_inputs)
			{
				if (input.m_output != null)
				{
					input.m_output.m_inputs.Remove(input);
					input.m_output = null;
				}
			}
		}

		abstract public bool CompatibleType(object typeToCompare);

		virtual public void Draw(int id)
		{
			GUI.DragWindow(new Rect(Vector2.zero, new Vector2(m_rect.width, 15)));
		}
		public void DrawCurve(Vector2 offSet)
		{
			foreach (var output in m_outputs)
			{
				Rect outputRect = output.m_rect;
				Vector3 startPosition = outputRect.position + Vector2.Scale(outputRect.size, new Vector2(1.0f, 0.5f)) + offSet;
				Vector3 startTangente = startPosition + Vector3.right * 50;

				foreach (var input in output.m_inputs)
				{
					Rect inputRect = input.m_rect;
					Vector3 endPosition = inputRect.position + Vector2.Scale(inputRect.size, new Vector2(0.0f, 0.5f)) + offSet;
					Vector3 endTangente = endPosition + Vector3.left * 50;

					Handles.DrawBezier(startPosition, endPosition, startTangente, endTangente, Color.black, null, 2);
				}
			}
		}

		public virtual string Serialize()
		{
			return null;
		}
	}
}
