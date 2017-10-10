using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    NONE,
    ROCK,
    PAPER,
    SCISSORS
}

/// <summary>
/// RPS gameplay
/// - Input S, C or F to play
/// - The AI will select the action
/// - We log the result
/// </summary>
public class RockPaperScissors : MonoBehaviour
{
    #region Statistics
    int nGames = 0;
    int nWon = 0;
    int nLost = 0;
    int nDraw = 0;
    #endregion

    public int maxGames = 0; // Check the inspector!

    public AIPlayer player1, player2;

    void Update()
    {
        Action chosenActionP1 = Action.NONE, chosenActionP2 = Action.NONE;

        if (nGames < maxGames)
        {
            // You choose
            if (player1.GetAction(out chosenActionP1) && player2.GetAction(out chosenActionP2))
            {
                Debug.Log("P1 chooses: " + chosenActionP1);
                Debug.Log("P2 chooses: " + chosenActionP2);

                // Check who wins
                switch (chosenActionP1 - chosenActionP2)
                {
                    case 0:
                        nDraw++;
                        Debug.Log("Draw!");
                        break;
                    case 1:
                    case -2:
                        nWon++;
                        Debug.Log("P1 Wins!");
                        break;
                    case 2:
                    case -1:
                        nLost++;
                        Debug.Log("P1 Wins!");
                        break;
                }
                nGames++;

                float myWinRate = nWon * 100 / (float)nGames;
                float aiWinRate = nLost * 100 / (float)nGames;
                Debug.Log("P1 winrate: " + myWinRate.ToString("0.000") + "% --- P2 winrate: " + aiWinRate.ToString("0.000") + "%"
                    + "\n" + "nGames: " + nGames + " --- nP1Won: " + nWon + " --- nP2Won: " + nLost + " --- nDraw: " + nDraw);
            }
        }
    }
}
