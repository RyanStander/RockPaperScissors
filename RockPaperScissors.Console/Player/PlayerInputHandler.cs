using RockPaperScissors.Console.AI;

namespace RockPaperScissors.Console.Player;
/// <summary>
/// The base class for the input handler, defines the methods currently in use
/// </summary>
public abstract class PlayerInputHandler
{
    public abstract Difficulty SelectDifficulty();
    public abstract Hand SelectHand();
    protected abstract void Quit();
    public abstract void ContinueGame();
}
