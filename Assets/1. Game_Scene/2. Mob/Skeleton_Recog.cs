using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Recog : MonoBehaviour
{
    public Skeleton skeleton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("�ν� ����");
            skeleton.CancelInvoke();
            skeleton.player_Recog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("�ν� ����");
            skeleton.Think_Move();
            skeleton.player_Recog = false;
        }
    }
}
