using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float attackDamage = 10;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip[] SwordSoundEffects;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kılıç oyuncuya çarptı");
            other.gameObject.GetComponent<HeroKnight>().DecreasePlayerHealth(attackDamage);
            // burada ses çal
            
            if (other.gameObject.GetComponent<HeroKnight>().isBlock)
            {
                _audio.PlayOneShot(SwordSoundEffects[1]);
            }
            else if(!other.gameObject.GetComponent<HeroKnight>().isBlock)
            {
                _audio.PlayOneShot(SwordSoundEffects[0]);
            }

            if (other.gameObject.GetComponent<HeroKnight>().isPlayerDeath)
            {
                StopAttack();
            }
        }
    }

    void StopAttack()
    {
        this.gameObject.GetComponentInParent<BanditController>().StopAttack();
        this.gameObject.GetComponentInParent<BanditController>().in_range = false;
    }
}