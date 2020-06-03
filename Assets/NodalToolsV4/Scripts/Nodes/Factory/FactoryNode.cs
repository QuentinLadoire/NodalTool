using UnityEngine;

namespace NodalToolsV4
{
	public class FactoryNode
	{
		public enum OperatorType
		{
			Addition,
			Substraction,
			Multiplication,
			Division
		}

		public static BaseNode CreateVariableNode<T>(int id, Vector2 position, string title)
		{
			return new VariableNode<T>(id, position, title);
		}
		public static BaseNode CreateOperatorNode<T>(OperatorType type, int id, Vector2 position, string title)
		{
			if (type == OperatorType.Addition)
			{
				return new AdditionNode<T>(id, position, title);
			}
			else if (type == OperatorType.Substraction)
			{
				return new SubstractionNode<T>(id, position, title);
			}
			else if (type == OperatorType.Multiplication)
			{
				return new MultiplicationNode<T>(id, position, title);
			}
			else if (type == OperatorType.Division)
			{
				return new DivisionNode<T>(id, position, title);
			}

			return null;
		}
	}
}
