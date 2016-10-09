using UnityEngine;
using System.Collections;

public class Obtainable
{
    enum Type { LockedChest, UnlockedChest, OpenChest, Item };
}


public interface IObtainable {

    void setObtainableType(int type);

    void setItemId(int id);
	
}
