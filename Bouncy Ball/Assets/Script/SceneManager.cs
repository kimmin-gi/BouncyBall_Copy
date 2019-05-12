using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    GameObject FindStar = null;
    GameObject FindBall = null;
    private static int Level = 0;

    public void ChangeScene()
    {
        //Level의 값을 1올린다.
        Level++;
        //다음 스테이지를 로드
        Application.LoadLevel(Level);
        
    }

    public void ReLoadScene()
    {
        //다음 스테이지를 로드
        Application.LoadLevel(Level);
        //Level의 값을 1올린다.
    }

    void Update ()
    {
        //Tag가 "STAR"인 가장 가까운 물체를 찾아 FindStar에 넣는다.
        //없다면 그 다음 가까운 것을 찾아 넣는다.
        FindStar = GameObject.FindWithTag("STAR");
        FindBall = GameObject.FindWithTag("BALL");

        //만약 null이라면
        if(FindStar == null) 
        {
            ChangeScene();
        }

        if (FindBall == null)
        {
            ReLoadScene();
        }


    }
}
