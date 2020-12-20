using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionImplementationsFabric : MonoBehaviour
{
    private FieldObjectsContainer Objects;

    private void Start()
    {
        Objects = FindObjectOfType<FieldObjectsContainer>();
    }

    public GameActionImplementation CreateImplementation(GameAction action)
    {
        GameActionImplementation impl;
        if (action is GemsSwapGameAction)
        {
            impl = new GemsSwapGameActionImplementation();
        }
        else if (action is GemsBothWayMovingGameAction)
        {
            impl = new GemsBothWayMovingGameActionImplementation();
        }
        else
        {
            throw new System.NotImplementedException("Unknown action type");
        }
        impl.SourceAction = action;
        impl.Objects = Objects;
        return impl;
    }
}
