using RockPaperScissors.Console;
using RockPaperScissors.Console.UserInterface;

GameManager gameManager = new();
gameManager.StartGame(UserInterfaceMode.Console);

Console.WriteLine("Please enter your choice (rock, paper, or scissors):");

string? userChoice = Console.ReadLine();

Console.WriteLine($"You have chosen {userChoice}!");

// Exit statement
Console.WriteLine("Press enter to exit");
Console.ReadLine();
