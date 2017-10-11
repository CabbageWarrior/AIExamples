using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AINGram : AIPlayer
{
    public int nGamesBeforePrediction;

    Dictionary<string, int[]> nGramTable;
    List<Action> actionsWindow;

    private void Awake()
    {
        nGramTable = new Dictionary<string, int[]>();

        // Create the pairs.
        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                string key =  i + "" + j ;
                int[] value = new int[3];
                nGramTable.Add(key, value);
            }
        }

        // Initialize the queue
        actionsWindow = new List<Action>();
    }

    public override void ReceiveOpponentAction(Action a)
    {
        if (actionsWindow.Count >= 2)
        {
            // Update the table
            Action a0 = actionsWindow[0];
            Action a1 = actionsWindow[1];

            string key = (int)a0 + "" + (int)a1;
            nGramTable[key][(int)a - 1] += 1;
        }

        // Add to the history
        actionsWindow.Add(a);

        if (actionsWindow.Count == 3)
        {
            actionsWindow.RemoveAt(0);
        }
    }

    private int nGames = 0;
    public override bool GetAction(out Action chosenAction)
    {
        nGames++;
        if (nGames < nGamesBeforePrediction)
        {
            chosenAction = (Action)Random.Range(1, 4);
        }
        else
        {
            // Use the table to predict the next action!
            Action a0 = actionsWindow[0];
            Action a1 = actionsWindow[1];

            string key = (int)a0 + "" + (int)a1;

            int[] opponentActionsCounts = nGramTable[key];
            int totalOpponentActions = opponentActionsCounts.Sum();

            float[] ourActionProbabilities = new float[3];
            ourActionProbabilities[(int)(Action.ROCK) - 1] = opponentActionsCounts[(int)(Action.SCISSORS) - 1] / (float)totalOpponentActions;
            ourActionProbabilities[(int)(Action.PAPER) - 1] = opponentActionsCounts[(int)(Action.ROCK) - 1] / (float)totalOpponentActions;
            ourActionProbabilities[(int)(Action.SCISSORS) - 1] = opponentActionsCounts[(int)(Action.PAPER) - 1] / (float)totalOpponentActions;

            // Random choose based on probabilities.
            float rndChoice = Random.value;
            if (rndChoice <= ourActionProbabilities[(int)(Action.ROCK) - 1])
            {
                chosenAction = Action.ROCK;
            }
            else if (rndChoice <= ourActionProbabilities[(int)(Action.ROCK) - 1] + ourActionProbabilities[(int)(Action.PAPER) - 1])
            {
                chosenAction = Action.PAPER;
            }
            else
            {
                chosenAction = Action.SCISSORS;
            }
        }
        return true;
    }
}
