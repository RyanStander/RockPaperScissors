namespace RockPaperScissors.Console.AI;

public static class AiDifficultySelector
{
    public static AiBase SelectDifficulty(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Random => new AiRandom(),
            Difficulty.Easy => throw new NotImplementedException("Easy mode Ai is not implemented yet."),
            Difficulty.Hard => throw new NotImplementedException("Hard mode Ai is not implemented yet."),
            Difficulty.None => throw new ArgumentException("Difficulty cannot be None.", nameof(difficulty)),
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
        };
    }
}
