using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;

public class BossDamage : MonoBehaviour, IHitable
{
    public LayerMask playerLayer;
    public static float life = 100;
    public Image lifeBar;
    public bool constantDamage;
    public bool justDidDamage;
    public PlayableDirector deathDirector;

    public string OnHit(int damage, Vector3 dir)
    {
        if(constantDamage) return "fail";

        life -= damage;

        if(life <= 0)
        { 
            //deathDirector.Play();
            transform.parent.GetComponent<BossBehaviour>().dead = true;
            StartCoroutine(Die());
            return "die";
        }

        transform.parent.DOShakePosition(0.2f, 0.24f);

        //LifeBar
        lifeBar.DOFillAmount(life/100f, 0.3f).SetEase(Ease.InQuad);
        lifeBar.DOBlendableColor(Color.red, 0.1f).OnComplete(() => lifeBar.DOBlendableColor(Color.white, 0.3f));

        return "hit";
    }

    private void Update() {
        if(constantDamage)
            DoDamage();
    }

    public void DoDamage() {
        if(justDidDamage) return;
        if(Physics.CheckBox(transform.position, (transform.localScale/2)*102,transform.rotation, playerLayer))
        {
            justDidDamage = true;
            StartCoroutine(ResetDamage());
            ActionCharacter.Instance.ReceiveDamage(20);
        }
    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(1);
        justDidDamage = false;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1);
        transform.parent.GetComponent<Animator>().SetTrigger("die");
        yield return new WaitForSeconds(10);
        //Travel
    }
}
