namespace RockPaperScissors.Console;

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
