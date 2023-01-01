using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TxtViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text playerHPTxt;
    [SerializeField] private TMP_Text playerGoldTxt;
    [SerializeField] private TMP_Text waveTxt;
    [SerializeField] private TMP_Text enemyCntTxt;

    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private WaveSystem waveSystem; // 웨이브 정보
    [SerializeField] private EnemyController enemyController; // 적 정보

    // Update is called once per frame
    void Update()
    {
        playerHPTxt.text = playerHP.CurHP + "/" + playerHP.MaxHP;
        playerGoldTxt.text = playerGold.CurGold.ToString();
        waveTxt.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
        enemyCntTxt.text = enemyController.CurrentEnemyCount + "/" + enemyController.MaxEnemyCount;
    }
}
