using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV4
{
	public abstract class OutputPin : BasePin
	{
		public List<InputPin> m_inputs = new List<InputPin>();
	}

	public class OutputPin<T> : OutputPin
	{
		OnGetValue m_onGetValue = () => { };
		public OnGetValue onGetValue
		{
			get => m_onGetValue;
			set => m_onGetValue = value;
		}

		T m_pinValue;
		public T GetPinValue()
		{
			m_onGetValue();
			return m_pinValue;
		}
		public void SetPinValue(T value)
		{
			m_pinValue = value;
		}

		//BasePin
		public override void Connect(BasePin otherPin)
		{
			//if same node return
			if (m_parentNode == otherPin.m_parentNode) return;

			//if already connect together return
			var input = otherPin as InputPin<T>;
			if (m_inputs.Contains(input)) return;

			//connect
			m_inputs.Add(input);
			input.Connect(this);
		}
		public override void Deconnect()
		{
			if (m_inputs.Count == 0) return;

			for (int i = 0; i < m_inputs.Count; i++)
			{
				if (m_inputs[i] != null)
				{
					m_inputs[i].m_output = null;
					m_inputs[i] = null;
				}
			}

			m_inputs.RemoveAll(input => input == null);
		}
		public override void Deconnect(out BasePin connectedPin)
		{
			connectedPin = null;
			Debug.LogWarning("This pin : " + GetType().Name + "isn't a inputPin.");
		}
		public override Type GetPinType()
		{
			return typeof(T);
		}
		public override bool IsCompatiblePin(Type typeToCompare)
		{
			return (typeToCompare == typeof(T));
		}
	}
}
