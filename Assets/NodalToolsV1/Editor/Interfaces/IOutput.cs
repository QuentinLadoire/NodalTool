using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV1
{
	public interface IOutput<T>
	{
		T GetValue();
	}
}
