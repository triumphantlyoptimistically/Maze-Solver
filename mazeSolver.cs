using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Solver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string maze = "########\n#S....##\n#.##.#E#\n#......#\n########";
            string[] lines = maze.Trim().Split('\n');
            int rows = lines.Length;
            int columns = lines[0].Length;
            char[,] cells = new char[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    cells[i, j] = lines[i][j];
                }
            }
            (int, int) start = (-1, -1);
            bool[,] visited = new bool[rows, columns];
            List<(int, int)> path = new List<(int, int)>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (cells[i, j] == 'S')
                    {
                        start = (i, j);
                    }
                }
            }
            if (DFS(cells, rows, columns, start.Item1, start.Item2, visited, path))
            {
                foreach ((int, int) point in path)
                {
                    if (cells[point.Item1, point.Item2] != 'S'&& cells[point.Item1, point.Item2] != 'E')
                    {
                        cells[point.Item1, point.Item2] = '*';
                    }
                }
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Console.Write(cells[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No path found");
            }
        }

        static bool DFS(char[,] cells, int rows, int columns, int row, int col, bool[,] visited, List<(int, int)> path)
        {
            if (row < 0 || row >= rows || col < 0 || col >= columns)
            {
                return false;
            }
            else if (cells[row, col] == '#')
            {
                return false;
            }
            else if (visited[row, col])
            {
                return false;
            }
            else if (cells[row, col] == 'E')
            {
                path.Add((row, col));
                return true;
            }
            visited[row, col] = true;
            path.Add((row, col));
            if (DFS(cells, rows, columns, row - 1, col, visited, path))
            {
                return true;
            }
            if (DFS(cells, rows, columns, row + 1, col, visited, path))
            {
                return true;
            }
            if (DFS(cells, rows, columns, row, col - 1, visited, path))
            {
                return true;
            }
            if (DFS(cells, rows, columns, row, col + 1, visited, path))
            {
                return true;
            }
            path.RemoveAt(path.Count - 1);
            return false;
        }
    }
}
