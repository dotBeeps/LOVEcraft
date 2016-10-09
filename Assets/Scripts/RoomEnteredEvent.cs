using UnityEngine;
using System.Collections;

public class RoomEnteredEvent : MonoBehaviour
{
    public Color inMiniMap;
    public Color outMiniMap;
    public Color unexploredMiniMap;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, -10);
            RoomStart roomScr = GetComponent<RoomStart>();
            Vector3 miniPos = new Vector3(roomScr.miniMapPart.transform.position.x, roomScr.miniMapPart.transform.position.y, -10);
            GameObject.FindGameObjectWithTag("MiniMapCamera").SendMessage("updateTarget", miniPos);
            GameObject.FindGameObjectWithTag("MainCamera").SendMessage("updateTarget", pos);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        RoomStart roomScr = GetComponent<RoomStart>();
        roomScr.miniMapPart.SetActive(true);
        roomScr.miniMapPart.GetComponent<SpriteRenderer>().color = inMiniMap;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        RoomStart roomScr = GetComponent<RoomStart>();
        roomScr.miniMapPart.GetComponent<SpriteRenderer>().color = outMiniMap;
    }
}
