using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    public float minXClamp;
    public float maxXClamp;


    private void LateUpdate()
    {
        if (GameManager.Instance.playerInstance == null) return;
        Vector3 cameraPos = transform.position;
        cameraPos.x = Mathf.Clamp(GameManager.Instance.playerInstance.transform.position.x, minXClamp, maxXClamp);
        transform.position = cameraPos;
    }
}