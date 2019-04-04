using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
	class Program
	{
		static int gridWidth = 30, gridHeight = 15;
		static Snake snake = new Snake();
		static Apple apple = new Apple(gridWidth, gridHeight, snake);
		static int score = 0;
		static bool gameOver = false;

		static void Main(string[] args)
		{
			while (true)
			{
				while (!gameOver)
				{
					Console.Clear();
					CheckUserInput();
					ChangeSnakePosition();
					DisplayOutput();
					System.Threading.Thread.Sleep(GetSpeed());
				}

				Console.WriteLine("GAME OVER MAN!!");
				Console.WriteLine("Press Enter to try again.");
				Console.ReadLine();

				gameOver = false;
				score = 0;
				snake.Initialize();
				apple.RandomizePosition(snake);
			}
		}

		static void CheckUserInput()
		{
			if (Console.KeyAvailable)
			{
				var action = Console.ReadKey(true);

				switch (action.Key)
				{
					case ConsoleKey.UpArrow:
						if (snake.Direction != Direction.Down)
							snake.Direction = Direction.Up;
						break;
					case ConsoleKey.DownArrow:
						if (snake.Direction != Direction.Up)
							snake.Direction = Direction.Down;
						break;
					case ConsoleKey.LeftArrow:
						if (snake.Direction != Direction.Right)
							snake.Direction = Direction.Left;
						break;
					case ConsoleKey.RightArrow:
						if (snake.Direction != Direction.Left)
							snake.Direction = Direction.Right;
						break;
				}
			}
		}

		static void ChangeSnakePosition()
		{
			Position lastPos = new Position(snake.Tail.X, snake.Tail.Y);

			//set tail positions
			for (int i = (snake.Positions.Count - 1); i > 0; i--)
			{
				snake.Positions[i].X = snake.Positions[i - 1].X;
				snake.Positions[i].Y = snake.Positions[i - 1].Y;
			}

			//set head position
			switch (snake.Direction)
			{
				case Direction.Up:
					snake.Head.Y--;
					break;
				case Direction.Down:
					snake.Head.Y++;
					break;
				case Direction.Left:
					snake.Head.X -= 2;
					break;
				case Direction.Right:
					snake.Head.X += 2;
					break;
			}

			//check if ate apple
			if (snake.Head.X == apple.Position.X && snake.Head.Y == apple.Position.Y)
			{
				snake.Positions.Add(lastPos);
				apple.RandomizePosition(snake);
				score += 10;
			}

			//check for collisions
			if (snake.Head.X < 1 || snake.Head.X > (gridWidth - 2) || snake.Head.Y < 1 || snake.Head.Y > (gridHeight - 2) || snake.AteSelf())
				gameOver = true;
		}

		static void DisplayOutput()
		{
			string output = "Score: " + score + Environment.NewLine;

			for (int y = 0; y < gridHeight; y++)
			{
				for (int x = 0; x < gridWidth; x++)
				{
					if (y == 0 || y == (gridHeight - 1))
						output += "=";
					else if (x == 0 || x == (gridWidth - 1))
						output += "|";
					else if (x == apple.Position.X && y == apple.Position.Y)
						output += "@";
					else if (x == snake.Head.X && y == snake.Head.Y)
						output += snake.GetHeadCharacter();
					else
					{
						bool tailFound = false;
						foreach (Position pos in snake.Positions)
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

		static int GetSpeed()
		{
			int speed = 200;

			speed -= (score / 2);

			if (speed < 75)
				speed = 75;

			return speed;
		}
	}

	enum Direction
	{
		Up, Down, Left, Right
	}

	class Position
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Position()
		{ }

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
