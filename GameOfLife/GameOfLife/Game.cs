using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GameOfLife;

public class Game
{
    private int FieldWidth { get; }
    private int FieldHeigth { get; }

    private readonly int _squareSize;
    private readonly bool[,] _field;
    private readonly bool[,] _oldField;
    private readonly VideoMode _mode;
    
    public Game(VideoMode mode, int squareSize)
    {

        _mode = mode;
        _squareSize = squareSize;
        
        FieldWidth  = (int) mode.Width  / squareSize;
        FieldHeigth = (int) mode.Height / squareSize;
        
        _field    = new bool[FieldWidth, FieldHeigth];
        _oldField = new bool[FieldWidth, FieldHeigth];

    }

    public void SetCell(int x, int y, bool value) => _field[x, y] = value;
    public bool GetCell(int x, int y) => _field[x, y];

    public void DrawField(RenderWindow window)
    {

        var square = new RectangleShape(new Vector2f(_squareSize, _squareSize));
        
        square.OutlineColor = Color.Black;
        square.OutlineThickness = 1f;
        square.FillColor = Color.White;
        
        for (var x = 0; x < FieldWidth; ++x)
        {
            for (var y = 0; y < FieldHeigth; ++y)
            {
                square.Position = new Vector2f(
                    _squareSize * x + (_mode.Width  % _squareSize) / 2,
                    _squareSize * y + (_mode.Height % _squareSize) / 2
                );

                square.FillColor = _field[x, y] ? Color.Black : Color.White;
                window.Draw(square);
            }
        }
    }
    
    public void NewGeneration()
    {
        SwapField();
        for (var x = 0; x < FieldWidth; ++x)
        {
            for (var y = 0; y < FieldHeigth; ++y)
            {
                var score = GetScore(x, y);
                
                if (_oldField[x, y] && (score == 2 || score == 3))
                    continue;
                
                _field[x, y] = (!_oldField[x, y]) && score == 3;
            }
        }
    }

    private void SwapField()
    {
        for (var x = 0; x < FieldWidth; ++x)
        {
            for (var y = 0; y < FieldHeigth; ++y)
            {
                _oldField[x, y] = _field[x, y];
            }
        }
    }
    

    private int GetScore(int x, int y)
    {
        var score = 0;

        for (var dx = -1; dx <= 1; ++dx)
        {
            for (var dy = -1; dy <= 1; ++dy)
            {
                if (dx == 0 && dy == 0) continue;

                var posX = x + dx;
                var posY = y + dy;

                posX = posX == FieldWidth  ? 0 : posX == -1 ? FieldWidth  - 1 : posX;
                posY = posY == FieldHeigth ? 0 : posY == -1 ? FieldHeigth - 1 : posY;

                score += _oldField[posX, posY] ? 1 : 0;
            } 
        }
        return score;
    }
    
}