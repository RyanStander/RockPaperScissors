using RockPaperScissors.Console.AI;

namespace RockPaperScissors.Console.Player;

public abstract class PlayerInputHandler
{
    public abstract Difficulty SelectDifficulty();
    public abstract Hand SelectHand();
    protected abstract void Quit();
    protected abstract void ContinueGame();
}
