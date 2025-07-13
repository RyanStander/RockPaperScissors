namespace RockPaperScissors.Console.AI;

public class AiHard : AiBase
{
    /// <summary>
    /// Mimic and Break is always 0.25f as there isnt anything that impacts whether its a good strategy.
    /// </summary>
    private float mimicAndBreakPreference = 0.1f;

    private float frequencyAnalysisPreference;
    private float markovChainPreference;
    private float baitingPreference;

    private const int mimicRounds = 2;
    private int currentRound;
    private bool isMimicing;

    private bool baiting;

    #region Dictionaries

    private Dictionary<Hand, int> handFrequency = new() { { Hand.Rock, 0 }, { Hand.Paper, 0 }, { Hand.Scissors, 0 } };

    private Dictionary<Hand, Dictionary<Hand, int>> transitions = new()
    {
        { Hand.Paper, new() { { Hand.Rock, 0 }, { Hand.Paper, 0 }, { Hand.Scissors, 0 } } },
        { Hand.Rock, new() { { Hand.Rock, 0 }, { Hand.Paper, 0 }, { Hand.Scissors, 0 } } },
        { Hand.Scissors, new() { { Hand.Rock, 0 }, { Hand.Paper, 0 }, { Hand.Scissors, 0 } } }
    };

    #endregion

    public override Hand GetMove()
    {
        //finish baiting
        if (baiting)
        {
            baiting = false;
            return SelectBaitingMove();
        }
        
        if (isMimicing)
            return SelectMimicAndBreakHand();

        DeterminePreference();

        //Even though experienced players know about rock being a common first move, it still is safe to start with paper as worst case it should be a draw.
        if (rounds.Count == 0)
            return Hand.Paper;

        //Determine which move is most preferential, each strategy has its own weight, ranging from 0 to 0.25.
        Random random = new();
        float totalPreference = frequencyAnalysisPreference + markovChainPreference + mimicAndBreakPreference +
                                baitingPreference;

        System.Console.WriteLine("Preference to each strategy with name: " +
                                 $"Frequency Analysis: {frequencyAnalysisPreference}, " +
                                 $"Markov Chain: {markovChainPreference}, " +
                                 $"Mimic and Break: {mimicAndBreakPreference}, " +
                                 $"Baiting: {baitingPreference}, ");

        //default to paper is there is no preferences
        if (totalPreference <= 0)
            return Hand.Paper;

        float randomValue = (float)random.NextDouble() * totalPreference;
        //switch statement with ranges for the different preferences
        switch (totalPreference)
        {
            case var _ when (totalPreference <= frequencyAnalysisPreference):
                return GetFrequencyAnalysisHand();
            case var _ when (totalPreference > frequencyAnalysisPreference &&
                             totalPreference <= frequencyAnalysisPreference + markovChainPreference):
                return GetMarkovChainHand();
            case var _ when (totalPreference > frequencyAnalysisPreference + markovChainPreference &&
                             totalPreference <= frequencyAnalysisPreference + markovChainPreference +
                             mimicAndBreakPreference):
                InitiateMimicAndBreak();
                return SelectMimicAndBreakHand();
            case var _ when (totalPreference >
                             frequencyAnalysisPreference + markovChainPreference + mimicAndBreakPreference &&
                             totalPreference <= frequencyAnalysisPreference + markovChainPreference +
                             mimicAndBreakPreference +
                             baitingPreference):
                baiting = true;
                return SelectBaitingMove();
        }

        //Paper is statistically the best move if no others are available.
        return Hand.Paper;
    }

    public override void AddRound(Round round)
    {
        base.AddRound(round);

        handFrequency[round.PlayerHand]++;

        if (rounds.Count <= 1) return;
        Hand previousMove = rounds[rounds.Count - 2].PlayerHand;
        transitions[previousMove][round.PlayerHand]++;
    }

    private void DeterminePreference()
    {
        frequencyAnalysisPreference = GetFrequencyAnalysisPreference();
        markovChainPreference = GetMarkovChainPreference();
        baitingPreference = GetBaitingPreference();
    }

    #region Frequency Analysis

    /// <summary>
    /// the preference increases as rounds go up to a max of 0.25, at 1 round it is 0, then every round thereafter it goes up by 0.05 until it reaches 0.25 at 5 rounds.
    /// </summary>
    /// <returns></returns>
    private float GetFrequencyAnalysisPreference() =>
        (rounds.Count - 1) * 0.05f > 0.25f ? 0.25f : (rounds.Count - 1) * 0.05f;

    private Hand GetFrequencyAnalysisHand()
    {
        Hand mostFrequentHand = handFrequency.MaxBy(kvp => kvp.Value).Key;

        return AiUtilityFunctions.GetCounterHand(mostFrequentHand);
    }

    #endregion

    #region Markov Chain

    private float GetMarkovChainPreference()
    {
        //get the sum of each of the transitions
        int rockResponses = transitions[Hand.Rock].Values.Sum();
        int paperResponses = transitions[Hand.Paper].Values.Sum();
        int scissorsResponses = transitions[Hand.Scissors].Values.Sum();

        //get the smallest of the values
        int minResponses = Math.Min(Math.Min(rockResponses, paperResponses), scissorsResponses);

        return minResponses * 0.05f > 0.25f ? 0.25f : minResponses * 0.05f;
    }

    private Hand GetMarkovChainHand()
    {
        //using our previous move, we can predict what is the most likely next move of the player
        //we will then counter that
        Hand previousMove = rounds.Last().PlayerHand;
        Hand mostLikelyNextMove = transitions[previousMove].MaxBy(kvp => kvp.Value).Key;

        return AiUtilityFunctions.GetCounterHand(mostLikelyNextMove);
    }

    #endregion

    #region Mimicing

    private void InitiateMimicAndBreak()
    {
        isMimicing = true;
        currentRound = 0;
    }

    private Hand SelectMimicAndBreakHand()
    {
        currentRound++;
        
        //mimic for the mimic round duration
        if(currentRound<= mimicRounds)
        {
            return rounds.Last().PlayerHand;
        }
        
        isMimicing = false;
        
        //then use frequency analysis to determine the next to break their plans
        return  GetFrequencyAnalysisHand();
    }

    #endregion

    #region Baiting

    private float GetBaitingPreference()
    {
        //check if lost the previous round
        if (rounds.Count > 1 && rounds.Last().GetResult() == RoundResult.PlayerWin)
        {
            //if the ai has lost the last 2 rounds, its a bit too risky to bait
            if (rounds.Count > 2 && rounds[rounds.Count - 2].GetResult() == RoundResult.PlayerWin)
            {
                return 0.05f;
            }

            //If only lost 1 round, then we find baiting a good option
            return 0.1f;
        }

        return 0f;
    }

    private Hand SelectBaitingMove()
    {
        if (baiting)
        {
            return rounds.Last().PlayerHand switch
            {
                Hand.Rock => Hand.Scissors,
                Hand.Paper => Hand.Rock,
                Hand.Scissors => Hand.Paper,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return AiUtilityFunctions.GetCounterHand(rounds.Last().PlayerHand);
    }

    #endregion
}

/*
Planning:

Frequency Analysis:
The ai will use previous moves to try and determine the next move of the player.

Markov Chain:
Use transition probabilities between previous player moves to predict the next

Mimic & Break:
Copy the player initially to appear predictable, then shift once the pattern is known

Baiting:
Purposely lose to provoke a repeat move then counter it
*/
