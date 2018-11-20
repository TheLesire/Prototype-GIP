using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    public Transform Player;
    public float cameraDistance = 30.0f;
    public float Hoogte;

    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    void FixedUpdate()
    {
        Height();
        transform.position = new Vector3(Player.position.x, Hoogte, -10);
    }

    void Height()
    {
        if(Player.transform.position.y > 7)
        {
            Hoogte = 7;
        }
        else
        {
            Hoogte = 5;
        }
    }
}
