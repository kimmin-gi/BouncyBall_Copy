using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    GameObject findStar = null;
    GameObject findBall = null;
    private static int level = 0;

    public void changeScene()
    {
        //Level의 값을 1올린다.
        level++;
        //다음 스테이지를 로드
    }

    public void reLoadScene()
    {
        //다음 스테이지를 로드
        Application.LoadLevel(level);
        //Level의 값을 1올린다.
    }

    void Update ()
    {
        //Tag가 "STAR"인 가장 가까운 물체를 찾아 FindStar에 넣는다.
        //없다면 그 다음 가까운 것을 찾아 넣는다.
        findStar = GameObject.FindWithTag("STAR");
        findBall = GameObject.FindWithTag("BALL");

        //만약 null이라면
        if(findStar == null) 
        {
            changeScene();
        }

        if (findBall == null)
        {
            reLoadScene();
        }
    }

    // public void firstScene()
    // {
    //     Application.LoadLevel("Stage01_01");
    // }
}
