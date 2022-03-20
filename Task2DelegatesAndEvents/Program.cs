// Task 2: events. BARS Edu

var keyHandler = new KeyHandler();
keyHandler.OnKeyPressed += PrintMessage;
keyHandler.Run();

void PrintMessage(object? sender, char character)
{
    Console.WriteLine($"\n'{character}' is typed.");
}

public class KeyHandler
{
    public event EventHandler<char>? OnKeyPressed;
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Type some character: ");
            var key = Console.ReadKey().KeyChar;
            if (key == 'c')
            {
                Console.WriteLine("\n'c' is pressed. Exit from the loop...");
                break;
            }
            else
                OnKeyPressed?.Invoke(this, key);
        }
    }
}