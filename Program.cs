using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleGame
{
    class Program
    {
        static Player player { get; set; }
        public static List<Wall> walls { get; private set; }

        static void Main(string[] args)
        {
            walls = new List<Wall>();

            // Make walls on the top and bottom of the map
            for (int x = 0; x < 10; x++)
            {  
                walls.Add(new Wall(x, 0));
                walls.Add(new Wall(x, 10));
            }

            // Make walls on the left and right of the map
            for (int y = 0; y < 10; y++)
            {
                walls.Add(new Wall(0, y));
                walls.Add(new Wall(10, y));
            }

            // Create player
            player = new Player(1, 1, 1, 1, "Henk");
            
            GameLoop();

            Console.WriteLine("The game has stopped");
            Console.ReadKey();
        }

        // GameLoop
        // Check for input for player movement
        // Move player accoring to input
        static void GameLoop() {
            while (true) {
                // Print player
                Print();

                // Get input
                var key = Console.ReadKey();

                // Check input
                // Move player
                if (key.Key == ConsoleKey.W) {
                    player.moveUp();
                }
                else if (key.Key == ConsoleKey.S) {
                    player.moveDown();
                }
                else if (key.Key == ConsoleKey.A) {
                    player.moveLeft();
                }
                else if (key.Key == ConsoleKey.D) {
                    player.moveRight();
                }
                else if (key.Key == ConsoleKey.Delete) {
                    break;
                }
            }
        }

        // Print
        // Print player
        // Print walls
        static void Print() {
            Console.Clear();

            // Print player
            PrintPlayer();

            // Print walls
            PrintWalls();
        }


        // Print player
        // Set console cursor to player X and Y
        static void PrintPlayer() {
            Console.SetCursorPosition(player.x, player.y);
            Console.Write("$");
        }

        // Print walls

        static void PrintWalls() {
            foreach (var wall in walls) {
                // Set console cursor to wall X and Y
                Console.SetCursorPosition(wall.x, wall.y);
                Console.Write("#");
            }
        }
    }

    // Player class
    // Has X and Y
    // Has a speed
    // Has a health
    // Has a name

    class Player {
        public int x { get; set; }
        public int y { get; set; }
        public int speed { get; set; }
        public int health { get; private set; }
        public string name { get; private set; }

        public Player(int x, int y, int speed, int health, string name)
        {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.health = health;
            this.name = name;
        }

        public void damage(int damage) {
            if (damage < 0)
                return;

            health -= damage;
        }

        // Is colliding with a wall
        public bool isCollidingWithWall(int x, int y) {
            // Check if wall is on x and y
            return Program.walls.Any(wall => wall.x == x && wall.y == y);
        }

        // Move up
        public void moveUp() {
            // Check if we can move up
            if (y - speed < 0)
                return;

            if (isCollidingWithWall(x, y - speed))
                return;

            y -= speed;
        }

        // Move down
        public void moveDown() {
            // Check if we can move down
            if (y + speed > 10)
                return;

            if (isCollidingWithWall(x, y + speed))
                return;

            y += speed;
        }

        // Move left
        public void moveLeft() {
            // Check if we can move left
            if (x - speed < 0)
                return;

            if (isCollidingWithWall(x - speed, y))
                return;

            x -= speed;
        }

        // Move right
        public void moveRight() {
            // Check if we can move right
            if (x + speed > 10)
                return;

            if (isCollidingWithWall(x + speed, y))
                return;
                
            x += speed;
        }
    }

    // Wall class
    // Has X and Y

    class Wall {
        public int x { get; set; }
        public int y { get; set; }

        public Wall(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
