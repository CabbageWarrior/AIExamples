using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBiased : AIPlayer
{
    public Action biasedAction;

    [Range(0, 1)]
    public float biasProbability;

    public override bool GetAction(out Action chosenAction)
    {
        if (Random.value <= biasProbability)
        {
            chosenAction = biasedAction;
        }
        else
        {
            chosenAction = (Action)Random.Range(1, 4);
        }

        return true;
    }
}
