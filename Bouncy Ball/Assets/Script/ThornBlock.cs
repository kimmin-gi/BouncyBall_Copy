using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornBlock : MonoBehaviour {

	private void ballSence(Collider2D other)
	{
		if (other.gameObject.tag == "BALL")
        {
            //그 물체를 삭제한다.
            Destroy(other.gameObject);
        }
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		ballSence(other);
	}
}
