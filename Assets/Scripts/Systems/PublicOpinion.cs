using System;
using UnityEngine;


public enum TierList
{
    // People love the player, but syndicates hate them.
    BastionOfTheFederation,
    GoodSamaritan,
    DoGooder,
    Neutral,
    Wary,
    Distrustful,
    Villain,
    BaronOfTheFederation,
    None
}

public class PublicOpinion : MonoBehaviour
{
    [Header("Current Opinion Standing")]
    [Range(-1000, 1000)]
    [SerializeField] private int opinionScore = 0; // Start off with zero.
    public TierList currentOpinion; // What the public currently thinks of the player.

    public static event Action<TierList> OnTierChange; // Fire the event if the tier changes. 

    public void Start()
    {
        // Fetch the current tier list from the save file, or just resort to neutral if no file detected.
        EvaluateTierChange();
    }

    /// <summary>
    /// Determines if the player will become good or evil.
    /// </summary>
    /// <param name="opinionPoints"></param>
    public void ModifyOpinion(int opinionPoints)
    {
        int previousScore = opinionScore; // Last score is equivalent to the opinionScore
        opinionScore = Mathf.Clamp(opinionScore + opinionPoints, -1000, 1000); // Clamp between -1000 and +1000

        if (previousScore != opinionScore) // Has the score changed?
        {
            EvaluateTierChange();
        }
    }

    /// <summary>
    /// Responsible for changing the tier based on points.
    /// </summary>
    public void EvaluateTierChange()
    {
        // Responsible for updating tier based on the score the player has.
        TierList updateTier = opinionScore switch
        {
            >= 751 => TierList.BastionOfTheFederation,
            >= 501 => TierList.GoodSamaritan,
            >= 201 => TierList.DoGooder,
            >= -200 => TierList.Neutral,
            >= -400 => TierList.Wary,
            >= -600 => TierList.Distrustful,
            >= -850 => TierList.Villain,
            _ => TierList.BaronOfTheFederation
        };

        if (updateTier != currentOpinion)
        {
            currentOpinion = updateTier; // Update the current tier.
            OnTierChange?.Invoke(currentOpinion);
            EvaluatePublicOpinion(currentOpinion);
        }
    }

    /// <summary>
    /// This evulates the currentOpinion perceived by the public, and how they are likely to act around the player.
    /// </summary>
    /// <param name="currentOpinion"></param>
    public void EvaluatePublicOpinion(TierList currentOpinion)
    {
        switch (currentOpinion)
        {
            case TierList.BastionOfTheFederation:
                // People see the player in a delightful manner
                break;
            case TierList.GoodSamaritan:
                break;
            case TierList.DoGooder:
                break;
            case TierList.Neutral:
                break;
            case TierList.Wary:
                break;
            case TierList.Distrustful:
                break;
            case TierList.Villain:
                break;
            case TierList.BaronOfTheFederation:
                break;
        }
    }
}
