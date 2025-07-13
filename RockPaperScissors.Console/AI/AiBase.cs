namespace RockPaperScissors.Console.AI;
/// <summary>
/// The base class for AI, defines the functions that are shared by all AI types.
/// </summary>
public abstract class AiBase
{
    protected List<Round> rounds = new List<Round>();
    public abstract Hand GetMove();
    
    public virtual void AddRound(Round round)
    {
        rounds.Add(round);
    }
}
