using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV4
{
	public abstract class InputPin : BasePin, ISerializable
	{
		public OutputPin m_output = null;

		public abstract string Serialize();
	}

	public class InputPin<T> : InputPin
	{
		//BasePin
		public override void Connect(BasePin otherPin)
		{
			//if same node return
			if (m_parentNode == otherPin.m_parentNode) return;

			//if already connect together return
			var output = otherPin as OutputPin<T>;
			if (output == m_output) return;

			//if i am connect, then deconnect
			if (m_output != null)
			{
				m_output.Deconnect();
			}

			//connect
			m_output = output;
			output.Connect(this);
		}
		public override void Deconnect()
		{
			if (m_output != null)
			{
				m_output.m_inputs.Remove(this);
				m_output = null;
			}
		}
		public override void Deconnect(out BasePin connectedPin)
		{
			connectedPin = m_output;
			Deconnect();
		}
		public override Type GetPinType()
		{
			return typeof(T);
		}
		public override bool IsCompatiblePin(Type typeToCompare)
		{
			return (typeToCompare == typeof(T));
		}

		public override string Serialize()
		{
			throw new NotImplementedException();
		}
	}
}
