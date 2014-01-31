using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Possible states for the game to be in
/// </summary>
public enum State {
    Running,
    Paused,
    Win,
    Lose
}

public class GameState : MonoBehaviour {
    /// <summary>
    /// The time the game has run so far.
    /// </summary>
    public float timeUsed = 0.0f;

    /// <summary>
    /// Maximum length for the game to run.
    /// </summary>
    public float maxtime = 7.0f; // TODO: refactor to move this into separate termination logic

    /// <summary>
    /// Alternative states for game to be in
    /// </summary>
    public State CurrentState = State.Running;

    /// <summary>
    /// Global score
    /// </summary>
    public float score = 0.0f;

    /// <summary>
    /// Record of history of objects clicked on by player
    /// Triples are of the form (time, object, tag) respectively.
    /// </summary>
    public List<Triple<double, string, string>> clickTrace = new List<Triple<double, string, string>>();

    /// <summary>
    /// Record of history of objects clicked on by partner
    /// </summary>
    public List<Triple<double, string, string>> partnerTrace = new List<Triple<double, string, string>>();

    /// <summary>
    /// Track actions for changing game parameters
    /// </summary>
    public List<ParamChange> actionTrace = new List<ParamChange>();

    /// <summary>
    /// Retains most recent action setting
    /// </summary>
    public ParamChange currentAction;


    // cf: http://clearcutgames.net/home/?p=437
    // (v1) Allow manipulation in editor and prevent duplicates
    // static singleton property
    public static GameState Singleton { get; private set; }

    // instantiate on game start
    void Awake() {

        // check for conflicting instances
        if (Singleton != null && Singleton != this) {
            Destroy(gameObject); // destroy others that conflict
        }

        Singleton = this; // save singleton instance

        // XXX (kasiu): BUT I NEED IT TO BE DESTROYED BETWEEN SCENES
        // Alex, don't kill me.
        //DontDestroyOnLoad(gameObject); // ensure not destroyed b/t scenes
    }

    // cf: http://clearcutgames.net/home/?p=437
    // (v2) Instantiate lazily
    //public static GameState singleton;
    //public static GameState Singleton {
    //    get { return singleton ?? (singleton = new GameObject("GlobalState").AddComponent<GameState>()); }
    //}


    // Update is called once per frame
    void Update() {
        if (CurrentState != State.Paused) {
            timeUsed += Time.deltaTime;
            GameState.Singleton.timeUsed += Time.deltaTime;
        }
    }
}

// XXX (kasiu): also hides global score.
public static class GameRoundCounter
{
    private static int current = 0;
    private static int totalScore = 0;

    public static int GetCurrentRound() {
        return current;
    }

    public static void AdvanceRound() {
        current++;
    }

    public static int GetTotalScore() {
        return totalScore;
    }

    public static void AddScore(int score) {
        totalScore += score;
    }
}