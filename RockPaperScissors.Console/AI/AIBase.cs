namespace RockPaperScissors.Console.AI;
/// <summary>
/// The base class for AI, defines the functions that are shared by all AI types.
/// </summary>
public abstract class AiBase
{
    public abstract Hand GetMove();
}
