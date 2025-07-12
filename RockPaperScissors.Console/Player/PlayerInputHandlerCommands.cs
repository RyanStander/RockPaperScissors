using RockPaperScissors.Console.AI;

namespace RockPaperScissors.Console.Player;

/// <summary>
/// This class handles player inputs through the console using command strings.
/// </summary>
public class PlayerInputHandlerCommands : PlayerInputHandler
{
    #region Command Definitions

    private static readonly string[] rockCommands = { "rock", "r" };
    private static readonly string[] paperCommands = { "paper", "p" };
    private static readonly string[] scissorsCommands = { "scissors", "s" };
    private static readonly string[] randomCommands = { "random", "r" };
    private static readonly string[] easyCommands = { "easy", "e" };
    private static readonly string[] hardCommands = { "hard", "h" };
    private static readonly string[] quitCommands = { "quit", "exit" };
    private static readonly string[] continueCommands = { "continue", "resume" };

    #endregion

    public override Hand SelectHand()
    {
        while (true)
        {
            string? command = System.Console.ReadLine()?.ToLowerInvariant().Trim();

            if (string.IsNullOrEmpty(command))
            {
                System.Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            if (MatchesCommand(rockCommands, command)) return Hand.Rock;
            if (MatchesCommand(paperCommands, command)) return Hand.Paper;
            if (MatchesCommand(scissorsCommands, command)) return Hand.Scissors;
            if (MatchesCommand(quitCommands, command)) Quit();

            System.Console.WriteLine($"Couldn't recognise: {command}. Please try again.");
        }
    }

    public override Difficulty SelectDifficulty()
    {
        string? command = System.Console.ReadLine()?.ToLowerInvariant().Trim();

        if (string.IsNullOrEmpty(command))
        {
            System.Console.WriteLine("Invalid input. Please try again.");
            return SelectDifficulty();
        }

        if (MatchesCommand(randomCommands, command)) return Difficulty.Random;
        if (MatchesCommand(easyCommands, command)) return Difficulty.Easy;
        if (MatchesCommand(hardCommands, command)) return Difficulty.Hard;

        System.Console.WriteLine($"Couldn't recognise: {command}. Please try again.");
        return SelectDifficulty();
    }

    protected override void Quit()
    {
        System.Console.WriteLine("Thank you for playing! Goodbye!");
        Thread.Sleep(2000);
        Environment.Exit(0);
    }

    public override void ContinueGame()
    {
        while (true)
        {
            string? command = System.Console.ReadLine()?.ToLowerInvariant().Trim();

            if (string.IsNullOrEmpty(command))
            {
                System.Console.WriteLine("Invalid input. Please either select 'continue' or 'quit'.");
                continue;
            }

            if (MatchesCommand(quitCommands, command)) Quit();
            if (MatchesCommand(continueCommands, command)) return;

            System.Console.WriteLine($"Couldn't recognise: {command}. Please either select 'continue' or 'quit'.");
        }
    }

    private static bool MatchesCommand(IEnumerable<string> commands, string enteredCommand)
    {
        return commands.Any(command => enteredCommand.ToLower().StartsWith(command));
    }
}
