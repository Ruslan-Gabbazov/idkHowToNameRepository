using Task6ParallelismAndMultithreading;

Console.WriteLine("Приложение запущено");
while (true)
{
    Console.WriteLine("Введите текст запроса. Для выхода введите /exit");
    var command = Console.ReadLine();

    if (command == "/exit") 
        break;

    Console.WriteLine($"Будет послано сообщение '{command}'");
    Console.WriteLine("Введите аргументы сообщения. Если аргументы не нужны - введите /end");
    
    var message = command;
    command = Console.ReadLine();
    
    var arguments = new List<string>();
    while (true)
    {
        if (command == "/end")
            break;
        
        arguments.Add(command ?? string.Empty);
        
        Console.WriteLine("Введите следующий аргумент сообщения. Если аргументы не нужны /end");
        command = Console.ReadLine();
    }

    ThreadPool.QueueUserWorkItem(_ => CallHandler(message ?? string.Empty, arguments.ToArray()));
}
Console.WriteLine("Приложение завершает работу");


static void CallHandler(string message, string[] arguments)
{
    var messageId = Guid.NewGuid().ToString("D");
    var handler = new DummyRequestHandler();

    Console.WriteLine($"Было отправлено сообщение '{message}'. " +
                      $"Присвоен индентефикатор: {messageId}");
    try
    {
        Console.WriteLine($"Сообщение с индентификатором '{messageId}' " +
                          $"получило ответ - {handler.HandleRequest(message, arguments)}");
    }
    catch (Exception exception)
    {
        Console.WriteLine($"Сообщение с индентификатором '{messageId}' " +
                          $"упало с ошибкой: {exception.Message}");
    }
}
