using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHuman : AIPlayer
{
    public override bool GetAction(out Action chosenAction)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            chosenAction = Action.ROCK;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            chosenAction = Action.PAPER;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            chosenAction = Action.SCISSORS;
            return true;
        }

        chosenAction = Action.NONE;
        return false;
    }
}
