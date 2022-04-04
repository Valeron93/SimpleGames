using SFML.Graphics;
using SFML.System;

namespace Snake;

public class SnakeGame
{
    public int Score { get; private set; }
    
    private bool     _food;
    private Vector2i _foodPosition;

    private readonly Snake  _snake;
    private readonly Grid   _grid;
    private readonly Random _random;

    private static readonly Color SnakeColor = Color.Black;
    private static readonly Color FoodColor  = Color.Red;


    public SnakeGame(Grid grid)
    {
        _grid = grid;
        
        _random = new Random();
        
        _snake = new Snake();
        _snake.Create();
    }

    public void RestartGame()
    {
        Score = 0;
        _grid.Clear(Color.White);
        _snake.Create();
        _food = false;
    }
    
    public void DrawSnake()
    {
        _grid.Clear(Color.White); 
        
        _grid.SetColor(_foodPosition, FoodColor);
        
        if (_snake.Cells.Count == 0)
            return;
        
        foreach (var point in _snake.Cells)
            _grid.SetColor(point, SnakeColor);
        
        _grid.Draw();
    }

    public void MoveSnake(Vector2i movement)
    {
        
        if (_snake.Cells.Count == 0) return;

        var head = MoveHead(_snake.Cells[0], movement);

        if (_snake.Cells.Contains(head))
        {
            GameOver();
            return;
        }
        
        if (head == _foodPosition)
        {
            _snake.Cells.Insert(0, _foodPosition);
            _food = false;
            
            ++Score;
        }
        
        for (var i = _snake.Cells.Count - 1; i > 0; --i)
        {
            _snake.Cells[i] = _snake.Cells[i - 1];
        }
        
        _snake.Cells[0] = head;
    }
    
    public void SpawnFood()
    {
        if (_food) return;
        
        Vector2i randomCell;

        do
        {
            randomCell = RandomCell();
        } while (_snake.Cells.Contains(randomCell));

        _foodPosition = randomCell;
        _food = true;
    }
    
    private Vector2i MoveHead(Vector2i position, Vector2i movement)
    {
        var dx = position.X + movement.X;
        var dy = position.Y + movement.Y;

        dx = dx == _grid.GridSize.X ? 0 : dx == -1 ? _grid.GridSize.X - 1 : dx;
        dy = dy == _grid.GridSize.Y ? 0 : dy == -1 ? _grid.GridSize.Y - 1 : dy;

        return new Vector2i(dx, dy);
    }
    
    private Vector2i RandomCell()
    {
        var x = _random.Next(0, _grid.GridSize.X);
        var y = _random.Next(0, _grid.GridSize.Y);

        return new Vector2i(x, y);
    }
    
    private void GameOver()
    {
        _snake.Cells.Clear();
    }
    
    
}