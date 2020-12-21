using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoresController : MonoBehaviour
{
    public MatchesListEventChannelSO MatchesEvent;
    public IntEventChannelSO ScoreChangedEvent;

    public int Score { get; private set; }

    void Start()
    {
        MatchesEvent.OnEventRaised += OnMatchesEventRaised;
    }

    private void OnDestroy()
    {
        MatchesEvent.OnEventRaised -= OnMatchesEventRaised;
    }

    private void OnMatchesEventRaised(MatchesList value)
    {
        List<List<Cell>> matches = value.GetAllMatches();
        int scores = matches.Sum(c => ScoresForMatch(c));
        Score += scores;
        ScoreChangedEvent.RaiseEvent(Score);
    }

    private int ScoresForMatch(List<Cell> match)
    {
        return 10 + (match.Count - 3) * 5;
    }
}
