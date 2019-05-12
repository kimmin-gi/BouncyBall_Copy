using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controll : MonoBehaviour
{
    public float speed = 4; //이동속도
    public float addForce = 300; //점프에 가하는 힘
    private bool jumpcheak = false; //계속 점프하는 것을 막기 위한 변수
    private bool moveBlockCheck = false; //MoveBlock에 충돌 했을때 true가 되며 공이 무중력이 되어 계속 이동

    private float dis;
    private int num;

    private bool enter;
    private bool exit;

    private Vector3 v_Dir;

     /////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌한 트리거 오브젝트를 매개변수로 넣는다.
        objectEat(collision.gameObject);
    }

    private void objectEat(GameObject item)
    {
        starEat(item);
    }
    
    private void starEat(GameObject star)
    {
        //충돌한 오브젝트의 태그가 "STAR"이라면,
        if (star.tag == "STAR")
        {
            //삭제한다 == 먹는다
            Destroy(star);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enter = true;
        exit = false;

        ballMoveBy_MoveBlock(collision);
    }

    private void ballMoveBy_MoveBlock(Collision2D collision)
    {
        if (collision.gameObject.tag == "MOVE BLOCK")
        {
            v_Dir = collision.gameObject.GetComponent<MoveBlock>().Dir;
            this.transform.position = collision.transform.position;
            moveBlockCheck = true;
        }

        if (moveBlockCheck == true && collision.gameObject.tag == "WALL")
        {
            moveBlockCheck = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        enter = false;
        exit = true;
    }
    
     /////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Update()
    {
        ballMove();

        exit = false;

        if (exit == true)
        {
            enter = false;
        }
    }

    private void ballMove()
    {
        float move = Time.deltaTime * speed;

        if (moveBlockCheck == false && Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * move);

        if (moveBlockCheck == false && Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * move);

        if (moveBlockCheck == true)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            float fMove = Time.deltaTime * speed * 1.5f;
            transform.Translate(v_Dir * fMove);
        }

        if (Input.GetKeyDown("space"))
        {
            moveBlockCheck = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        jumpcheak = false;
    }

     /////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void FixedUpdate()
    {
        Vector2 vPos = this.gameObject.transform.position;
        CircleCollider2D colCircle = GetComponent<CircleCollider2D>();

        int nLayer = 1 << LayerMask.NameToLayer("Block");
        Collider2D[] cColCheck = Physics2D.OverlapCircleAll(vPos + colCircle.offset, colCircle.radius, nLayer);

        Vector2 thisObject = this.gameObject.transform.position;

        if (cColCheck.Length > 0)
        {
            //작은 값의 인덱스를 찾아 작동시킨다.
            //for문으로 배열의 길이만큼 돌면서 Dis에 거리가 가장 작은 값을 비교해서 넣기

            dis = 9999999;

            for (int i = 0; i < cColCheck.Length; i++)
            {
                if (dis > Vector3.Distance(thisObject, cColCheck[i].transform.position))
                {
                    dis = Vector3.Distance(thisObject, cColCheck[i].transform.position);
                    num = i;
                }

                checkTag(cColCheck);
            }
        }
    }

    private void checkTag(Collider2D[] cColCheck)
    {
        if (cColCheck[num].gameObject.tag == "BLOCK")
            {
                if (enter == true && jumpcheak == false)
                {
                    this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * addForce);
                    jumpcheak = true;
                }
            }

            else if (cColCheck[num].gameObject.tag == "DISPOSABLE BLOCK")
            {
                if (enter == true && jumpcheak == false)
                {
                    this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * addForce);
                    jumpcheak = true;
                    Destroy(cColCheck[num].gameObject);
                }
            }

            else if (cColCheck[num].gameObject.tag == "JUMP BLOCK")
            {
                if (enter == true && jumpcheak == false)
                {
                    this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * addForce * 1.5f);
                    jumpcheak = true;
                }
            }
    }
}