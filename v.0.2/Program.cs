

using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;

Rectangle field = new Rectangle(40, 10, 30, 15);

Point vector = new Point(1, 0);

Collection<Point> snake = new Collection<Point>();
snake.Add(new Point(0, 0));
snake.Add(new Point(1, 0));
snake.Add(new Point(2, 0));
snake.Add(new Point(3, 0));
Point location = new Point(3, 0);


Console.CursorVisible = false;

while (true)
{
    //change vector if key availble
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo ch = new ConsoleKeyInfo();

        while (Console.KeyAvailable)
        {
            ch = Console.ReadKey();
        }

        if (ch.Key == ConsoleKey.UpArrow)
            vector = new Point(0, -1);
        else if (ch.Key == ConsoleKey.DownArrow)
            vector = new Point(0, 1);
        else if (ch.Key == ConsoleKey.LeftArrow)
            vector = new Point(-1, 0);
        else if (ch.Key == ConsoleKey.RightArrow)
            vector = new Point(1, 0);


    }


    location.X += vector.X * 1;
    location.Y += vector.Y * 1;

    snake.Add(location);

    snake = new Collection<Point>(snake.TakeLast(snake.Count() - 1).ToList());
    

    //check for END game
    Point physicLocation = new Point(0, 0);
    physicLocation = new Point(field.X + location.X, field.Y + location.Y);


    if (!field.Contains(physicLocation))
    {
        Console.Beep();
        location = new Point(0, 0);
        snake.Clear();
        snake.Add(new Point(0, 0));
        snake.Add(new Point(1, 0));
        snake.Add(new Point(2, 0));
        snake.Add(new Point(3, 0));

        vector = new Point(1, 0);
        location = new Point(3, 0);
    }


    //Out
    Console.Clear();

    RenderField();
    RenderZmeyka();



    Thread.Sleep(500);
}


void RenderField()
{
    //top border
    Console.SetCursorPosition(field.X - 1, field.Y - 1);
    for (int i = 0; i < field.Width + 2; i++)
    {
        Console.Write('-');
    }

    //left border
    for (int j = 0; j < field.Height; j++)
    {
        int x = field.X - 1;
        int y = field.Y + j * 1;

        Console.SetCursorPosition(x, y);
        Console.Write('|');
    }

    //right border
    for (int j = 0; j < field.Height; j++)
    {
        int x = field.X + field.Width;
        int y = field.Y + j * 1;

        Console.SetCursorPosition(x, y);
        Console.Write('|');
    }

    //bottom border
    Console.SetCursorPosition(field.X - 1, field.Y + field.Height);
    for (int i = 0; i < field.Width + 2; i++)
    {
        Console.Write('-');
    }

}

void RenderZmeyka()
{
    foreach (Point p in snake)
    {
        Point l = new Point(0, 0);
        l = new Point(field.X + p.X, field.Y + p.Y);

        Console.SetCursorPosition(l.X, l.Y);
        Console.Write('+');
    }
}

