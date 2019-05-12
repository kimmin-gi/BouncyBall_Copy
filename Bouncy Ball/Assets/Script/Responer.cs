using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Responer : MonoBehaviour {

    public GameObject PrefapObject;//해당 오브젝트의 프리팹
    public GameObject ObjObject; //해당 오브젝트
    public GameObject DummyObject;//해당 오브젝트가 재성성되는 장소
    public bool Auto; //재성성을 할지 말지 정한다.
    public float ResponTime; //재성되기까지 걸리는 시간

    //코루틴 함수
    IEnumerator ResponTimer(float time)
    {
        while (true)
        {
            //time만큼 기다렸다가
            yield return new WaitForSeconds(time);
            //if문이 실행된다.
            if (Auto) ResponObject();
        }
    }

    void ResponObject()
    {
        //만약 ObjObject가 null이라면(해당 오브젝트가 삭제되었다면)
        if (ObjObject == null)
        {
            //PrefapObject에 있는 물체를 ObjObject에 넣는다.
            ObjObject = Instantiate(PrefapObject);
            //ObjObject의 재성성 위치는 DummyObject의 위치로 한다.
            ObjObject.transform.position = DummyObject.transform.position;
        }
    }

    void Start()
    {
        //코루틴 : 타이머나 일정시간마다 반복하도록 만들때 사용함
        //ResponTimer함수에 매개변수로 ResponTime을 넣어서 실행
        StartCoroutine(ResponTimer(ResponTime));
    }
}
