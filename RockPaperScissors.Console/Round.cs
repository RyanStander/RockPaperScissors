namespace RockPaperScissors.Console;

public class Round
{
    public Hand PlayerHand;
    public Hand AiHand;
    public RoundResult GetResult()
    {
        if (PlayerHand == AiHand)
        {
            return RoundResult.Draw;
        }

        if ((PlayerHand == Hand.Rock && AiHand == Hand.Scissors) ||
            (PlayerHand == Hand.Paper && AiHand == Hand.Rock) ||
            (PlayerHand == Hand.Scissors && AiHand == Hand.Paper))
        {
            return RoundResult.PlayerWin;
        }

        return RoundResult.AiWin;
    }
}
