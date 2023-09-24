using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Player player;

    public Animator skeleton_Animator;//애니메이션
    public bool is_atk;
    public float attack_CT;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        is_atk = false;
        speed = 2.2f;
        attack_CT = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(1.8f, 1.8f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1.8f, 1.8f);
        }

        if (is_atk == false)
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, Time.deltaTime * speed);

        /*if(is_atk == true)
        {
            speed = 0;
        }*/

        if(speed == 0)
        {
            if (attack_CT > 0)
            {
                attack_CT -= Time.deltaTime;
            }
            else
            {
                skeleton_Animator.SetTrigger("Is_Atk");
                attack_CT = 2.5f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            skeleton_Animator.SetBool("Is_Idle", true);
            speed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            skeleton_Animator.SetBool("Is_Idle", false);
            speed = 2.2f;
        }
    }

    public void Attack()
    {
        is_atk = true;
    }

    public void Attack_End()
    {
        is_atk = false;

    }
}
