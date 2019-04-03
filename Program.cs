using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static int gridWidth = 60, gridHeight = 25;
		static Snake snake = new Snake();

		class Snake
		{
			public Direction Direction { get; set; }
			public Position Position { get; set; }
			public List<Position> Tail { get; set; }

			public Snake()
			{
				Direction = Direction.Right;
				Position = new Position(3, 1);
				Tail = new List<Position>();
				Tail.Add(new Position(2, 1));
				Tail.Add(new Position(1, 1));
			}
		}

		class Position
		{
			public int X { get; set; }
			public int Y { get; set; }

			public Position(int x, int y)
			{
				X = x;
				Y = y;
			}
		}

		enum Direction
		{
			Up, Down, Left, Right
		}

		static void Main(string[] args)
		{
			while (true)
			{
				Console.Clear();
				CheckUserInput();
				ChangeSnakePosition();
				DisplayOutput();
				System.Threading.Thread.Sleep(100);
			}
		}

		static void CheckUserInput()
		{
			if (Console.KeyAvailable)
			{
				var action = Console.ReadKey(true);

				switch (action.Key)
				{
					case ConsoleKey.UpArrow: snake.Direction = Direction.Up; break;
					case ConsoleKey.DownArrow: snake.Direction = Direction.Down; break;
					case ConsoleKey.LeftArrow: snake.Direction = Direction.Left; break;
					case ConsoleKey.RightArrow: snake.Direction = Direction.Right; break;
				}
			}
		}

		static void ChangeSnakePosition()
		{
			Position origPos = new Position(snake.Position.X, snake.Position.Y);

			switch (snake.Direction)
			{
				case Direction.Up:
					if (snake.Position.Y > 1)
						snake.Position.Y--;
					break;
				case Direction.Down:
					if (snake.Position.Y < (gridHeight - 2))
						snake.Position.Y++;
					break;
				case Direction.Left:
					if (snake.Position.X > 1)
						snake.Position.X--;
					break;
				case Direction.Right:
					if (snake.Position.X < (gridWidth - 2))
						snake.Position.X++;
					break;
			}

			snake.Tail[0].X = origPos.X;
			snake.Tail[0].Y = origPos.Y;
		}

		static void DisplayOutput()
		{
			string output = "";

			for (int y = 0; y < gridHeight; y++)
			{
				for (int x = 0; x < gridWidth; x++)
				{
					if (y == 0 || y == (gridHeight - 1))
						output += "=";
					else if (x == 0 || x == (gridWidth - 1))
						output += "|";
					else if (x == snake.Position.X && y == snake.Position.Y)
						output += "X";
					else
					{
						bool tailFound = false;
						foreach (Position pos in snake.Tail)
						{
							if (x == pos.X && y == pos.Y)
							{
								output += "O";
								tailFound = true;
								break;
							}
						}

						if (!tailFound)
							output += " ";
					}
				}

				output += Environment.NewLine;
			}

			Console.Write(output);
		}
	}
}
