using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BanditMovement : MonoBehaviour
{
    public Transform leeway;
    public LayerMask leeway_mask;
    public float leeway_length;
    public float attack_distance;
    public float moving_speed;
    public float timer;

    private Animator anim;
    private GameObject target;
    private RaycastHit2D hit;
    private float distance;
    private bool attack_mode;
    private bool in_range;
    private bool cooling;
    private float int_timer;
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
