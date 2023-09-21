using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator player_Animator;//애니메이션

    public float attack_next_Time;//어택 다음공격 쿨타임 (0.25초 이내면 한번 더 공격
    public int attack_Cnt;
    public bool is_roll;//구르고 있는지
    public float speed;//속도


    public float xMove;//좌우 입력
    public float yMove;//상하 입력
    // Start is called before the first frame update
    void Start()
    {
        speed = 4.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Roll();

        Move();

        Attack();
    }

    public void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            speed = 4.5f;
            is_roll = true;
            player_Animator.SetTrigger("Is_Roll");

        }
    }
    public void Roll_End()
    {
        is_roll = false;
    }

    public void Attack_Start()
    {
        speed = 0;
    }
    public void Attack()
    {
        attack_next_Time += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && is_roll == false && attack_next_Time > 0.25f)
        {
            Vector3 mouse_Point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y ));
            if(mouse_Point.x > gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
            }
            else if (mouse_Point.x < gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector2(-0.7f, 0.7f);
            }
            attack_Cnt++;

            if(attack_Cnt > 3)
            {
                attack_Cnt = 1;
            }

            if (attack_next_Time > 1.0f)
                attack_Cnt = 1;

            player_Animator.SetTrigger("Attack" + attack_Cnt);

            // Reset timer
            attack_next_Time = 0.0f;
        }
    }
    public void Attack_End()
    {
        speed = 4.5f;
    }

    public void Move()
    {
        //움직임 구현
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }

        //움직임 애니메이션
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            player_Animator.SetBool("Is_Run", true);
        }
        else
        {
            player_Animator.SetBool("Is_Run", false);
        }

        //좌우 반전
        if (Input.GetKeyDown(KeyCode.D))//오른쪽 이동
        {
            gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.A))//왼쪽 이동
        {
            gameObject.transform.localScale = new Vector2(-0.7f, 0.7f);
        }
    }
}
