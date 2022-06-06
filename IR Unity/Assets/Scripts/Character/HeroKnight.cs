﻿using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource Attack1SoundEffect;
    [SerializeField] private AudioSource TaklaSoundEffect;
    [SerializeField] private AudioSource ShieldSoundEffect;

    [SerializeField] private Transform leftAttackPoint;
    [SerializeField] private Transform rightAttackPoint;
    private Vector2 attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackDamage;
    [SerializeField] private float playerHealth;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    public bool isPlayerDeath;
    public bool isBlock;


    private void Awake()
    {
        attackPoint = rightAttackPoint.position;
        playerHealth = 100f;
        isPlayerDeath = false;
        isBlock = false;
    }

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

    }

    void Attack()
    {
        // player atak yapınca düşman hasar alacak
        Debug.Log("Player Attack çalıştı");
        // alanı tara ve önünde düşman var mı bak
        Collider2D enemy = Physics2D.OverlapCircle(attackPoint, attackRadius, enemyLayer);

        // düşman scriptine ilet

        if (enemy != null)
        {
            enemy.gameObject.GetComponentInParent<BanditController>().DecreaseHealth(attackDamage);
        }

    }

    public void DecreasePlayerHealth(float banditAttackDamage)
    {
        // oyuncu sağlığından gelen değer kadar düşür
        // hurt animasyonu çalıştır
        if (!isBlock)
        {
            playerHealth = playerHealth - banditAttackDamage;
            m_animator.SetTrigger("Hurt");
        }

    }

    private void PlayerDeath()
    {
        // ölme animasyonu
        // hareket edemesin
        // ölme durumunda açılacak sahne

        isPlayerDeath = true;

        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetBool("Death", true);
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint, attackRadius);
    }

    private void SetAttackPoint(string direction)
    {
        // sağa sola dönünce attackPoint değiştir
        switch (direction)
        {
            case "left":
                attackPoint = leftAttackPoint.position;
                break;
            case "right":
                attackPoint = rightAttackPoint.position;
                break;
            default:
                attackPoint = rightAttackPoint.position;
                break;
        }

    }

    void Update()
    {

        m_timeSinceAttack += Time.deltaTime;

        if (playerHealth <= 0 && !isPlayerDeath)
        {
            // PlayerDeath();
        }

        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;


        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;


        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }


        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }


        float inputX = Input.GetAxis("Horizontal");


        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            SetAttackPoint("right");
        }

        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            SetAttackPoint("left");
        }


        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);


        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);


        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);


        if (Input.GetKeyDown("e") && !m_rolling)
        {
            // m_animator.SetBool("noBlood", m_noBlood);
            // m_animator.SetTrigger("Death");
        }


        // else if (Input.GetKeyDown("q") && !m_rolling)
        //    m_animator.SetTrigger("Hurt");



        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;


<<<<<<< HEAD
            Attack1SoundEffect.Play();
=======

>>>>>>> aadd7f021be4b549b0e46fa23b478975dadfa9dc
            if (m_currentAttack > 3)
                m_currentAttack = 1;


            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;


            m_animator.SetTrigger("Attack" + m_currentAttack);


            m_timeSinceAttack = 0.0f;

            Attack();
        }


        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
<<<<<<< HEAD
            ShieldSoundEffect.Play();
=======
            isBlock = true;
>>>>>>> aadd7f021be4b549b0e46fa23b478975dadfa9dc
        }

        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
<<<<<<< HEAD
        
=======
            isBlock = false;
        }
>>>>>>> aadd7f021be4b549b0e46fa23b478975dadfa9dc

        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            TaklaSoundEffect.Play();
        }



        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
            jumpSoundEffect.Play();
        }


        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {

            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }


        else
        {

            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }


    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {

            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;

            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}
