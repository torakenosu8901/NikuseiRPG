using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManeger : SingletonClass<EffectManeger>
{
    [SerializeField] Object effectDamagePrefab;
    [SerializeField] Object effectDeadPrefab;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    public void PlayerEffectAttack()
    {
        var damageEffect = (GameObject)Instantiate(effectDamagePrefab);
        //Debug.Log(damageEffect.gameObject.transform.position);
        damageEffect.gameObject.transform.position = new Vector3(enemy.transform.position.x - 150f
                                                                , enemy.transform.position.y
                                                                ,75f);
        damageEffect.transform.localScale = 150.0f * damageEffect.transform.localScale;
        
    }

    public void EnemyEffectAttack()
    {

        var damageEffect = (GameObject)Instantiate(effectDamagePrefab);
        //Debug.Log(damageEffect.gameObject.transform.position);
        damageEffect.gameObject.transform.position = new Vector3(player.transform.position.x
                                                                , player.transform.position.y
                                                                , 75f);
        damageEffect.transform.localScale = 150.0f * damageEffect.transform.localScale;

    }

    public void PlayerEffectDead()
    {
        var deadEffect = (GameObject)Instantiate(effectDeadPrefab, this.transform);
        deadEffect.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 75f);
        deadEffect.transform.localScale = 150.0f * deadEffect.transform.localScale;
    }

    public void EnemyEffectDead()
    {
        var deadEffect = (GameObject)Instantiate(effectDeadPrefab, this.transform);
        deadEffect.gameObject.transform.position = new Vector3(enemy.transform.position.x - 150f, enemy.transform.position.y, 75f);
        deadEffect.transform.localScale = 150.0f * deadEffect.transform.localScale;
    }
}
