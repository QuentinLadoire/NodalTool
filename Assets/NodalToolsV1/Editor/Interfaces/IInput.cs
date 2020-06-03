using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodalToolsV1
{
	public interface IInput
	{
		void SetInput(object input, int index);

		int GetIndexInput(Vector3 position);
	}
}
