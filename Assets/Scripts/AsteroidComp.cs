using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AsteroidComp : MonoBehaviour
{
    public UnityEvent<AsteroidComp> HitByBulletEvent;
    public UnityEvent<GameObject> OutOfScreenEvent;

    [HideInInspector]
    public Vector3 velocity;

    [HideInInspector]
    public int level;
    private Camera cam;

    static private float destroyThreshold = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;


        Vector3 position2D = cam.WorldToScreenPoint(transform.position);
        if (position2D.x < 0 - destroyThreshold || position2D.x > Screen.width + destroyThreshold || position2D.y < 0 - destroyThreshold || position2D.y > Screen.height + destroyThreshold)
        {
            Debug.Log("Out of screen");

            OutOfScreenEvent.Invoke(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Asteroid: Hit By Bullet");
            HitByBulletEvent.Invoke(this);
        }
    }
}
