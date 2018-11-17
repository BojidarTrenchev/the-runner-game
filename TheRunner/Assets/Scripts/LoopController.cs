using UnityEngine;
using System.Collections;

public class LoopController : MonoBehaviour
{
    private GameObject player;
    private Transform tr;
    private float offset;
    void Start()
    {
        tr = this.GetComponent<Transform>();
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.offset = this.transform.position.x - this.player.transform.position.x;
    }
    
    void FixedUpdate()
    {
        var currentPosition = this.tr.position;
        currentPosition.x = offset + this.player.transform.position.x;
        this.tr.position = currentPosition;
    }
}
