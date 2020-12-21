using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Matches List Event Channel")]
public class MatchesListEventChannelSO : ScriptableObject
{
	public UnityAction<MatchesList> OnEventRaised;

	public void RaiseEvent(MatchesList value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
