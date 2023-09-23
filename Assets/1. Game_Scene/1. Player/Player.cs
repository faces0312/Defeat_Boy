using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game_Manager gm;

    public Animator player_Animator;//�ִϸ��̼�

    //����
    public bool is_Attack;
    public float attack_next_Time;//���� �������� ��Ÿ�� (0.25�� �̳��� �ѹ� �� ����
    public int attack_Cnt;//�޺� ���� Ƚ��
    
    public bool move_right;//���������� ���� �ִ���

    public float speed;//�ӵ�

    //Roll
    public bool is_roll;//������ �ִ���
    //Run
    public bool is_Run;//�ٰ� �ִ�
    //���¹̳�
    public bool is_stamina;//���¹̳� �ִ���



    public float xMove;//�¿� �Է�
    public float yMove;//���� �Է�
    // Start is called before the first frame update
    void Start()
    {
        is_Attack = false;
        is_Run = false;
        is_stamina = true;
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Roll();

        Move();

        Attack();

        Run();

        Stamina_Reload();
        Stamina_Loading();
        Stamina();
    }

    public void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gm.stamina.localScale.x >= 0.2f && is_stamina == true)
        {
            gm.stamina.localScale = new Vector2(gm.stamina.localScale.x - 0.25f, gm.stamina.localScale.y);
            speed = 5.5f;
            is_roll = true;
            player_Animator.SetTrigger("Is_Roll");

        }
    }
    public void Roll_End()
    {
        speed = 3f;
        is_roll = false;
    }

    public void Attack()
    {
        attack_next_Time += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && is_roll == false && attack_next_Time > 0.25f)
        {
            is_Attack = true;
            speed = 0;
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

            // Reset Timer and Direction
            attack_next_Time = 0.0f;
        }
    }
    public void Attack_End()
    {
        is_Attack = false;
        if (move_right == true)
        {
            gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        else
            gameObject.transform.localScale = new Vector2(-0.7f, 0.7f);
        speed = 3f;
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
            move_right = true;
            gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.A))//���� �̵�
        {
            move_right = false;
            gameObject.transform.localScale = new Vector2(-0.7f, 0.7f);
        }
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && gm.stamina.localScale.x > 0 && is_stamina == true && is_Attack == false)
        {
            is_Run = true;
            speed = 5.5f;
            gm.stamina.localScale = new Vector2(gm.stamina.localScale.x - 0.002f, gm.stamina.localScale.y); 
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            is_Run = false;
        }
    }

    public void Stamina()
    {
        if(is_Run == false)
        {
            if(gm.stamina.localScale.x <= 1)
                gm.stamina.localScale = new Vector2(gm.stamina.localScale.x + 0.001f, gm.stamina.localScale.y);
        }
    }

    public void Stamina_Loading()
    {
        if(gm.stamina.localScale.x <= 0)
        {
            gm.stamina.gameObject.SetActive(false);

            gm.stamina_loading.localScale = new Vector2(0f, gm.stamina_loading.localScale.y);
            gm.stamina_loading.gameObject.SetActive(true);

            is_stamina = false;
        }
    }

    public void Stamina_Reload()
    {
        if (gm.stamina_loading.localScale.x <= 1)
            gm.stamina_loading.localScale = new Vector2(gm.stamina_loading.localScale.x + 0.001f, gm.stamina_loading.localScale.y);

        if (gm.stamina.localScale.x >= 1 && is_stamina == false)
        {
            gm.stamina.gameObject.SetActive(true);
            gm.stamina_loading.gameObject.SetActive(false);
            is_stamina = true;
        }
    }
}