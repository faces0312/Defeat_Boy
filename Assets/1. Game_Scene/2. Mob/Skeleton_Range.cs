using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Range : MonoBehaviour
{
    //���� ����

    public Skeleton skeleton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Player_Invi")
        {
            skeleton.skeleton_Animator.SetBool("Is_Idle", true);
            skeleton.attack_Load_Col = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Player_Invi")
        {
            skeleton.speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Player_Invi")
        {
            skeleton.attack_Load_Col = false;
            skeleton.skeleton_Animator.SetBool("Is_Idle", false);
            if (skeleton.is_atk == false)
                skeleton.speed = 75f;
        }
    }
}
