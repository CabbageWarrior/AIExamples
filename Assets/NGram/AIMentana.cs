using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMentana : AIPlayer
{
    public override bool GetAction(out Action chosenAction)
    {
        chosenAction = Action.PAPER;
        return true;
    }
}
