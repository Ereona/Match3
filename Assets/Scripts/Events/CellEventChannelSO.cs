using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Cell Event Channel")]
public class CellEventChannelSO : ScriptableObject
{
	public UnityAction<Cell> OnEventRaised;

	public void RaiseEvent(Cell cell)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(cell);
	}
}
