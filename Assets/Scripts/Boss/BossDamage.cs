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

        transform.parent.DOShakePosition(0.2f, 0.24f);

        //LifeBar
        lifeBar.DOFillAmount(life/100f, 0.3f).SetEase(Ease.InQuad);
        lifeBar.DOBlendableColor(Color.red, 0.1f).OnComplete(() => lifeBar.DOBlendableColor(Color.white, 0.3f));

        if(life <= 0)
        { 
            //deathDirector.Play();
            transform.parent.GetComponent<BossBehaviour>().dead = true;
            deathDirector.Play();
            transform.parent.GetComponent<BossBehaviour>().Die();
            return "hit";
        }

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
            if(constantDamage)
                StartCoroutine(ResetDamage());
            ActionCharacter.Instance.ReceiveDamage(20);
        }
    }

    private void OnDisable()
    {
        justDidDamage = false;
    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(1);
        justDidDamage = false;
    }
}
