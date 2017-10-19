using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    //length in rooms to keep active
    public int levelLength = 10;
    public float maxAngle = 90;
    public Room StartingRoom;

    private LinkedList<Room> _roomList;

    void Start()
    {
        GenerateInitialLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateRoom();
        }
    }

    public void GenerateInitialLevel()
    {
        _roomList = new LinkedList<Room>();
        bool success = true;

        _roomList.AddLast(StartingRoom);
        while(_roomList.Count < levelLength && success){
            success = GenerateRoom();
        }

    }

    public bool GenerateRoom()
    {
        if (_roomList.Count >= levelLength)
        {
            RemoveFirstRoom();
        }

        //The potential prefabs comes from the last room we generated
        PrefabSet potentialPrefabs = _roomList.Last.Value.PossibleConnectedRooms;

        GameObject roomPrefab = potentialPrefabs.GetRandomValidSetPrefab(IsRoomValid);

        if (roomPrefab != null)
        {
            GameObject newRoom = ObjectPoolManager.GetObject(roomPrefab);

            newRoom.transform.parent = this.gameObject.transform;
            newRoom.transform.position = _roomList.Last.Value.ExitTransform.position;
            newRoom.transform.rotation = _roomList.Last.Value.ExitTransform.rotation;

            //this should be valid already since IsRoomValid will remove all prefabs without a "Room" script on it from the potential list
            _roomList.AddLast(newRoom.GetComponent<Room>());
            return true;
        }
        else
        {
            Debug.Log("No rooms are valid after the current last room! AHH");
            return false;
        }
    }

    //We only want to consider prefabs whose exit transforms are within -90 and 90 degrees of the first room's exit, to avoid overlaps
    //This could certainly be more robust to allow for more interesting levels, but for basic endless runner type games, this works fine
    public bool IsRoomValid(GameObject pRoomGameObject)
    {
        Room pRoom = pRoomGameObject.GetComponent<Room>();
        if (pRoom != null)
        {
            Vector3 firstRoomExitDirection = Vector3.forward;
            Vector3 lastRoomExitDirection = _roomList.Last.Value.ExitTransform.forward;

            float angle = Vector3.SignedAngle(firstRoomExitDirection, lastRoomExitDirection, Vector3.up);

            Vector3 pRoomEntranceDirection = pRoom.transform.forward;
            Vector3 pRoomExitDirection = pRoom.ExitTransform.forward;

            angle += Vector3.SignedAngle(pRoomEntranceDirection, pRoomExitDirection, Vector3.up);

            if (Mathf.Abs(angle) <= maxAngle + 1)
            {
                return true;
            }
        }
        return false;
    }

    private void RemoveFirstRoom()
    {
        if (_roomList.Count > 0)
        {
            Room firstRoom = _roomList.First.Value;
            //doing return object delayed to ensure that we can't re-use the same room instance on the same frame it's removed, since the sub-objects might need to also return delayed
            ObjectPoolManager.ReturnObjectDelayed(firstRoom.gameObject, .01f);
            _roomList.RemoveFirst();
        }
        else
        {
            Debug.Log("Can't remove the first room, room list is currently empty");
        }
    }

}
