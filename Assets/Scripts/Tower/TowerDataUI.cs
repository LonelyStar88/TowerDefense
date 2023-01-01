using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataUI : MonoBehaviour
{
    [SerializeField] private Image imageTower;
    [SerializeField] private TMP_Text DamageTxt;
    [SerializeField] private TMP_Text RateTxt;
    [SerializeField] private TMP_Text RangeTxt;
    [SerializeField] private TMP_Text LVTxt;
    [SerializeField] private TowerAtkRange towerAttackRange;

    TowerAttack currentTower;
    // Start is called before the first frame update
    void Start()
    {
        OffPanel();
    }

    // Update is called once per frame
    void Update()
    {
        //클릭하면 타워정보 열림, ESC 누르면 타워정보 닫힘
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerATK)
    {
        // 출력해야하는 타워 정보
        currentTower = towerATK.GetComponent<TowerAttack>();
        // 타워 정보창 활성화
        gameObject.SetActive(true);
        // 타워 정보 갱신
        UpdataTowerData();
        // 타워 오브젝트 주변에 표시되는 타워 공격범위 Sprite on
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
    }
    public void OffPanel()
    {
        gameObject.SetActive(false);
        towerAttackRange.OffAttackRange();
    }
    private void UpdataTowerData()
    {
        DamageTxt.text = "Damage : " + currentTower.Damage;
        RateTxt.text = "Rate : " + currentTower.Rate;
        RangeTxt.text = "Range : " + currentTower.Range;
        LVTxt.text = "Level : " + currentTower.Level;
    }
}
