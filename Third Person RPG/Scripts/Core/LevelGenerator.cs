using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelGeneratorProfile profile;
    public int rooms = 0;

    Vector2[] TilesOccupied;
    List<Vector2> TilesPending;

    int pending = 0;
    int random;

    float roomSize;

    void Start()
    {
        TilesOccupied = new Vector2[profile.MaxRooms];
        TilesPending = new List<Vector2>();

        AttemptSurroundingTiles(Vector2.zero);

        roomSize = profile.RoomGO.transform.localScale.x;

        GenerateRooms();
    }

    void GenerateRooms()
    {
        if (profile.MaxRooms > 0)
        {
            if (profile.GenerateFirstRoom)
            {
                // First Room
                GameObject.Instantiate(profile.RoomGO, new Vector3(0, 0, 0), transform.rotation);
                TilesOccupied[rooms] = Vector2.zero;
                rooms++;
            }
            else
            {
                TilesOccupied[rooms] = Vector2.zero;
                rooms++;
            }

            while (rooms < profile.MaxRooms && pending < TilesPending.Count)
            {
                int p = TilesPending.Count - pending;

                if (p < profile.ForceRoomTreshold)
                {
                    random = 0;
                }
                else
                {
                    random = Random.Range(0, 100);
                }

                if (random <= profile.ChangeForRoom)
                {
                    GameObject.Instantiate(profile.RoomGO, new Vector3(TilesPending[pending].x, 0, TilesPending[pending].y) * roomSize, transform.rotation);
                    TilesOccupied[rooms] = TilesPending[pending];
                    AttemptSurroundingTiles(TilesPending[pending]);
                }

                pending++;
                rooms++;

                profile.GeneratedRooms = rooms;
            }
        }

        for (int i = TilesPending.Count - 1; i >= 0; i--)
        {
            if (FreeTile(TilesPending[i]))
            {
                GameObject.Instantiate(profile.WallGO, new Vector3(TilesPending[i].x, 0, TilesPending[i].y) * roomSize, transform.rotation);
            }
        }
    }

    void AttemptSurroundingTiles(Vector2 coordinates)
    {
        CheckAndSetPending(coordinates + Vector2.up);
        CheckAndSetPending(coordinates + Vector2.left);
        CheckAndSetPending(coordinates + Vector2.right);
        CheckAndSetPending(coordinates + Vector2.down);
    }

    void CheckAndSetPending(Vector2 coordinates)
    {
        if (FreeTile(coordinates))
        {
            TilesPending.Add(coordinates);
        }
    }

    bool FreeTile(Vector2 coordinates)
    {
        for (int i = 0; i < TilesOccupied.Length; i++)
        {
            if (TilesOccupied[i] == coordinates)
            {
                return false;
            }
        }

        return true;
    }
}
