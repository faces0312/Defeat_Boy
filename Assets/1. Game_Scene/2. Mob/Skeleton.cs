using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Player player;

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
    // Start is called before the first frame update
    void Start()
    {
        hp = 5;

        player_Recog = false;

        is_atk = false;
        speed = 50f;
        attack_CT = 1f;


        Think_Move();
    }

    private void OnEnable()
    {
        attack_Col.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player_Recog == true)
        {
            if (player.transform.position.x > transform.position.x && is_atk == false)
            {
                gameObject.transform.localScale = new Vector3(1.8f, 1.8f);
            }
            else if (player.transform.position.x <= transform.position.x && is_atk == false)
            {
                gameObject.transform.localScale = new Vector3(-1.8f, 1.8f);
            }
        }
        else
        {
            if(nextMove == 1)
            {
                gameObject.transform.localScale = new Vector3(1.8f, 1.8f);
            }
            else if(nextMove == -1)
            {
                gameObject.transform.localScale = new Vector3(-1.8f, 1.8f);
            }
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
    {
        //기본 이동
        mob_Rigid.velocity = new Vector2(nextMove * Time.deltaTime * speed, mob_Rigid.velocity.y);

        //낭떠러지 체크
        Vector2 frontVec = new Vector2(mob_Rigid.position.x + nextMove * 0.2f, mob_Rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if(rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think_Move", 3f);
        }
    }
    //다음 이동
    void Think_Move()
    {
        nextMove = Random.Range(-1, 2);

        float next_Time = Random.Range(2f, 5f);
        Invoke("Think_Move", next_Time);
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
            speed = 50f;
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
        Attack_LoadingEnd();
        is_atk = false;

        attack_Col.gameObject.SetActive(true);
        Invoke("Dis_Damage", 0.1f);
    }
    private void Dis_Damage()
    {
        attack_Col.gameObject.SetActive(false);
    }

    public void Attack_End()
    {
        speed = 50f;
        is_atk = false;
    }
}
