using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHeavyMetal : AIPlayer
{
    public override bool GetAction(out Action chosenAction)
    {
        chosenAction = Action.ROCK;
        return true;
    }
}
