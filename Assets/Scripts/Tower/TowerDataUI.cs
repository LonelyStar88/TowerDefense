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
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private TMP_Text ErrorTxt;

    TowerAttack currentTower;
    bool isTxtPrint;
    float Cooldowntime = 0f; //클릭 남발과 에러메세지가 동시에 호출되는 것을 막기 위해 3초의 쿨다운을 부여
    // Start is called before the first frame update
    void Start()
    {
        OffPanel();
        isTxtPrint = false;
        ErrorTxt.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Cooldowntime += Time.deltaTime;
        //클릭하면 타워정보 열림, ESC 누르면 타워정보 닫힘
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }

        if(isTxtPrint)
        {
            Cooldowntime = 0f;
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
        imageTower.sprite = currentTower.TowerImg;
        DamageTxt.text = "Damage : " + currentTower.Damage;
        RateTxt.text = "Rate : " + currentTower.Rate;
        RangeTxt.text = "Range : " + currentTower.Range;
        LVTxt.text = "Level : " + currentTower.Level;

        // 업그레이드가 불가능한 경우 버튼 비활성화
        upgradeBtn.interactable = currentTower.Level < currentTower.MaxLevel ? true : false; // 3항식으로 조건이 참이면 upgradeBtn.interactable에 true를,
                                                                                             // 거짓이면 false를 저장 
    }

    IEnumerator ErrorTextPrint()
    {
        if (isTxtPrint)
        {
            ErrorTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            ErrorTxt.gameObject.SetActive(false);
            isTxtPrint = false;
        }
    }
    public void OnClickTowerUpgrade()
    {
        // 타워 업그레이드 시도(true - 성공, false, 실패)
        bool isSuccess = currentTower.Upgrade();

        if(isSuccess == true)
        {
            // 타워가 업그레이드 되었으므로 갱신
            UpdataTowerData();
            // 타워 주변에 보이는 공격범위도 갱신
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);        
        }
        else
        {
            if (Cooldowntime > 3f)
            {
                isTxtPrint = true;
                // 비용 부족
                StartCoroutine("ErrorTextPrint");
            }
        }
    }
    public void OnClickTowerSell()
    {
        // 타워 판매
        currentTower.Sell();
        // 선택한 타워가 사라져서 Panel, 공격범위 off
        OffPanel();
    }
}
