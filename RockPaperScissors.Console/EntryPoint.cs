using RockPaperScissors.Console;
using RockPaperScissors.Console.UserInterface;

GameManager gameManager = new();
gameManager.StartGame(UserInterfaceMode.Console, 5);
// Exit statement
Console.WriteLine("Press enter to exit");
Console.ReadLine();
