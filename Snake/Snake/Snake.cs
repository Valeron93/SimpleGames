using SFML.System;

namespace Snake;

public class Snake
{
    public static readonly Vector2i Up    = new (0, -1);
    public static readonly Vector2i Down  = new (0, 1);
    public static readonly Vector2i Left  = new (-1, 0);
    public static readonly Vector2i Right = new (1, 0);

    public List<Vector2i> Cells;

    public Snake()
    {
        Cells = new List<Vector2i>();
    }
    
    public void Create()
    {
        Cells.Clear();
        Cells = new List<Vector2i>
        {
            new (6, 5),
            new (5, 5),
            new (4, 5)
        };
    }

}