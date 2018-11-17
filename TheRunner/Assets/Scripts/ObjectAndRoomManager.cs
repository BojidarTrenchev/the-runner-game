using UnityEngine;
using System.Collections.Generic;

public class ObjectAndRoomManager : MonoBehaviour
{
    public List<GameObject> availableRooms;

    private Transform backgroundParent;
    private float distanceBetweenRooms;
    private float distanceBetweenForegrounds;

    void Start()
    {
        this.backgroundParent = GameObject.Find("Background").GetComponent<Transform>();
        this.distanceBetweenRooms = CalculateDistance(GameObject.FindGameObjectsWithTag("Room"));
        this.distanceBetweenForegrounds = CalculateDistance(GameObject.FindGameObjectsWithTag("FieldForeground"));
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Room"))
        {
            var oldRoomX = collider.gameObject.transform.position.x;
            Destroy(collider.gameObject);

            int index = Random.Range(0, this.availableRooms.Count);
            var newRoom = Instantiate(availableRooms[index]);
            newRoom.transform.parent = backgroundParent;

            var newRoomPosition = newRoom.transform.position;
            newRoomPosition.x = oldRoomX + this.distanceBetweenRooms;
            newRoom.transform.position = newRoomPosition;
        }
        else if(collider.CompareTag("FieldForeground"))
        {
            var currentPosition = collider.transform.position;
            currentPosition.x += this.distanceBetweenForegrounds;
            collider.transform.position = currentPosition;
        }
    }

    private float CalculateDistance(GameObject[] currentObjects)
    {
        if (currentObjects.Length < 2)
        {
            throw new System.ArgumentException("There must be at least 2 rooms in the scene!");
        }
        float distance = 0;
        float mindistance = float.MaxValue;

        for (int i = 1; i < currentObjects.Length; i++)
        {
            distance = Mathf.Abs(currentObjects[i].transform.position.x - currentObjects[i - 1].transform.position.x);
            if (distance < mindistance)
            {
                mindistance = distance;
            }
        }
        return mindistance * currentObjects.Length;
    }
}
