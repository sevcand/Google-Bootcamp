using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;

public class BanditMovement : MonoBehaviour
{
    #region Public Variables
    
    public float attack_distance;
    public float moving_speed;
    public float timer;
    public Transform left_limit;
    public Transform right_limit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool in_range;
    public GameObject Hotzone;
    public GameObject TriggerArea;
    
    
    #endregion

    #region Private Variables
    
    private Animator anim;
    private float distance;
    private bool attack_mode;
    private bool cooling;
    private float int_timer;
    
    #endregion

    private void Awake()
    {
        SelectTarget();
        int_timer = timer;
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        
        if (!attack_mode && in_range)
        {
            Move(); 
        }

        //attack animasyonunun trigger sağlamak için aşağıdaki satırı ve anim.set.boolu yazdım, işe yaramadı//
        //if (attack_mode && in_range)
        //{
        //    anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_LightBandit");
        //}

        if (!InsideofLimits() && !in_range && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_LightBandit"))
        {
            SelectTarget();
        }

        if (in_range == false)
        {
            //burası da yorum olabilir, 3.videoda kaldırılıyor. ancak bu satırı yorumlayınca
            //trigger areadan karakter çıktığı halde bandit koşma animasyonunda kalıyor.
            //bu satır duruyorken karakter trigger areadan çıktığı anda bandit idle animasyona dönebiliyor
            anim.SetBool("CanWalk", false);
            //
            StopAttack();
            
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
        //alttaki satır normalde 3.videoda kaldırılıyor ancak kaldırılınca dönüş hareketini bozuyor tamamen
        anim.SetBool("CanWalk", true);
        //üstteki bu satırda CanWalk yerine Attack parametresini yazınca bu kez hareket etmeden saldırıyor sadece
        
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
    

    public void TriggerCooling()
    {
        cooling = true;
        
    }

    private bool InsideofLimits()
    {
        return transform.position.x > left_limit.position.x && transform.position.x < right_limit.position.x;
        
    }

    public void SelectTarget()
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

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 0;
            
        }
        else 
        {
            rotation.y = 180;
            
        }

        transform.eulerAngles = rotation;
    }   
}
