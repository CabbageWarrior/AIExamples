using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRotation : AIPlayer
{
    public Action rotatingAction;

    public override bool GetAction(out Action chosenAction)
    {
        rotatingAction += 1;
        if (rotatingAction == (Action)3) rotatingAction = (Action)1;
        chosenAction = rotatingAction;
        return true;
    }
}
