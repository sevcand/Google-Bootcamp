using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BanditMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    private Rigidbody2D myRigidbody;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    
    void Update()
    {
        if (IsFacingLeft())
        {
            myRigidbody.velocity = new Vector2(movementSpeed, 0f);
            
        }
        else
        {
            myRigidbody.velocity = new Vector2(-movementSpeed, 0f);
        }
    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x < Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
    }
}
