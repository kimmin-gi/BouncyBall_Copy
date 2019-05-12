using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftBlock : MonoBehaviour
{
    Vector3 updatePosition;

    public int speed = 1;

    void Start()
    {
        updatePosition = transform.position;
    }

    void Update()
    {
        updatePosition.x += speed * Time.deltaTime;
        transform.position = updatePosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "LIFT BLOCK LASTPART")
        {
            speed *= -1;
        }
    }
}
