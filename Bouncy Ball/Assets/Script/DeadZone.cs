using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    //이 오브젝트에 충돌한 물체가 있다면,
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //그 충돌한 오브젝트를 삭제한다.
        Destroy(collision.gameObject);
    }
}
