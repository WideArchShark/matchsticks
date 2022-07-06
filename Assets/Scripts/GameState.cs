using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : ScriptableObject {
    public abstract void StartState(GameManager gameManager);

    public abstract void UpdateState(GameManager gameManager);

    public abstract void LostState(GameManager gameManager);

    public IEnumerator ChangeStatusText(GameManager gameManager, string text, float duration) {
        gameManager.statusText.gameObject.SetActive(true);
        gameManager.statusText.text = text;
        yield return new WaitForSeconds(duration);
        gameManager.statusText.gameObject.SetActive(false);
    }

}
