using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV1
{
	public abstract class OperatorNode<T> : BaseNode, IOutput<T>, IInput
	{
		public OperatorNode(int id, Vector2 position, string title = "OperatorNode") : base(id, position, title)
		{
		}

		protected List<IOutput<T>> m_inputs = new List<IOutput<T>>();
		protected List<Rect> m_inputRects = new List<Rect>();

		public abstract T GetValue();

		public abstract void SetInput(object input, int index);

		public abstract int GetIndexInput(Vector3 position);
	}
}
