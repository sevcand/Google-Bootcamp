using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float attackDamage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kılıç oyuncuya çarptı");
            other.gameObject.GetComponent<HeroKnight>().DecreasePlayerHealth(attackDamage);
            if (other.gameObject.GetComponent<HeroKnight>().isPlayerDeath)
            {
                StopAttack();
            }
        }
    }

    void StopAttack()
    {
        this.gameObject.GetComponentInParent<BanditController>().StopAttack();
    }
}
