using RockPaperScissors.Console.AI;

namespace RockPaperScissors.Console.UserInterface;

/// <summary>
/// Interface for in the console, provides text-based information only.
/// </summary>
public class TextInterface : GameInterfaceBase
{
    public void Clear()
    {
        System.Console.Clear();
    }

    private void Header(string title)
    {
        System.Console.WriteLine("╔══════════════════════════════════════════════╗");
        System.Console.WriteLine($"║ {title.PadRight(44)} ║");
        System.Console.WriteLine("╚══════════════════════════════════════════════╝\n");
    }

    public override void GameStart(int bestOfRounds)
    {
        Clear();
        Header("ROCK PAPER SCISSORS");
        System.Console.WriteLine($"Welcome to Rock, Paper, Scissors!");
        System.Console.WriteLine($"You will be playing best of {bestOfRounds} rounds.\n");
        System.Console.WriteLine("Good luck!\n");
    }

    public override void DisplayDifficultySelection()
    {
        Clear();
        Header("SELECT DIFFICULTY");
        System.Console.WriteLine("Type 'quit' to exit the game.\n");
        System.Console.WriteLine("Please choose a difficulty:");
        System.Console.WriteLine("  → easy");
        System.Console.WriteLine("  → medium");
        System.Console.WriteLine("  → hard\n");
    }

    public override void DifficultySelected(Difficulty difficulty)
    {
        Clear();
        Header("DIFFICULTY SELECTED");
        System.Console.WriteLine($"You have selected: {difficulty.ToString().ToUpper()}");
        System.Console.WriteLine();
    }

    public override void DisplayRoundStart()
    {
        Header("NEW ROUND");
        System.Console.WriteLine("Choose your hand:");
        System.Console.WriteLine("  ✊ rock");
        System.Console.WriteLine("  ✋ paper");
        System.Console.WriteLine("  ✌️ scissors\n");
    }

    public override void DisplayMatchResult(Round round, GameStateData gameStateData)
    {
        Clear();
        Header("ROUND RESULT");
        System.Console.WriteLine($"You chose: {round.PlayerHand}");
        System.Console.WriteLine($"AI chose:  {round.AiHand}\n");

        switch (round.GetResult())
        {
            case RoundResult.PlayerWin:
                System.Console.WriteLine("✅ You win this round!");
                break;
            case RoundResult.AiWin:
                System.Console.WriteLine("❌ The AI wins this round!");
                break;
            case RoundResult.Draw:
                System.Console.WriteLine("➖ It's a draw!");
                break;
        }

        System.Console.WriteLine($"\nCurrent Score:");
        System.Console.WriteLine($"  Player: {gameStateData.PlayerRoundPoints}");
        System.Console.WriteLine($"  AI:     {gameStateData.AiRoundPoints}\n");
    }

    public override void RequestQuit(Round round, GameStateData gameStateData)
    {
        Header("MATCH OVER");

        if (round.GetResult() == RoundResult.PlayerWin)
        {
            System.Console.WriteLine("🎉 Congratulations! You won the match!\n");
        }
        else
        {
            System.Console.WriteLine("🤖 The AI won the match. Better luck next time!\n");
        }

        System.Console.WriteLine($"Total Matches Won:");
        System.Console.WriteLine($"  Player: {gameStateData.PlayerMatchPoints}");
        System.Console.WriteLine($"  AI:     {gameStateData.AiMatchPoints}\n");

        System.Console.WriteLine("Type 'continue' to play again or 'quit' to exit.");
    }
}
