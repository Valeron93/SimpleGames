using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Snake;

public class GameWindow
{
    private const string FontName = "font.ttf";
    private const int Framerate = 8;

    private readonly VideoMode _mode = new (1280, 800);
    
    private readonly RenderWindow _window;
    private readonly Grid _grid;
    private readonly SnakeGame _game;
    
    private readonly Text _scoreText;
    
    private Vector2i _moveVector = Snake.Right;
    
    public GameWindow()
    {
        var font = new Font(FontName);
        
        _scoreText = new Text("", font);
        _scoreText.CharacterSize = 30;
        _scoreText.FillColor = Color.Black;
        _scoreText.Position = new Vector2f(30, 360);
        
        _window = new RenderWindow(_mode, "Snake", Styles.Close | Styles.Titlebar);
        _window.SetFramerateLimit(Framerate);
        _grid = new Grid(new Vector2i(30, 30), 25, _window);
        _game = new SnakeGame(_grid);
    }

    public void Show()
    {
        _window.Closed += (_, _) => _window.Close();
        _window.KeyPressed += KeyPressed;

        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            _window.Clear(Color.White);
            GameCycle();
            _window.Display();
        }
    }

    private void KeyPressed(object? sender, KeyEventArgs eventArgs)
    {
        switch (eventArgs.Code)
        {
            case Keyboard.Key.A:
                _moveVector = _moveVector == Snake.Right ? _moveVector : Snake.Left;
                break;
            case Keyboard.Key.D:
                _moveVector = _moveVector == Snake.Left ? _moveVector : Snake.Right;
                break;
            case Keyboard.Key.W:
                _moveVector = _moveVector == Snake.Down ? _moveVector : Snake.Up;
                break;
            case Keyboard.Key.S:
                _moveVector = _moveVector == Snake.Up ? _moveVector : Snake.Down;
                break;
            
            case Keyboard.Key.Space:
                _moveVector = Snake.Right;
                _game.RestartGame();
                break;
            
        }
    }

    private void GameCycle()
    {
        _scoreText.DisplayedString = $"SCORE: {_game.Score}";
        _window.Draw(_scoreText);

        _game.SpawnFood();
        _game.MoveSnake(_moveVector);
        _game.DrawSnake();
        _grid.Draw();
    }
    

}