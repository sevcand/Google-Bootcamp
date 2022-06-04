using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotzoneCheck : MonoBehaviour
{
    private BanditController bandit_parent;
    private bool in_range;
    private Animator anim;

    private void Awake()
    {
        bandit_parent = GetComponentInParent<BanditController>();
        anim = GetComponentInParent<Animator>();
        
    }

    private void Update()
    {
        if (in_range && !bandit_parent.isDeath && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_LightBandit"))
        {
            bandit_parent.Flip();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            in_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        in_range = false;
        gameObject.SetActive(false);
        bandit_parent.TriggerArea.SetActive(true);
        bandit_parent.in_range = false;
       // bandit_parent.SelectTarget();
    }
}
