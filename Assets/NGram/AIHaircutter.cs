using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHaircutter : AIPlayer
{
    public override bool GetAction(out Action chosenAction)
    {
        chosenAction = Action.SCISSORS;
        return true;
    }
}
