using System.Collections;
using System.Collections.Generic;

namespace NodalToolsV3
{
	public class NodeUtility
	{
		public static bool ConnectNode(Input input, Output output)
		{
			if (output.m_parentNode != input.m_parentNode)
			{
				if (input.m_parentNode.CompatibleType(output.GetValue()))
				{
					if (input.m_output != null)
					{
						input.m_output.m_inputs.Remove(input);
						input.m_output = null;
					}

					input.m_output = output;
					output.m_inputs.Add(input);

					return true;
				}
			}

			return false;
		}
	}
}
