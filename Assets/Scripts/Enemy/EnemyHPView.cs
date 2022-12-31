using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPView : MonoBehaviour
{
    EnemyHP enemyHP;
    Slider hpSilder;


    public void Setup(EnemyHP hp)
    {
        this.enemyHP = hp;
        hpSilder = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        hpSilder.value = enemyHP.CurHP / enemyHP.MaxHP;
    }
}
