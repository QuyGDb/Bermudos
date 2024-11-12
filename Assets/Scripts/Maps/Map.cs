using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{

    private void Start()
    {
        StaticEventHandler.CallMapChangedEvent(this);
    }

}
