using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game_Manager gm;

    public Animator player_Animator;//애니메이션

    //공격
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public bool is_Attack;
    public float attack_next_Time;//어택 다음공격 쿨타임 (0.25초 이내면 한번 더 공격
    public int attack_Cnt;//콤보 공격 횟수
    
    public bool move_right;//오른쪽으로 가고 있는지

    public float speed;//속도

    //Roll
    public bool is_roll;//구르고 있는지
    //Run
    public bool is_Run;//뛰고 있다
    //스태미나
    public bool is_stamina;//스태미나 있는지
    //맞았을 때
    public bool is_Dmg;


    public float xMove;//좌우 입력
    public float yMove;//상하 입력
    // Start is called before the first frame update
    void Start()
    {
        move_right = true;
        is_Attack = false;
        is_Run = false;
        is_stamina = true;
        is_Dmg = false;
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
            is_Dmg = false;
            gameObject.tag = "Player_Invi";
            gm.stamina.localScale = new Vector2(gm.stamina.localScale.x - 0.25f, gm.stamina.localScale.y);
            speed = 5.5f;
            is_roll = true;
            player_Animator.SetTrigger("Is_Roll");

        }
    }
    public void Roll_End()
    {
        gameObject.tag = "Player";
        speed = 3f;
        is_roll = false;
    }

    public void Attack()
    {
        attack_next_Time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J) && is_roll == false && attack_next_Time > 0.25f && is_Dmg == false)
        {
            is_Attack = true;
            speed = 0;
            /*Vector3 mouse_Point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y ));
            if(mouse_Point.x > gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
            }
            else if (mouse_Point.x < gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector2(-0.7f, 0.7f);
            }*/

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

    public void Attack1()
    {
        attack1.gameObject.SetActive(true);
        Invoke("Dis_Attack1", 0.03f);
    }

    public void Attack2()
    {
        attack2.gameObject.SetActive(true);
        Invoke("Dis_Attack2", 0.03f);
    }

    public void Attack3()
    {
        attack3.gameObject.SetActive(true);
        Invoke("Dis_Attack3", 0.03f);
    }

    public void Dis_Attack1()
    {
        attack1.gameObject.SetActive(false);
    }
    public void Dis_Attack2()
    {
        attack2.gameObject.SetActive(false);
    }
    public void Dis_Attack3()
    {
        attack3.gameObject.SetActive(false);
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

        /*if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }*/

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
            move_right = true;
            gameObject.transform.localScale = new Vector2(0.7f, 0.7f);
        }
        if (Input.GetKeyDown(KeyCode.A))//왼쪽 이동
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
            speed = 3f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Skeleton_Attack" && gameObject.tag == "Player")
        {
            is_Dmg = true;
            player_Animator.SetTrigger("Dmg");
            gameObject.tag = "Player_Invi";
            gm.hp.localScale = new Vector2(gm.hp.localScale.x - 0.2f, gm.hp.localScale.y);
        }
    }

    public void Dmg_End()
    {
        gameObject.tag = "Player";
        is_Dmg = false;
        speed = 3f;
    }
}