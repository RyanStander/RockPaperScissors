using RockPaperScissors.Console.AI;

namespace RockPaperScissors.Console.UserInterface;
/// <summary>
/// Interface for in the console, provides text based information only.
/// </summary>
public class TextInterface : GameInterfaceBase
{
    public override void GameStart(int bestOfRounds)
    {
        System.Console.WriteLine("Welcome to Rock, Paper, Scissors!\n" +
                                 $"You will be playing best of {bestOfRounds}, good luck!");
    }

    public override void DisplayDifficultySelection()
    {
        System.Console.WriteLine("Type 'quit' to exit the game.\n" +
                                 "Please choose a difficulty: 'easy', 'medium', or 'hard'.");
    }

    public override void DifficultySelected(Difficulty difficulty)
    {
        System.Console.WriteLine($"You have selected {difficulty} difficulty.");
    }

    public override void DisplayRoundStart()
    {
        System.Console.WriteLine("A new round is starting!\n" + "Choose your hand: 'rock', 'paper', or 'scissors'.");
    }

    public override void DisplayMatchResult(Round round, GameStateData gameStateData)
    {
        System.Console.Write($"Round result: Player chose {round.PlayerHand}, AI chose {round.AiHand}. ");
        
        switch (round.GetResult())
        {
            case RoundResult.PlayerWin:
                System.Console.WriteLine("You win this round!");
                break;
            case RoundResult.AiWin:
                System.Console.WriteLine("The AI wins this round!");
                break;
            case RoundResult.Draw:
                System.Console.WriteLine("It's a draw!");
                break;
        }

        System.Console.WriteLine(
            $"Current score - Player: {gameStateData.PlayerRoundPoints}, AI: {gameStateData.AiRoundPoints}.");
    }

    public override void RequestQuit(Round round, GameStateData gameStateData)
    {
        //tell the player whether they wont the whole match, then ask them if they want to quit or continue
        if (round.GetResult() == RoundResult.PlayerWin)
        {
            System.Console.WriteLine("Congratulations! You won the match!");
        }
        else
        {
            System.Console.WriteLine("The AI won the match. Better luck next time!");
        }
        
        System.Console.WriteLine($"Total matches won - Player: {gameStateData.PlayerMatchPoints}, AI: {gameStateData.AiMatchPoints}.");
        System.Console.WriteLine("Type 'quit' to exit the game or 'continue' to play again.");
    }
}
