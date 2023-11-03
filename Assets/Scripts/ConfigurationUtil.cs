using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationUtil : MonoBehaviour
{
    public static ConfigurationUtil instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }else
        {
            instance = this;
        }
    }

    [Header("Player Moving Mpeed")]
    public float PlayerTranslateSpeed = 5.0f;

    [Header("Player Rotation Mpeed")]
    public float PlayerRotateSpeed = 10.0f;

    [Header("Player Bullet Mpeed")]
    public float PlayerBulletSpeed = 0.01f;

    [Header("Player Max Number of Lives")]
    public int PlayerMaxNumLife = 10;

    [Header("Asteroids will be spawn in every {X} seconds")]
    public float AsteroidSpawnTimeInterval = 1.0f;

    [Header("Asteroids will be spawn Between -{Angle} to {Angle}")]
    public float AsteroidSpawnHalfAngle = 30.0f;

    [Header("Asteroids will be spawn with Speed {X}")]
    public float AsteroidVelocity = 0.01f;

    [Header("Asteroids will be split at -{Angle} and {Angle}")]
    public float AsteroidSplitAngle = 30.0f;
}
