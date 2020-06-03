using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NodalToolsV1
{
	public abstract class BaseNode : ScriptableObject
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

		public BaseNode(int id, Vector2 position, string title = "BaseNode")
		{
			m_id = id;
			m_title = title;
			m_rect.position = position;
			m_rect.size = new Vector2(100, 100);
		}
		
		virtual public void Draw(int id)
		{
			GUI.DragWindow(new Rect(Vector2.zero, new Vector2(m_rect.width, 15)));
		}
	}
}
