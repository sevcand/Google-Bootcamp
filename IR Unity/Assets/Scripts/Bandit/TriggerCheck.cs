using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    private BanditMovement bandit_parent;

    private void Awake()
    {
        {
            bandit_parent = GetComponentInParent<BanditMovement>();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            bandit_parent.target = collider.transform;
            bandit_parent.in_range = true;
            bandit_parent.Hotzone.SetActive(true);
            
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
