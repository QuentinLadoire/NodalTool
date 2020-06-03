using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV4
{
	public abstract class BasePin
	{
		public BaseNode m_parentNode = null;

		public Rect m_rect;

		public abstract void Connect(BasePin otherPin);

		public abstract void Deconnect();
		public abstract void Deconnect(out BasePin connectedPin);

		public abstract Type GetPinType();

		public abstract bool IsCompatiblePin(Type typeToCompare);
	}
}
