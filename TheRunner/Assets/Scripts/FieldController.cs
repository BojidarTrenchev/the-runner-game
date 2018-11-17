using UnityEngine;
using System.Collections;

public class FieldController : MonoBehaviour
{
    private Rigidbody2D[] fieldForegroundRBs;
    private PlayerController player;
    private float fieldForegroundSpeed;
    private float fieldBackgroundSpeed;
    
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        var foregrounds = GameObject.FindGameObjectsWithTag("FieldForeground");
        this.fieldForegroundRBs = new Rigidbody2D[foregrounds.Length];
        for (int i = 0; i < this.fieldForegroundRBs.Length; i++)
        {
            this.fieldForegroundRBs[i] = foregrounds[i].GetComponent<Rigidbody2D>();
        }

    }
    
    void FixedUpdate()
    {
        this.fieldForegroundSpeed = player.forwardSpeed / 1.5f;
        var currentVelocity = new Vector2();

        foreach (var foreground in this.fieldForegroundRBs)
        {
            currentVelocity = foreground.velocity;
            if (player.forwardSpeed != 0)
            {
                currentVelocity.x = this.fieldForegroundSpeed;
            }
            else
            {
                currentVelocity.x = 0;
            }

            foreground.velocity = currentVelocity;
        }


    }
}
