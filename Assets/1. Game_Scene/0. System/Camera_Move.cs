using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public GameObject player;

    float camera_speed = 6f;

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y + 2f);
        Vector3 moveVector = new Vector3(dir.x * camera_speed * Time.deltaTime, dir.y * camera_speed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
