//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMatchesState : GameState
{
    public override void LostState(GameManager gameManager)
    {
    }

    public override void StartState(GameManager gameManager)
    {
        gameManager.StartCoroutine(AddMatches(gameManager));
    }

    public override void UpdateState(GameManager gameManager)
    {
        // throw new System.NotImplementedException();
    }

    private IEnumerator AddMatches(GameManager gameManager)
    {
        int matchCount = Random.Range(gameManager.minMatches, gameManager.maxMatches + 1);

        gameManager.matches = new List<GameObject>();

        for (int i = 0; i < matchCount; i++)
        {
            GameObject match = Instantiate(gameManager.matchPrefab, new Vector3(0, 6 + (i * 0.4f), 0), Random.rotation);
            gameManager.matches.Add(match);
        }

        gameManager.matchCountListener?.Invoke(gameManager.matches.Count);

        yield return new WaitForSeconds(3f);

        gameManager.ChangeState(gameManager.playerTurnState);
    }
}
