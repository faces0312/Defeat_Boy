using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    //움직임
    public Rigidbody2D mob_Rigid;
    public int nextMove;

    public Animator skeleton_Animator;//애니메이션

    //플레이어 인식
    public bool player_Recog;

    public GameObject attack_Col;

    public bool is_atk;
    public bool attack_Load_Col;
    public float attack_CT;

    public GameObject attack_Loading;

    public float hp;
    public float speed;

    public float next_Time;
    // Start is called before the first frame update
    void Awake()
    {
        hp = 12;

        player_Recog = false;

        is_atk = false;
        speed = 75f;
        attack_CT = 1f;


        Think_Move();
    }

    private void OnEnable()
    {
        attack_Col.gameObject.SetActive(false);
        attack_Loading.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            gameObject.SetActive(false);
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
    }

    private void FixedUpdate()
    {if (player_Recog == true)
        {
            if (Player.player.gameObject.transform.position.x > transform.position.x && is_atk == false)
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(mob_Rigid.position.x + 0.25f, mob_Rigid.position.y), Vector3.down, 1, LayerMask.GetMask("Ground"));
                gameObject.transform.localScale = new Vector3(1.8f, 1.8f);
                if(raycastHit.collider == null)
                    mob_Rigid.velocity = new Vector2(0, mob_Rigid.velocity.y);
                else
                    mob_Rigid.velocity = new Vector2(Time.deltaTime * speed, mob_Rigid.velocity.y);
            }
            else if (Player.player.gameObject.transform.position.x <= transform.position.x && is_atk == false)
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(mob_Rigid.position.x - 0.25f, mob_Rigid.position.y), Vector3.down, 1, LayerMask.GetMask("Ground"));
                gameObject.transform.localScale = new Vector3(-1.8f, 1.8f);
                if (raycastHit.collider == null)
                    mob_Rigid.velocity = new Vector2(0, mob_Rigid.velocity.y);
                else
                    mob_Rigid.velocity = new Vector2(-1 * Time.deltaTime * speed, mob_Rigid.velocity.y);
            }
        }
        else
        {
            if (nextMove == 1 && is_atk == false)
            {
                gameObject.transform.localScale = new Vector3(1.8f, 1.8f);
            }
            else if (nextMove == -1 && is_atk == false)
            {
                gameObject.transform.localScale = new Vector3(-1.8f, 1.8f);
            }
            //기본 이동
            mob_Rigid.velocity = new Vector2(nextMove * Time.deltaTime * speed, mob_Rigid.velocity.y);

            //낭떠러지 체크
            Vector2 frontVec = new Vector2(mob_Rigid.position.x + nextMove * 0.25f, mob_Rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

            if (rayHit.collider == null)
            {
                nextMove *= -1;
                CancelInvoke("Think_Move");
                Invoke("Think_Move", 1.5f);
            }
        }
    }
    //다음 이동
    public void Think_Move()
    {
        nextMove = Random.Range(-1, 2);
        
        if(nextMove == 0 && player_Recog == false)
        {
            skeleton_Animator.SetBool("Is_Idle", true);
        }
        else
            skeleton_Animator.SetBool("Is_Idle", false);


        next_Time = Random.Range(2f, 5f);
        Invoke("Think_Move", next_Time);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player_Attack1" && is_atk == false)
        {
            skeleton_Animator.SetTrigger("Is_Dmg");
            hp -= 1;
            Hurt();
        }
        if (collision.tag == "Player_Attack2" && is_atk == false)
        {
            skeleton_Animator.SetTrigger("Is_Dmg");
            hp -= 1;
            Hurt();
        }
        if (collision.tag == "Player_Attack3" && is_atk == false)
        {
            skeleton_Animator.SetTrigger("Is_Dmg");
            hp -= 3;
            Hurt();
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
        /*Attack_LoadingEnd();
        is_atk = false;*/

        attack_Col.gameObject.SetActive(true);
        Invoke("Dis_Damage", 0.17f);
    }
    private void Dis_Damage()
    {
        attack_Col.gameObject.SetActive(false);

        speed = 75f;
        is_atk = false;
    }

    public void Hurt()
    {
        speed = 0f;
        Invoke("Dis_Hurt", 0.15f);
    }
    public void Dis_Hurt()
    {
        speed = 75f;
    }

}
