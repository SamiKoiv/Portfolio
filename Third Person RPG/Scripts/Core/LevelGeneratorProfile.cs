using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorProfile : ScriptableObject
{
    public bool GenerateFirstRoom;
    public int MaxRooms;
    public int ChangeForRoom;
    public int ForceRoomTreshold;

    public GameObject RoomGO;
    public GameObject WallGO;

    [Header("Debug")]
    public int GeneratedRooms;

}
