namespace RockPaperScissors.Console.AI;

public class AiEasy : AiBase
{
    public override Hand GetMove()
    {
        //If this is the first round, start with paper
        if (rounds.Count == 0)
            return Hand.Paper;

        if (CounterRepeatWin())
            return CounterLastWin();
        
        if (CounterLossCycle())
            return CounterLastLoss();

        //On draw default to paper as it is the safest move
        return Hand.Paper;
    }

    #region Counter Repeat Win

    /// <summary>
    /// If the player has won the previous round, there is a high likelihood that they will repeat the same move.
    /// If the player has won twice with the same move, do not expect it again.
    /// </summary>
    /// <returns></returns>
    private bool CounterRepeatWin()
    {
        int roundCount = rounds.Count;
        //If the player won the previous round
        if (rounds[roundCount - 1].GetResult() == RoundResult.PlayerWin)
        {
            //If there has been at least 2 rounds and the player did not win twice
            if (roundCount > 1 && rounds[roundCount - 2].GetResult() == RoundResult.PlayerWin)
            {
                //Do not expect the same move again
                return false;
            }

            //Otherwise, expect the same move again
            return true;
        }

        //If player lost, they are unlikely to repeat the same move
        return false;
    }

    private Hand CounterLastWin()
    {
        //Counter the last move made by the player
        return rounds.Last().PlayerHand switch
        {
            Hand.Rock => Hand.Paper,
            Hand.Paper => Hand.Scissors,
            Hand.Scissors => Hand.Rock,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion

    #region Counter Loss Cycle

    private bool CounterLossCycle()
    {
        return rounds.Last().GetResult() switch
        {
            RoundResult.Draw => false,
            RoundResult.PlayerWin => false,
            RoundResult.AiWin => true,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Hand CounterLastLoss()
    {
        //If the player lost, assume they'll switch Rock -> Paper -> Scissors in a cycle.
        return rounds.Last().PlayerHand switch
        {
            Hand.Rock => Hand.Scissors,
            Hand.Paper => Hand.Rock,
            Hand.Scissors => Hand.Paper,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #endregion
}

/*
Planning:

Repeat moves:
If the player has won the previous round, there is a high likelihood that they will repeat the same move.
If the player has won twice with the same move, do not expect it again.

Loss Cycle:
If the player loses, assume they'll switch Rock -> Paper -> Scissors in a cycle.

Start with Paper:
If this is the first round, start with Paper, as it beats Rock and is a common starting move.

*/
