

using System.Collections;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;

Rectangle field = new Rectangle(40, 10, 30, 15);

Point vector = new Point(1, 0);

Collection<Point> snake = new Collection<Point>();
snake.Add(new Point(0, 0));
snake.Add(new Point(1, 0));
snake.Add(new Point(2, 0));
snake.Add(new Point(3, 0));
Point location = new Point(3, 0);


Point apple = new Point(new Random().Next() % field.Width, new Random().Next() % field.Height);
Point time = new Point(new Random().Next() % field.Width, new Random().Next() % field.Height);
float timeScale = 1f;
DateTime timeStarted = DateTime.MinValue;

Point hole1 = new Point(new Random().Next() % field.Width, new Random().Next() % field.Height);
Point hole2 = new Point(new Random().Next() % field.Width, new Random().Next() % field.Height);
ConsoleColor holeColor = ConsoleColor.White;

Console.CursorVisible = false;
Console.OutputEncoding = Encoding.Unicode;

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



    //hole
    if(location == hole1)
    {
        location.X = hole2.X + vector.X * 1;
        location.Y = hole2.Y + vector.Y * 1;
    }

    if(location == hole2)
    {
        location.X = hole1.X + vector.X * 1;
        location.Y = hole1.Y + vector.Y * 1;
    }



    snake.Add(location);

    //



    //check for END game
    bool isEndOfGame = false;

    Point physicLocation = new Point(0, 0);
    physicLocation = new Point(field.X + location.X, field.Y + location.Y);


    if (!field.Contains(physicLocation))
    {
        isEndOfGame = true;
    }

    foreach (Point p in snake.Take(snake.Count() - 1))
    {
        if (p == location)
            isEndOfGame = true;
    }

    if (isEndOfGame)
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

        continue;
    }

    //
    if (apple == location)
    {
        apple = new Point(new Random().Next() % field.Width, new Random().Next() % field.Height);
    }
    else if (time == location)
    {
        timeScale = (new Random().Next(0, 2) == 0 ? 0.7f : 2f);
        timeStarted = DateTime.Now;

        snake = new Collection<Point>(snake.TakeLast(snake.Count() - 1).ToList());

        time = new Point(new Random().Next() % field.Width, new Random().Next() % field.Height);

    }
    else
    {
        snake = new Collection<Point>(snake.TakeLast(snake.Count() - 1).ToList());
    }



    //Out
    Console.Clear();

    RenderField();
    RenderZmeyka();

    DateTime t1 = timeStarted;
    DateTime t2 = timeStarted.AddSeconds(5);
    DateTime t3 = DateTime.Now;


    if (timeStarted.AddSeconds(5) < DateTime.Now)
    {
        timeScale = 1;
    }

    Thread.Sleep((int)(500 * timeScale));
}



void RenderField()
{
    //apple
    Console.SetCursorPosition(field.X + apple.X, field.Y + apple.Y);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write('@');
    Console.ForegroundColor = ConsoleColor.Gray;

    //time scale
    Console.SetCursorPosition(field.X + time.X, field.Y + time.Y);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write('T');
    Console.ForegroundColor = ConsoleColor.Gray;



    //black hole
    holeColor = (holeColor == ConsoleColor.White ? ConsoleColor.DarkGray : ConsoleColor.White);

    Console.SetCursorPosition(field.X + hole1.X, field.Y + hole1.Y);
    Console.ForegroundColor = holeColor;
    Console.Write('o');
    Console.ForegroundColor = ConsoleColor.Gray;

    
    Console.SetCursorPosition(field.X + hole2.X, field.Y + hole2.Y);
    Console.ForegroundColor = holeColor;
    Console.Write('o');
    Console.ForegroundColor = ConsoleColor.Gray;



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

