using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    public float birdSpeed;

    public float beklemeSuresi;
    float beklemeSayac;

    int kacinciPozisyon;

    Animator anim;

    Vector2 kusYonu;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }
    }

    private void Start()
    {
        kacinciPozisyon = 0;

        transform.position = pozisyonlar[kacinciPozisyon].position;
    }

    private void Update()
    {
        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("ucsunMu", false);
        }
        else
        {

            kusYonu = new Vector2(pozisyonlar[kacinciPozisyon].position.x - transform.position.x, pozisyonlar[kacinciPozisyon].position.y - transform.position.y);


            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x)*Mathf.Rad2Deg;

            if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }else
            {
                transform.localScale = Vector3.one;
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, birdSpeed * Time.deltaTime);

            anim.SetBool("ucsunMu", true);

            if(Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
            {
                beklemeSayac = beklemeSuresi;

                PozisyonuDegistir();
                beklemeSayac = beklemeSuresi;
            }
        }
    }

    void PozisyonuDegistir()
    {
        kacinciPozisyon++;
        if (kacinciPozisyon >= pozisyonlar.Length)
        {
            kacinciPozisyon = 0;
        }
    }
}
