using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlayer : MonoBehaviour
{
    public abstract bool GetAction(out Action chosenAction);
}
