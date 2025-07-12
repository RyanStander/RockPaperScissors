using RockPaperScissors.Console.AI;

namespace RockPaperScissors.Console.UserInterface;
/// <summary>
/// The base class for the game interface, this will display all information only, does not handle any functions
/// </summary>
public abstract class GameInterfaceBase
{
    public abstract void GameStart(int bestOfRounds);

    public abstract void DisplayDifficultySelection();
    public abstract void DifficultySelected(Difficulty difficulty);

    public abstract void DisplayRoundStart();

    public abstract void DisplayMatchResult(Round round, GameStateData gameStateData);

    public abstract void RequestQuit(Round round, GameStateData gameStateData);
}
