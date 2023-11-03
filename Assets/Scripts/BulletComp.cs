using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class BulletComp : MonoBehaviour
{
    public UnityEvent<GameObject> HitAsteroidEvent;
    public UnityEvent<GameObject> OutOfScreenEvent;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bullet Spawned");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;

        
        Vector3 position2D = cam.WorldToScreenPoint(transform.position);
        if (position2D.x < 0 || position2D.x > Screen.width || position2D.y < 0 || position2D.y > Screen.height)
        {
            Debug.Log("Out of screen");

            // thumpthump
            OutOfScreenEvent.Invoke(this.gameObject);
        }
            
    }

    public Vector3 Velocity
    {
        get { return velocity; }
        set
        {
             velocity = value;
        }
    }
    private Vector3 velocity;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Asteroid")
        {
            Debug.Log("Bullet: Hit Asteroid");
            HitAsteroidEvent.Invoke(this.gameObject);
        }
    }
}
