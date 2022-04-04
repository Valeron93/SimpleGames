using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Snake;

public class Grid
{
    public Vector2i GridSize { get; }
    private int CellSize { get; }

    private readonly Color[,] _grid;
    private readonly Vector2i _offset;
    private readonly RenderWindow _window;
    
    public Grid(Vector2i size, int cellSize, RenderWindow window)
    {
        GridSize = size;
        CellSize = cellSize;
        _grid = new Color[GridSize.X, GridSize.Y];
        _window = window;
        _offset = GetOffset();
    }

    public void SetColor(int x, int y, Color color) => _grid[x, y] = color;
    public void SetColor(Vector2i pos, Color color) => _grid[pos.X, pos.Y] = color;
    public Color GetColor(int x, int y) => _grid[x, y];
    public Color GetColor(Vector2i pos) => _grid[pos.X, pos.Y];


    public void Draw()
    {
        var square = new RectangleShape(new Vector2f(CellSize, CellSize));

        square.OutlineColor = Color.Black;
        square.OutlineThickness = 1;
        
        for (var x = 0; x < GridSize.X; ++x)
        {
            for (var y = 0; y < GridSize.Y; ++y)
            {
                var position = new Vector2f(_offset.X + x * CellSize, _offset.Y + y * CellSize);
                square.Position = position;
                square.FillColor = _grid[x, y];
                _window.Draw(square);
            }
        }
    }

    public void Clear(Color color)
    {
        for (var x = 0; x < GridSize.X; ++x)
        {
            for (var y = 0; y < GridSize.Y; ++y)
            {
                SetColor(x, y, color);
            }
        }
    }

    private Vector2i GetOffset()
    {
        var windowSize = _window.Size;
        var gridSize = new Vector2i(GridSize.X * CellSize, GridSize.Y * CellSize);

        var offset = new Vector2i(
            (int)(windowSize.X - gridSize.X) / 2,
            (int)(windowSize.Y - gridSize.Y) / 2
        );
        return offset;
    }
}