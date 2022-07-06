using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerTurnState : GameState
{
    public override void LostState(GameManager gameManager)
    {
        // throw new System.NotImplementedException();
    }

    public override void StartState(GameManager gameManager)
    {
        gameManager.StartCoroutine(CalculateMove(gameManager));
    }

    private IEnumerator CalculateMove(GameManager gameManager)
    {
        yield return ChangeStatusText(gameManager, "Computers turn", 3f);
        yield return ChangeStatusText(gameManager, "", 1f);
        int currentMatchCount = gameManager.matches.Count;
        int matchesToRemove = Mathf.Max(1, (currentMatchCount - 1) % 4);

        for (int i = 0; i < matchesToRemove; i++)
        {
            GameObject.Destroy(gameManager.matches[0]);
            gameManager.matches.RemoveAt(0);
        }

        gameManager.matchCountListener?.Invoke(gameManager.matches.Count);
        yield return ChangeStatusText(gameManager, "Matches removed: " + matchesToRemove, 3f);

        if (gameManager.matches.Count == 1)
        {
            yield return ChangeStatusText(gameManager, "", 1f); // little pause :)
            yield return ChangeStatusText(gameManager, "Computer wins!", 5f);
            gameManager.newGamePanel.SetActive(true);
        }
        else
        {
            yield return ChangeStatusText(gameManager, "", 1f); // little pause :)
            gameManager.ChangeState(gameManager.playerTurnState);
        }
    }

    public override void UpdateState(GameManager gameManager)
    {
        // throw new System.NotImplementedException();
    }
}
