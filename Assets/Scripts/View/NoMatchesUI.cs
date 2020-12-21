using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMatchesUI : MonoBehaviour
{
    public BoolEventChannelSO MixEvent;
    public GameObject Content;

    void Start()
    {
        Content.SetActive(false);
        MixEvent.OnEventRaised += OnMixEvent;
    }

    private void OnDestroy()
    {
        MixEvent.OnEventRaised -= OnMixEvent;
    }

    private void OnMixEvent(bool value)
    {
        Content.SetActive(value);
    }
}
