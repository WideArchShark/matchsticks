using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnState : GameState
{
    private Button button1, button2, button3;

    public override void LostState(GameManager gameManager)
    {
        // throw new System.NotImplementedException();
    }

    public override void StartState(GameManager gameManager)
    {
        gameManager.StartCoroutine(ShowDetails(gameManager));
        Button[] buttons = gameManager.optionsButtonPanel.GetComponentsInChildren<Button>();
        button1 = buttons[0];
        button2 = buttons[1];
        button3 = buttons[2];
        gameManager.OptionsButtonListener = OptionsButtonPressed; // This is going to get called every player turn... Not sure if that's ideal.
    }

    private IEnumerator ShowDetails(GameManager gameManager)
    {
        yield return gameManager.StartCoroutine(ChangeStatusText(gameManager, "Players Turn", 3f));

        if (gameManager.matches.Count == 2)
        {
            button2.gameObject.SetActive(false);
            button3.gameObject.SetActive(false);
        }

        gameManager.optionsButtonPanel.SetActive(true);
    }

    private void OptionsButtonPressed(GameManager gameManager, int value)
    {
        gameManager.StartCoroutine(HandleButtonPress(gameManager, value));
    }

    private IEnumerator HandleButtonPress(GameManager gameManager, int value)
    {
        gameManager.optionsButtonPanel.SetActive(false);

        for (int i = 0; i < value; i++)
        {
            Destroy(gameManager.matches[0]);
            gameManager.matches.RemoveAt(0);
        }

        gameManager.matchCountListener?.Invoke(gameManager.matches.Count);
        yield return ChangeStatusText(gameManager, "Matches removed: " + value, 3f);

        if (gameManager.matches.Count == 1)
        {
            yield return ChangeStatusText(gameManager, "", 1f); // little pause :)
            yield return ChangeStatusText(gameManager, "Player wins!", 4f);
            gameManager.newGamePanel.SetActive(true);
        } else
        {
            yield return ChangeStatusText(gameManager, "", 1f); // little pause :)
            gameManager.ChangeState(gameManager.computerTurnState);
        }
    }

    public override void UpdateState(GameManager gameManager)
    {
        // throw new System.NotImplementedException();
    }
}