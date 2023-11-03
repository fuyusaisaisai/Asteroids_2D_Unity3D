using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct PlayerScore
{
    public PlayerScore(int inNumAsteroids)
    {
        numAsteroids = inNumAsteroids;
    }
    public int numAsteroids;
}
public class MainPlayerComp : MonoBehaviour
{
    public UnityEvent<GameObject> HitByAsteroidEvent;
    public UnityEvent<PlayerScore> GameOverEvent;
    public UnityEvent<PlayerScore> GameStartEvent;

    public UI_PlayerState uiPlayerState;
    public GameObject BulletPrefab;
    public float BulletSpeed = 1.0f;

    private int numLife = 5;
    private int numAsteroid = 0;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if(ConfigurationUtil.instance)
        {
            numLife = ConfigurationUtil.instance.PlayerMaxNumLife;
            BulletSpeed = ConfigurationUtil.instance.PlayerBulletSpeed;
        }
        
        numAsteroid = 0;

        if (uiPlayerState)
        {
            uiPlayerState.SetNumLife(numLife);
            uiPlayerState.SetNumAsteroid(numAsteroid);
        }
        else
        {
            Debug.LogError("MainPlayerComp: Cannot Find UI PlayerState.");
        }

        GameStartEvent.Invoke(new PlayerScore(numAsteroid));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log("Space");

            if (BulletPrefab)
            {
                GameObject bulletInstance = Instantiate(BulletPrefab, transform.position, transform.rotation);
                BulletComp bulletComp = bulletInstance.GetComponent<BulletComp>();

                if (bulletComp)
                {
                    bulletComp.Velocity = transform.right * BulletSpeed;
                    bulletComp.HitAsteroidEvent.AddListener(OnBulletHitAsteroid);
                    bulletComp.OutOfScreenEvent.AddListener(OnBulletOutOfScreen);
                }

            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log("Player: OnTriggerEnter2D");

        if (col.gameObject.tag == "Asteroid")
        {
            Debug.Log("Player: Hit By Asteroid");
            HitByAsteroidEvent.Invoke(this.gameObject);

            numLife--;

            if (numLife <= 0)
            {
                GameOverEvent.Invoke(new PlayerScore(numAsteroid));
                return;
            }

            if (uiPlayerState)
            {
                uiPlayerState.SetNumLife(numLife);
            }

            animator?.SetTrigger("hurt");
        }
    }
    void OnBulletHitAsteroid(GameObject bullet)
    {
        Debug.Log("OnBulletHitAsteroid");
        numAsteroid++;
        if (uiPlayerState)
        {
            uiPlayerState.SetNumAsteroid(numAsteroid);
        }

        GameObject.Destroy(bullet);
        bullet = null;
    }

    void OnBulletOutOfScreen(GameObject bullet)
    {
        // Debug.Log("OnBulletOutOfScreen");
        GameObject.Destroy(bullet);
    }

}
