using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InputControlComp : MonoBehaviour
{
    public float TranslateSpeed = 5.0f;
    public float RotateSpeed = 50.0f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        if (ConfigurationUtil.instance)
        {
            TranslateSpeed = ConfigurationUtil.instance.PlayerTranslateSpeed;
            RotateSpeed = ConfigurationUtil.instance.PlayerRotateSpeed;
        }

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        

        // Rotation

        float DeltaRotate = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("LeftArrow");
            DeltaRotate += 1.0f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("RightArrow");
            DeltaRotate += -1.0f;
        }

        transform.RotateAround(transform.position, Vector3.forward, DeltaRotate * RotateSpeed * Time.deltaTime);

        // Translation
        Vector3 DeltaTranslate = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A");
            DeltaTranslate += new Vector3(-1.0f, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("S");
            DeltaTranslate += new Vector3(0, -1.0f, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("D");
            DeltaTranslate += new Vector3(1.0f, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("W");
            DeltaTranslate += new Vector3(0, 1.0f, 0);
        }

        Vector3 tempPosition = transform.position + DeltaTranslate * TranslateSpeed * Time.deltaTime;

        Vector3 position2D = cam.WorldToScreenPoint(tempPosition);

        if (position2D.x <= 0 || position2D.x >= Screen.width || position2D.y <= 0 || position2D.y >= Screen.height)
        {
            return;
        }

        transform.position = tempPosition;
    }

}
