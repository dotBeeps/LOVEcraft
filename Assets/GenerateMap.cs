using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateMap : MonoBehaviour {

    private int roomsGenerated = 0;
    private List<Room> rooms = new List<Room>();
    private List<GameObject> openRooms = new List<GameObject>();
    private Room[,] roomArray;


    class Room
    {
        public Room left { get; set; }
        public Room right { get; set; }
        public Room up { get; set; }
        public Room down { get; set; }

        public bool generated = false;

        public int x { get; set; }
        public int y { get; set; }

        public Room[] adjacentRooms = new Room[4];
        
        public Room()
        {
            adjacentRooms[0] = up;
            adjacentRooms[1] = right;
            adjacentRooms[2] = left;
            adjacentRooms[3] = down;
        }

        public bool openDoor()
        {
            return left == null || right == null || up == null || down == null;
        }

        public int getRandomOpen()
        {
            return getRandomOpen(Random.Range(0, 3));
        }

        public int getRandomOpen(int roomDir)
        {
            if (roomDir == 4)
            {
                roomDir = 0;
            }
            if (adjacentRooms[roomDir] == null)
            {
                return roomDir;
            } else
            {
                if (roomDir > 3)
                    return getRandomOpen(0);
                else
                    return getRandomOpen(roomDir + 1);
            }
        }

        public void setNeighbor(int dir, Room room)
        {
            adjacentRooms[dir] = room;
            if (dir == 0)
                this.up = room;
            else if (dir == 1)
                this.right = room;
            else if (dir == 2)
                this.down = room;
            else if (dir == 3)
                this.left = room;
        }
    }



	// Use this for initialization
	void Start () {
        Debug.Log("MAP GENERATOR WAS STARTED");
        GenerateRooms(100);
	}
	
    void UpdateRoomNeighbors(Room[,] roomArr, List<Room> roomL)
    {
        foreach (Room room in roomL)
        {
            room.setNeighbor(0, roomArray[room.x, room.y + 1]);
            room.setNeighbor(1, roomArray[room.x + 1, room.y]);
            room.setNeighbor(2, roomArray[room.x, room.y - 1]);
            room.setNeighbor(3, roomArray[room.x - 1, room.y]);
        }
    }

    void PrintRoomArray(Room[,] roomArr)
    {
        string arrayString = "";
        int x = roomArr.GetLength(0);
        int y = roomArr.GetLength(1);
        for (int i = 0; i < y;i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (roomArr[j, i] == null)
                    arrayString += "O";
                else
                    arrayString += "X";
            }
            arrayString += "\n";
        }
        Debug.Log(arrayString);
    }



    void GenerateRooms(int numRooms)
    {
        roomArray = new Room[numRooms+numRooms/2, numRooms+numRooms/2];

        Room firstRoom = new Room();
        firstRoom.x = numRooms / 2;
        firstRoom.y = numRooms / 2;

        roomArray[firstRoom.x, firstRoom.y] = firstRoom;
        rooms.Add(firstRoom);

        while (rooms.Count < numRooms)
        {
            Room newRoom = new Room();
            IEnumerable<Room> potentialRooms = rooms.Where(e => e.openDoor());
            int index = Random.Range(0, potentialRooms.Count()-1);
            Room parentRoom = potentialRooms.ElementAt(index);
            int dir = parentRoom.getRandomOpen();
            if (parentRoom.adjacentRooms[dir] != null)
            {
                Debug.Log("EXTREME PROBLEMS");
            }
            parentRoom.setNeighbor(dir, newRoom);
            if (dir == 0)
            {
                newRoom.x = parentRoom.x;
                newRoom.y = parentRoom.y + 1;
            } else if (dir == 1)
            {
                newRoom.x = parentRoom.x + 1;
                newRoom.y = parentRoom.y;
            } else if (dir == 2)
            {
                newRoom.x = parentRoom.x;
                newRoom.y = parentRoom.y - 1;
            } else if (dir == 3)
            {
                newRoom.x = parentRoom.x - 1;
                newRoom.y = parentRoom.y;
            }
            if (roomArray[newRoom.x, newRoom.y] != null)
                Debug.Log("THERE'S PROBLEMS");

            roomArray[newRoom.x, newRoom.y] = newRoom;

            UpdateRoomNeighbors(roomArray, rooms);

            rooms.Add(newRoom);
        }
        PrintRoomArray(roomArray);
        InitiateRooms(firstRoom, Vector3.zero);
    }

    void InitiateRooms(Room firstRoom, Vector3 pos)
    {
        makeNewRoom(pos,firstRoom.x,firstRoom.y);
        firstRoom.generated = true;
        for (int i = 0; i < 4; i++)
        {
            if (firstRoom.adjacentRooms[i] != null && !firstRoom.adjacentRooms[i].generated)
            {
                InitiateRooms(firstRoom.adjacentRooms[i], pos + getOffset(i));
            }
        }
    }

    GameObject makeNewRoom(Vector3 curRoomTransform,int x, int y)
    {
        GameObject room = Instantiate(Resources.Load("Rooms/RoomBase") as GameObject, curRoomTransform, Quaternion.identity) as GameObject;
        RoomStart roomScript = room.GetComponent<RoomStart>();
        if (roomArray[x, y + 1] != null)
            roomScript.northDoor = true;
        if (roomArray[x, y - 1] != null)
            roomScript.southDoor = true;
        if (roomArray[x+1, y] != null)
            roomScript.eastDoor = true;
        if (roomArray[x-1, y] != null)
            roomScript.westDoor = true;

        return room;
    }

    bool isObstructed(int dir, Transform curRoomTransform)
    {
        return Physics2D.OverlapCircle(curRoomTransform.position + getOffset(dir), 1f);
    }

    Vector3 getOffset(int dir)
    {
        if (dir == 0)
            return new Vector3(0, 11);
        else if (dir == 1)
            return new Vector3(19, 0);
        else if (dir == 2)
            return new Vector3(0, -11);
        else
            return new Vector3(-19, 0);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
