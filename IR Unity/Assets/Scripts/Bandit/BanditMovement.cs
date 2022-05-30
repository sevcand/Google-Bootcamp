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

        if (hit.collider != null)
        {
            BanditAI();
            
        }
        
        else if (hit.collider == null)
        {
            in_range = false;
        }

        if (in_range == false)
        {
            anim.SetBool("CanWalk", false);
            StopAttack();
            
        }
        
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            in_range = true;
            
        }
        
    }

    void BanditAI()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attack_distance)
        {
            Move();
            StopAttack();
            
        }
        
        else if (distance <= attack_distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            anim.SetBool("Attack", false);
            CoolDown();
            
        }
    }

    void Move()
    {
        anim.SetBool("CanWalk", true);
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_LightBandit"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moving_speed * Time.deltaTime);
            
        }
    }
    
    void Attack()
    {
        timer = int_timer;
        attack_mode = true;
        
        anim.SetBool("CanWalk", false);
        anim.SetBool("Attack", true);
        
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attack_mode)
        {
            cooling = false;
            timer = int_timer;
            
        }
    }

    void StopAttack()
    {
        cooling = false;
        attack_mode = false;
        
        anim.SetBool("Attack", false);
        
    }

    void RayCastDebugger()
    {
        if (distance > attack_distance)
        {
            Debug.DrawRay(leeway.position, Vector2.left * leeway_length, Color.blue);

        }
        else if (distance < attack_distance)
        {
            Debug.DrawRay(leeway.position, Vector2.left * leeway_length, Color.yellow);

        }
    }

    public void TriggerCooling()
    {
        cooling = true;
        
    }
}
