using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossDamage : MonoBehaviour, IHitable
{
    public Transform ParticleLocation;
    public Transform HitFX;
    private MaterialPropertyBlock _propBlock;
    public Renderer[] renderers;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
    }
    
    public string OnHit(int damage, Vector3 dir)
    {
        DOVirtual.Float(0f, .4f, 0.088f, (float value) => {
            _propBlock.SetFloat("_EmInt", value);
            foreach (var renderer in renderers)
            {
                renderer.SetPropertyBlock(_propBlock);  
            }    
        }).SetLoops(2, LoopType.Yoyo);
        
        var fx = Instantiate(HitFX, ParticleLocation.position, Quaternion.identity);
        Destroy(fx.gameObject, 1f);

        Debug.Log("Damage");
        return "hit";
    }
}
