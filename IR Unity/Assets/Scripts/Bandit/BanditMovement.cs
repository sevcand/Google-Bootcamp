using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BanditMovement : MonoBehaviour
{
    #region Public Variables
    
    public Transform leeway;
    public LayerMask leeway_mask;
    public float leeway_length;
    public float attack_distance;
    public float moving_speed;
    public float timer;
    
    #endregion

    #region Private Variables
    
    private Animator anim;
    private GameObject target;
    private RaycastHit2D hit;
    private float distance;
    private bool attack_mode;
    private bool in_range;
    private bool cooling;
    private float int_timer;
    
    #endregion

    private void Awake()
    {
        int_timer = timer;
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (in_range)
        {
            hit = Physics2D.Raycast(leeway.position, Vector2.left, leeway_length, leeway_mask);
            RayCastDebugger();
            
        }

    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            in_range = true;
            
        }
        
    }

    void RayCastDebugger()
    {
        if (distance > attack_distance) ;
        Debug.DrawRay(leeway.position, Vector2.left * leeway_length, Color.red);
        
    }
    
}
