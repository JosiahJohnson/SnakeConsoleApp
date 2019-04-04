using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
	class Apple
	{
		public Position Position { get; set; }
		private int GridWidth;
		private int GridHeight;

		public Apple(int gridWidth, int gridHeight, Snake snake)
		{
			GridWidth = gridWidth;
			GridHeight = gridHeight;
			RandomizePosition(snake);
		}

		public void RandomizePosition(Snake snake)
		{
			Position pos = new Position();

			do
			{
				pos.X = GetRandomNumber(1, GridWidth - 2);
				pos.Y = GetRandomNumber(1, GridHeight - 2);
			} while ((pos.X % 2 == 0) || IsOnSnakePosition(pos, snake));//no even numbers on x axis since snake moves 2 horizontal spaces at a time

			Position = pos;
		}

		private int GetRandomNumber(int min, int max)
		{
			Random random = new Random();
			return random.Next(min, max);
		}

		private bool IsOnSnakePosition(Position pos, Snake snake)
		{
			bool overlap = false;

			foreach (Position sPos in snake.Positions)
			{
				if (pos.X == sPos.X && pos.Y == sPos.Y)
				{
					overlap = true;
					break;
				}
			}

			return overlap;
		}
	}
}
