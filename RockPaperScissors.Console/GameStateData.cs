namespace RockPaperScissors.Console;
/// <summary>
/// Holds the data for the game state to be passed around to other classes that might need this.
/// </summary>
public class GameStateData
{
    public int AiRoundPoints { get; private set; }
    public int PlayerRoundPoints { get; private set; }
    public int AiMatchPoints { get; private set; }
    public int PlayerMatchPoints { get; private set; }

    public void PlayerWonRound() => PlayerRoundPoints++;
    public void PlayerLostRound() => AiRoundPoints++;
    public void PlayerWonMatch() => PlayerMatchPoints++;
    public void PlayerLostMatch() => AiMatchPoints++;

    public void ResetMatch()
    {
        PlayerRoundPoints = 0;
        AiRoundPoints = 0;
    }
}
