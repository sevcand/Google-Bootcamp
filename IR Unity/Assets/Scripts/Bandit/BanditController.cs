using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
//using UnityEditor.SearchService;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    #region Public Variables

    public float attack_distance;
    public float moving_speed;
    public float timer;
   // public Transform left_limit;
   // public Transform right_limit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool in_range;
    public GameObject Hotzone;
    public GameObject TriggerArea;
    [HideInInspector] public bool isDeath;
    public float health;


    #endregion

    #region Private Variables

    private Animator anim;
    private float distance;
    private bool attack_mode;
    private bool cooling;
    private float int_timer;
    [SerializeField] private GameObject[] _banditColliders;
   // [SerializeField] private Transform attackPoint;
   // [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;

    #endregion

    private void Awake()
    {
        // SelectTarget();
        target = gameObject.transform;
        int_timer = timer;
        anim = GetComponent<Animator>();
        health = 100f;
        isDeath = false;

    }

    void Update()
    {
        /*  SORUNLAR

        Atak durumunu ele alan bir koşul yok
         
         */

        if (!attack_mode && in_range && !isDeath)
        {
            Move();
            Debug.Log("Move çalıştı");
        }

        //attack animasyonunun trigger sağlamak için aşağıdaki satırı ve anim.set.boolu yazdım, işe yaramadı//
        //if (attack_mode && in_range)
        //{
        //    anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_LightBandit");
        //}

        if (in_range && Vector2.Distance(transform.position, target.position) > 1.5f)
        {
            Debug.Log("Yeni koşul !!!");
            StopAttack();
        }

        if (health <= 0)
        {
            DeathBandit();
        }

    /*
        if (!InsideofLimits() && !in_range && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_LightBandit"))
        {
            Debug.Log("select target çalıştı");
            //  SelectTarget();
        }

    */

        if (in_range == false)
        {
            Debug.Log("alan dışında");
            //burası da yorum olabilir, 3.videoda kaldırılıyor. ancak bu satırı yorumlayınca
            //trigger areadan karakter çıktığı halde bandit koşma animasyonunda kalıyor.
            //bu satır duruyorken karakter trigger areadan çıktığı anda bandit idle animasyona dönebiliyor
            anim.SetBool("CanWalk", false);
            //
            StopAttack();

        }

    }

    void DeathBandit()
    {
        // ölürken olacaklar
        // animasyon, hareket edememeli, atak yapamamalı

        Debug.Log("Bandit öldü!");
        isDeath = true;
        StopAttack();
        anim.SetBool("isDeath", true);

        foreach(GameObject obj in _banditColliders )
        {
            obj.SetActive(false);
        }
        gameObject.GetComponent<Rigidbody2D>().Sleep();

    }

    public void DecreaseHealth(float attackDamage)
    {
        health = health - attackDamage;
        // hurt animasyonunu tetikle
        if (!isDeath)
        {
            anim.SetTrigger("Hurt");
        }

    }


    void BanditAI()
    {
        // BURASI DÜŞMAN OYUNCUYA YAKLAŞMAYA BAŞLAYINCA ÇALIŞMASI GEREK
        // YANİ MOVE() 'DA

        Debug.Log("BanditAI çalıştı");
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attack_distance)
        {
            StopAttack();

        }

        else if (distance <= attack_distance && cooling == false)
        {
            Attack();
        }

        else if (distance <= attack_distance && cooling == true)
        {
            StopAttack();
            anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_LightBandit");
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
            BanditAI();

        }
    }

    void Attack()
    {
        Debug.Log("Attack fonksiyonu çalıştı");

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

    public void StopAttack()
    {
        Debug.Log("stop attack çalıştı");

        cooling = false;
        attack_mode = false;

        anim.SetBool("Attack", false);

    }


    public void TriggerCooling()
    {
        cooling = true;

    }

    /*
    private bool InsideofLimits()
    {
        return transform.position.x > left_limit.position.x && transform.position.x < right_limit.position.x;

    }

    */

    /* public void SelectTarget()
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

     }  */

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

/*
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    */
}
