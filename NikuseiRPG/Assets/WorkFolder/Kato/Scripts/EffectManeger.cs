using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManeger : SingletonClass<EffectManeger>
{
    [SerializeField] Object effectDamagePrefab;
    [SerializeField] Object effectDeadPrefab;

    public void EffectAttack()
    {
        var damageEffect = (GameObject)Instantiate(effectDamagePrefab, this.transform);
        damageEffect.transform.localScale = 10.0f * damageEffect.transform.localScale;
        
    }

    public void EffectDead()
    {
        var deadEffect = (GameObject)Instantiate(effectDeadPrefab, this.transform);
        deadEffect.transform.localScale = 10.0f * deadEffect.transform.localScale;
    }
}
