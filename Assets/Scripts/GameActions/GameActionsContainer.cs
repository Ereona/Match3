using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionsContainer : MonoBehaviour
{
    private List<GameActionImplementation> actions = new List<GameActionImplementation>();
    private GameActionImplementationsFabric Fabric;

    private void Start()
    {
        Fabric = GetComponent<GameActionImplementationsFabric>();
    }

    public void Add(GameAction action)
    {
        actions.Add(Fabric.CreateImplementation(action));
    }

    public void Clear()
    {
        actions.Clear();
    }

    public IEnumerator PerformAll()
    {
        foreach (GameActionImplementation a in actions)
        {
            StartCoroutine(a.Perform());
        }
        while (HasPerforming())
        {
            yield return null;
        }
    }

    private bool HasPerforming()
    {
        foreach (GameActionImplementation a in actions)
        {
            if (!a.IsPerformed)
            {
                return true;
            }
        }
        return false;
    }
}
