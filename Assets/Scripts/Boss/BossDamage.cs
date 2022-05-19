using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour, IHitable
{
    public Transform ParticleLocation;
    public Transform HitFX;
    private MaterialPropertyBlock _propBlock;
    public Renderer[] renderers;
    public LayerMask playerLayer;
    public float life = 100;
    public Image lifeBar;
    public bool constantDamage;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
    }
    
    public string OnHit(int damage, Vector3 dir)
    {
        life -= damage;

        DOVirtual.Float(0f, .4f, 0.088f, (float value) => {
            _propBlock.SetFloat("_EmInt", value);
            foreach (var renderer in renderers)
            {
                renderer.SetPropertyBlock(_propBlock);  
            }    
        }).SetLoops(2, LoopType.Yoyo);
        
        var fx = Instantiate(HitFX, ParticleLocation.position, Quaternion.identity);
        Destroy(fx.gameObject, 1f);

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
        if(Physics.CheckBox(transform.position, (transform.localScale/2)*102,transform.rotation, playerLayer))
        {
            constantDamage = false;
            ActionCharacter.Instance.ReceiveDamage(20);
        }
    }
}
