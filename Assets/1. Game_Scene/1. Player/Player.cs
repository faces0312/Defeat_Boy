using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator player_Animator;//�ִϸ��̼�

    public float attack_next_Time;//���� �������� ��Ÿ�� (0.25�� �̳��� �ѹ� �� ����
    public int attack_Cnt;
    public bool is_roll;//������ �ִ���
    public float speed;//�ӵ�


    public float xMove;//�¿� �Է�
    public float yMove;//���� �Է�
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
        //������ ����
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

        //������ �ִϸ��̼�
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            player_Animator.SetBool("Is_Run", true);
        }
        else
        {
            player_Animator.SetBool("Is_Run", false);
        }

        //�¿� ����
        if (Input.GetKeyDown(KeyCode.D))//������ �̵�
        {
            gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.A))//���� �̵�
        {
            gameObject.transform.localScale = new Vector2(-0.7f, 0.7f);
        }
    }
}
