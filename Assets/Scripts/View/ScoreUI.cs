using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public IntEventChannelSO ScoreChangedEvent;
    public Text ScoreValueText;

    void Start()
    {
        ScoreChangedEvent.OnEventRaised += OnScoreChanged;
    }

    private void OnDestroy()
    {
        ScoreChangedEvent.OnEventRaised -= OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        ScoreValueText.text = value.ToString();
    }
}
