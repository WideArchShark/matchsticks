using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int minMatches, maxMatches;
    public GameObject matchPrefab;
    public TextMeshProUGUI matchesRemainingText;
    public TextMeshProUGUI statusText;
    public GameObject optionsButtonPanel;
    public GameObject newGamePanel;

    public delegate void MatchValues(int matches);
    public delegate void OptionsButton(GameManager gameManager, int num);
    public MatchValues matchCountListener;
    public OptionsButton OptionsButtonListener;

    [HideInInspector] public GameState loadMatchesState;
    [HideInInspector] public GameState playerTurnState;
    [HideInInspector] public GameState computerTurnState;
    private GameState currentState;

    public List<GameObject> matches;

    // Start is called before the first frame update
    void Start()
    {
        loadMatchesState = ScriptableObject.CreateInstance<LoadMatchesState>();
        playerTurnState = ScriptableObject.CreateInstance<PlayerTurnState>();
        computerTurnState = ScriptableObject.CreateInstance<ComputerTurnState>();

        matchCountListener += MatchesUpdated;
        currentState = loadMatchesState;
        currentState.StartState(this);
    }

    private void MatchesUpdated(int newCount)
    {
        matchesRemainingText.text = "Matches Remaining: " + newCount;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void ChangeState(GameState newState)
    {
        currentState.LostState(this);
        currentState = newState;
        newState.StartState(this);
    }

    public void OptionButtonClicked(int value)
    {
        OptionsButtonListener?.Invoke(this, value);
    }

    public void NewGameButtonClicked()
    {
        Debug.Log("Reloading Scene");
        SceneManager.LoadScene(0);
    }
}
