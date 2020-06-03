using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV4
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
			set
			{
				m_id = value;
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

		public List<InputPin> m_inputs = new List<InputPin>();
		public List<OutputPin> m_outputs = new List<OutputPin>();

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
				output.Deconnect();
			}

			//Deconnect input
			foreach (var input in m_inputs)
			{
				input.Deconnect();
			}
		}

		public abstract string Serialize();
	}
}
