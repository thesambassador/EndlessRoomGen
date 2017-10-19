using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Transform ExitTransform;
    public PrefabSet PossibleConnectedRooms;

    void Awake()
    {
        if (ExitTransform == null)
        {
            ExitTransform = transform.Find("Exit");
        }
    }

}
