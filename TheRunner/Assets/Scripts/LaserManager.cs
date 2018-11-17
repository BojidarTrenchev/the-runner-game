using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour
{
    public Sprite laserOn;
    public Sprite laserOff;
    public float rotateAngle = 50f;
    public float timeInterval = 1;

    private BoxCollider2D laserCollider;
    private SpriteRenderer laserRenderer;
    private Transform laserTransform;
    private bool laserEnabled = true;
    private float timeSinceStart;

    void Start()
    {
        this.laserCollider = this.GetComponent<BoxCollider2D>();
        this.laserTransform = this.GetComponent<Transform>();
        this.laserRenderer = this.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        this.timeSinceStart += Time.deltaTime;
        if (timeSinceStart > this.timeInterval)
        {
            this.timeSinceStart = 0;
            TurnLaserOnOff();
        }
        this.laserTransform.Rotate(new Vector3(0, 0, this.rotateAngle * Time.deltaTime));
    }

    private void TurnLaserOnOff()
    {
        this.laserEnabled = !this.laserEnabled;
        this.laserRenderer.sprite = this.laserEnabled ? this.laserOn : this.laserOff;
        this.laserCollider.enabled = laserEnabled; 
    }
}
