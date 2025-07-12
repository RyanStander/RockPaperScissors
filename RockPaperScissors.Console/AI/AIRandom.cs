namespace RockPaperScissors.Console.AI;

public class AiRandom : AiBase
{
    public override Hand GetMove()
    {
        return DetermineMoveRandom();
    }

    private Hand DetermineMoveRandom()
    {
        Random random = new Random();
        int move = random.Next(1, Enum.GetValues(typeof(Hand)).Length);

        return (Hand)move;
    }
}
