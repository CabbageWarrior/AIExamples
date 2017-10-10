using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandom : AIPlayer
{
    public override bool GetAction(out Action chosenAction)
    {
        chosenAction = (Action)Random.Range(1, 4);
        return true;
    }
}
