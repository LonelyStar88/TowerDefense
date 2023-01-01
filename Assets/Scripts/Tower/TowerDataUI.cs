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
        //Ŭ���ϸ� Ÿ������ ����, ESC ������ Ÿ������ ����
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerATK)
    {
        // ����ؾ��ϴ� Ÿ�� ����
        currentTower = towerATK.GetComponent<TowerAttack>();
        // Ÿ�� ����â Ȱ��ȭ
        gameObject.SetActive(true);
        // Ÿ�� ���� ����
        UpdataTowerData();
        // Ÿ�� ������Ʈ �ֺ��� ǥ�õǴ� Ÿ�� ���ݹ��� Sprite on
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
