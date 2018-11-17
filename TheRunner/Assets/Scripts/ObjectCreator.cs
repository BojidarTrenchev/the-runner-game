using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObjectCreator : MonoBehaviour
{
    public List<GameObject> objects;
    public Transform objPositioner;
    private PlayerController player;
    private Transform parent;
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.parent = GameObject.Find("Objects").GetComponent<Transform>();        
    }    

    private void GenerateObject(float maxDistanceX)
    {
        int rd = Random.Range(0, this.objects.Count);
        float rdY = Random.Range(-1.1f, 1.1f);
        var newObj = Instantiate(this.objects[rd]);
        var newObjPosition = newObj.transform.position;
        newObjPosition.x = this.objPositioner.position.x;
        newObjPosition.y = rdY;
        newObj.transform.position = newObjPosition;

        newObj.transform.parent = this.parent;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("LaserCollider") || collider.CompareTag("CoinContainer"))
        {
            Destroy(collider.gameObject);
            this.GenerateObject(this.parent.position.x + (player.forwardSpeed - player.initialForwardSpeed));
        }
    }

}
