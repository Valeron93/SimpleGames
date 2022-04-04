namespace GameOfLife;

internal static class Program
{
    private const string Title = "Game of Life";

    private static void Main()
    {
        var gameWindow = new GameWindow(1920, 1080, 20, Title);
        gameWindow.Show();
    }
}