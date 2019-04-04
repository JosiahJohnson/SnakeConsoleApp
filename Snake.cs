using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
	class Snake
	{
		public Direction Direction { get; set; }
		public Position Head
		{
			get { return Positions[0]; }
		}
		public Position Tail
		{
			get { return Positions[Positions.Count - 1]; }
		}
		public List<Position> Positions { get; set; }

		public Snake()
		{
			Initialize();
		}

		public void Initialize()
		{
			Direction = Direction.Right;
			Positions = new List<Position>();
			Positions.Add(new Position(5, 1));
			Positions.Add(new Position(3, 1));
			Positions.Add(new Position(1, 1));
		}

		public string GetHeadCharacter()
		{
			string head = ">";

			switch (Direction)
			{
				case Direction.Up: head = "^"; break;
				case Direction.Down: head = "v"; break;
				case Direction.Left: head = "<"; break;
				case Direction.Right: head = ">"; break;
			}

			return head;
		}

		public bool AteSelf()
		{
			bool ateSelf = false;

			for (int i = 1; i < Positions.Count; i++)
			{
				if (Head.X == Positions[i].X && Head.Y == Positions[i].Y)
				{
					ateSelf = true;
					break;
				}
			}

			return ateSelf;
		}
	}
}
