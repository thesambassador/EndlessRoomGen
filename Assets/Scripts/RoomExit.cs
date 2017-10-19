using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//RoomExit marks an object as the "exit" location of the room (where the next room will be placed)
//It also specifies a valid prefab set that can connect to this exit
//So you could have some rooms have a big door as an exit with a set of appropriate rooms to connect to the "big" exit, 
//then some rooms could have a "small" exit and there would be a set of rooms to connect to a "small" exit if you wanted
public class RoomExit : MonoBehaviour {

    public PrefabSet PossibleConnectedRooms;

}
