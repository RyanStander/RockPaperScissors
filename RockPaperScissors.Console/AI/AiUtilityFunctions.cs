namespace RockPaperScissors.Console.AI;
/// <summary>
/// Determines the AI difficulty based on the provided Difficulty enum.
/// </summary>
public static class AiUtilityFunctions
{
    public static AiBase SelectDifficulty(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Random => new AiRandom(),
            Difficulty.Easy => new AiEasy(),
            Difficulty.Hard => new AiHard(),
            Difficulty.None => throw new ArgumentException("Difficulty cannot be None.", nameof(difficulty)),
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
        };
    }

    public static Hand GetCounterHand(Hand hand)
    {
        return hand switch
        {
            Hand.Rock => Hand.Paper,
            Hand.Paper => Hand.Scissors,
            Hand.Scissors => Hand.Rock,
            _ => throw new ArgumentOutOfRangeException(nameof(hand), hand, "Invalid hand type.")
        };
    }
}
