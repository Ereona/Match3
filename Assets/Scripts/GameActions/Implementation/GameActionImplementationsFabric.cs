﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionImplementationsFabric : MonoBehaviour
{
    public float ActionDuration = 0.25f;
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
        else if (action is RemoveGemGameAction)
        {
            impl = new RemoveGemGameActionImplementation();
        }
        else if (action is MoveGemGameAction)
        {
            impl = new MoveGemGameActionImplementation();
        }
        else if (action is SpawnGemGameAction)
        {
            impl = new SpawnGemGameActionImplementation();
        }
        else if (action is HideGemGameAction)
        {
            impl = new HideGemGameActionImplementation();
        }
        else if (action is ShowGemGameAction)
        {
            impl = new ShowGemGameActionImplementation();
        }
        else
        {
            throw new System.NotImplementedException("Unknown action type");
        }
        impl.Duration = ActionDuration;
        impl.SourceAction = action;
        impl.Objects = Objects;
        return impl;
    }
}
