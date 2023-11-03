using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyTimerComp : MonoBehaviour
{
    public Animator animator;

    float accTime = 0;
    float timeLength = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        timeLength = currentClipInfo[0].clip.length;
        Debug.Log("SelfDestroyTimerComp" + timeLength);
        accTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        accTime += Time.deltaTime;

        if(accTime > timeLength)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
