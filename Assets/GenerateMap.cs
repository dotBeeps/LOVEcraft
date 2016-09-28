using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateMap : MonoBehaviour {

    private int roomsGenerated = 0;
    private List<GameObject> openRooms = new List<GameObject>();

	// Use this for initialization
	void Start () {
        GenerateRooms(20);
	}
	
    void GenerateRooms(int numRooms)
    {
        openRooms.Add(Instantiate(Resources.Load("Rooms/RoomBase") as GameObject,Vector3.zero,Quaternion.identity) as GameObject);
        roomsGenerated++;

        while (roomsGenerated-2<numRooms)
        {
            GameObject curRoom = openRooms[Random.Range(0, Mathf.FloorToInt(openRooms.Count - 1))];
            RoomStart roomScript = curRoom.GetComponent<RoomStart>();
            int dir = Mathf.RoundToInt(Random.Range(0, 3));
            //Debug.Log("NUMBER IS");
            //Debug.Log(dir);
            Debug.Log("START GEN NEW ROOM");
            while (!roomScript.doors[dir])
            {
                Debug.Log("Fixing Direction");
                if (dir < 4)
                    dir++;
                else
                    dir = 0;
            }
            float yOffset = 0;
            float xOffset = 0;
            if (dir % 2 == 0)
                yOffset = 11;
            else
                xOffset = 19;
            if (dir>1)
            {
                yOffset *= -1;
                xOffset *= -1;
            }

            Vector3 offset = new Vector3(xOffset, yOffset,0);
            GameObject newRoom = Instantiate(Resources.Load("Rooms/RoomBase") as GameObject, curRoom.transform.position + offset, Quaternion.identity) as GameObject;
            roomScript.doors[dir] = false;
            RoomStart newScript = newRoom.GetComponent<RoomStart>();
            if (dir > 1)
                newScript.doors[dir - 2] = false;
            else
                newScript.doors[dir + 2] = false;

            openRooms.Add(newRoom);
            roomScript.neighbors.Add(newRoom);
            if (roomScript.neighbors.Count >= 4)
                openRooms.Remove(curRoom);
            roomsGenerated++;
        }

    }


	// Update is called once per frame
	void Update () {
	
	}
}
