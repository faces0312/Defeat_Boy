using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Player player;

    public Animator skeleton_Animator;//애니메이션
    
    public GameObject attack_Col;

    public bool is_atk;
    public bool attack_Load_Col;
    public float attack_CT;

    public GameObject attack_Loading;

    public float hp;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        hp = 5;

        is_atk = false;
        speed = 2.2f;
        attack_CT = 1f;
    }

    private void OnEnable()
    {
        attack_Col.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x && is_atk == false)
        {
            gameObject.transform.localScale = new Vector3(1.8f, 1.8f);
        }
        else if (player.transform.position.x <= transform.position.x && is_atk == false)
        {
            gameObject.transform.localScale = new Vector3(-1.8f, 1.8f);
        }

        if (speed == 0 || attack_Load_Col ==true)
        {
            if (attack_CT > 0)
            {
                attack_CT -= Time.deltaTime;
            }
            else if(attack_CT <= 0)
            {
                skeleton_Animator.SetTrigger("Is_Atk");
                attack_CT = 2f;
            }
        }


        if (is_atk == false)
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            skeleton_Animator.SetBool("Is_Idle", true);
            attack_Load_Col = true;
            speed = 0;
        }

        if(collision.tag == "Player_Attack1" && is_atk == false)
        {
            skeleton_Animator.SetTrigger("Is_Dmg");
        }
        if (collision.tag == "Player_Attack2" && is_atk == false)
        {
            skeleton_Animator.SetTrigger("Is_Dmg");
        }
        if (collision.tag == "Player_Attack3" && is_atk == false)
        {
            skeleton_Animator.SetTrigger("Is_Dmg");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attack_Load_Col = false;
            skeleton_Animator.SetBool("Is_Idle", false);
            speed = 2.2f;
        }
    }

    public void Attack()
    {
        attack_Loading.gameObject.SetActive(true);
        is_atk = true;
        speed = 0;
    }

    public void Attack_LoadingEnd()
    {
        attack_Loading.gameObject.SetActive(false);
    }

    public void Damage()
    {
        attack_Col.gameObject.SetActive(true);
        Invoke("Dis_Damage", 0.1f);
    }
    private void Dis_Damage()
    {
        attack_Col.gameObject.SetActive(false);
    }

    public void Attack_End()
    {
        speed = 2.2f;
        is_atk = false;
    }
}
