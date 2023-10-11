using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Recog_Stage1 : MonoBehaviour
{
    //캐릭터 인식
    public Skeleton_Stage1 skeleton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Player_Invi")
        {
            //Debug.Log("인식 설정");
            skeleton.player_Recog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Player_Invi")
        {
            //Debug.Log("인식 해제");
            //skeleton.Think_Move();
            skeleton.player_Recog = false;
        }
    }
}
