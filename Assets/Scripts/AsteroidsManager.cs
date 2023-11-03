using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    public float SpawnTimeInterval = 1.0f;

    public float SpawnHalfAngle = 30.0f;

    private float accTime = 0.0f;
    public GameObject[] AsteroidPrefabArray;

    public float AsteroidVelocity = 1.0f;
    public float SplitAngle = 30.0f;

    Camera cam;

    public GameObject ExplodeAnimatorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (ConfigurationUtil.instance)
        {
            SpawnTimeInterval = ConfigurationUtil.instance.AsteroidSpawnTimeInterval;
            SpawnHalfAngle = ConfigurationUtil.instance.AsteroidSpawnHalfAngle;
            AsteroidVelocity = ConfigurationUtil.instance.AsteroidVelocity;
            SplitAngle = ConfigurationUtil.instance.AsteroidSplitAngle;
        }

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        accTime += Time.deltaTime;

        if(accTime > SpawnTimeInterval)
        {
            SpawnAsteroidRandom();
            accTime = 0;
        }
    }

    enum EScreenEdge
    {
        Left, Top, Right, Bottom
    };


    void SpawnAsteroidRandom()
    {
        // pos
        int edge_int = Random.Range(0, 3);
        EScreenEdge edge = (EScreenEdge)edge_int;

        float edge_ratio = Random.Range(0, 1.0f);

        Vector2 spawnPosition_Screen = Vector2.zero;

        switch (edge)
        {
            case EScreenEdge.Left:
                // Debug.Log("SpawnAsteroidRandom Left");
                spawnPosition_Screen = new Vector2(0, Screen.height * edge_ratio);
                break;
            case EScreenEdge.Top:
                // Debug.Log("SpawnAsteroidRandom Top");
                spawnPosition_Screen = new Vector2(Screen.width * edge_ratio, Screen.height);
                break;
            case EScreenEdge.Right:
                // Debug.Log("SpawnAsteroidRandom Right");
                spawnPosition_Screen = new Vector2(Screen.width, Screen.height * edge_ratio);
                break;
            case EScreenEdge.Bottom:
                // Debug.Log("SpawnAsteroidRandom Left");
                spawnPosition_Screen = new Vector2(Screen.width * edge_ratio, 0);
                break;
        }

        Vector3 spawnPosition = cam.ScreenToWorldPoint(spawnPosition_Screen);

        // rot
        float spawnRotation_360 = Random.Range(0, 360.0f);
        Quaternion spawnRotation = Quaternion.AngleAxis(spawnRotation_360, Vector3.forward);

        // velocity
        Vector3 spawnEdgeNormalDirection = Vector3.zero;

        switch (edge)
        {
            case EScreenEdge.Left:
                spawnEdgeNormalDirection = new Vector3(1.0f, 0, 0);
                break;
            case EScreenEdge.Top:
                spawnEdgeNormalDirection = new Vector3(0, -1.0f, 0);
                break;
            case EScreenEdge.Right:
                spawnEdgeNormalDirection = new Vector3(-1.0f, 0, 0);
                break;
            case EScreenEdge.Bottom:
                spawnEdgeNormalDirection = new Vector3(0, 1.0f, 0);
                break;
        }

        //Debug.LogFormat("SpawnAsteroidRandom spawnEdgeNormalDirection {0}", spawnEdgeNormalDirection);

        float spawnAngle = Random.Range(-SpawnHalfAngle, SpawnHalfAngle);

        Vector3 spawnVelocity = Quaternion.AngleAxis(spawnAngle, Vector3.forward) * spawnEdgeNormalDirection * AsteroidVelocity;

        //Debug.LogFormat("SpawnAsteroidRandom spawnVelocity {0}", spawnVelocity);

        // type
        int level = Random.Range(0, AsteroidPrefabArray.Length);

        SpawnAsteroid(spawnPosition, spawnRotation, spawnVelocity, level);
        
    }

    void SpawnAsteroid(Vector2 pos, Quaternion rot, Vector3 velocity, int level)
    {
        GameObject AsteroidPrefab = AsteroidPrefabArray[level];
        if (AsteroidPrefab)
        {
            GameObject asteroidInstance = Instantiate(AsteroidPrefab, pos, rot);

            AsteroidComp asteroidComp = asteroidInstance.GetComponent<AsteroidComp>();

            if (asteroidComp)
            {
                //Debug.LogFormat("SpawnAsteroid {0}", velocity);
                asteroidComp.level = level;
                asteroidComp.velocity = velocity;
                asteroidComp.HitByBulletEvent.AddListener(OnAsteroidHitByBullet);
                asteroidComp.OutOfScreenEvent.AddListener(OnAsteroidOutOfScreen);
            }
        }

    }


    void OnAsteroidHitByBullet(AsteroidComp asteroidComp)
    {
        if(!asteroidComp)
        {
            Debug.LogError("OnAsteroidHitByBullet AsteroidComp null");
            return;
        }

        if(ExplodeAnimatorPrefab)
        {
            GameObject explodeAnimatorGo = Instantiate(ExplodeAnimatorPrefab, asteroidComp.gameObject.transform.position, asteroidComp.gameObject.transform.rotation);
        }
        if (asteroidComp.level < AsteroidPrefabArray.Length - 1)
        {
            // Split to two
            {
                float spawnRotation_360 = Random.Range(0, 360.0f);
                Quaternion spawnRotation = Quaternion.AngleAxis(spawnRotation_360, Vector3.forward);

                SpawnAsteroid(
                    asteroidComp.transform.position,
                    spawnRotation,
                    Quaternion.AngleAxis(SplitAngle, Vector3.forward) * asteroidComp.velocity,
                    asteroidComp.level + 1
                );
            }
            {
                float spawnRotation_360 = Random.Range(0, 360.0f);
                Quaternion spawnRotation = Quaternion.AngleAxis(spawnRotation_360, Vector3.forward);

                SpawnAsteroid(
                    asteroidComp.transform.position,
                    spawnRotation,
                    Quaternion.AngleAxis(-SplitAngle, Vector3.forward) * asteroidComp.velocity,
                    asteroidComp.level + 1
                );
            }
        }

        GameObject.Destroy(asteroidComp.gameObject);
    }

    void OnAsteroidOutOfScreen(GameObject asteroid)
    {
        if (asteroid)
        {
            GameObject.Destroy(asteroid);
            asteroid = null;
        }
        
    }
}
