using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
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
    public Transform left_limit;
    public Transform right_limit;
    
    
    #endregion

    #region Private Variables
    
    private Animator anim;
    private Transform target;
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
        SelectTarget();
    }

    void Update()
    {
        if (!attack_mode)
        {
            Move();
        }

        if (!InsideofLimits() && !in_range && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_LightBandit"))
        {
            SelectTarget();
        }
        
        if (in_range)
        {
            hit = Physics2D.Raycast(leeway.position, transform.right, leeway_length, leeway_mask);
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
            StopAttack();
            
        }
        
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            in_range = true;
            Flip();
        }
        
    }

    void BanditAI()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attack_distance)
        {
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
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
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
            Debug.DrawRay(leeway.position, transform.right * leeway_length, Color.blue);

        }
        else if (distance < attack_distance)
        {
            Debug.DrawRay(leeway.position, transform.right * leeway_length, Color.yellow);

        }
    }

    public void TriggerCooling()
    {
        cooling = true;
        
    }

    private bool InsideofLimits()
    {
        return transform.position.x > left_limit.position.x && transform.position.x < right_limit.position.x;
        
    }

    private void SelectTarget()
    {
        float distance_to_left = Vector2.Distance(transform.position, left_limit.position);
        float distance_to_right = Vector2.Distance(transform.position, right_limit.position);

        if (distance_to_left > distance_to_right)
        {
            target = left_limit;
            
        }
        
        else if (distance_to_left < distance_to_right)
        {
            target = right_limit;
            
        }

        Flip();
        
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
            
        }

        else 
        {
            rotation.y = 0;
        }
    }
}
