using SFML.Graphics;
using SFML.Window;

namespace GameOfLife;

public class GameWindow
{
    private readonly RenderWindow _window;
    private readonly Game _game;
    private readonly string _title;
    private readonly int _squareSize;
    
    private bool _pause = true;
    private uint _framerate = 5;
    private uint Framerate
    {
        get => _framerate;
        set => _framerate = value > 0 ? value : 1;
    }
    
    public GameWindow(uint width, uint height, int squareSize, string title)
    {
        var videoMode = new VideoMode(width, height);
        _squareSize = squareSize;
        
        _window = new RenderWindow(videoMode, "", Styles.Close | Styles.Titlebar);
        _game = new Game(videoMode, squareSize);
        
        _title = title;
        UpdateTitle();
    }
    
    public void Show()
    {
        _window.SetFramerateLimit(60);

        _window.Closed             += (_, _) => _window.Close();
        _window.KeyPressed         += KeyPressed;
        _window.MouseButtonPressed += MouseButtonPressed;

        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            
            _window.Clear(Color.White);
            
            if (!_pause)
                _game.NewGeneration();
            
            _game.DrawField(_window);
            
            _window.Display();
        }
    }
    
    private void KeyPressed(object? sender, KeyEventArgs eventArgs)
    {
        switch (eventArgs.Code)
        {
            case Keyboard.Key.LBracket:
                Framerate -= 1;
                break;
            case Keyboard.Key.RBracket:
                Framerate += 1;
                break;
            case Keyboard.Key.Space:
                _pause = !_pause;
                break;
        }
        
        _window.SetFramerateLimit(_pause ? 60 : Framerate);
        
        UpdateTitle();
    }
    
    private void MouseButtonPressed(object? sender, MouseButtonEventArgs eventArgs)
    {
        switch (eventArgs.Button)
        {
            case Mouse.Button.Left:
                if (!_pause) return;

                var posX = eventArgs.X / _squareSize;
                var posY = eventArgs.Y / _squareSize;

                _game.SetCell(posX, posY, !_game.GetCell(posX, posY));
                break;
        }
    }
    
    private void UpdateTitle() => _window.SetTitle($"{_title}  FPS: {Framerate}{(_pause ? " - PAUSED" : "")}");
    
}