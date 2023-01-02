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
    float Cooldowntime = 0f; //Ŭ�� ���߰� �����޼����� ���ÿ� ȣ��Ǵ� ���� ���� ���� 3���� ��ٿ��� �ο�
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
        //Ŭ���ϸ� Ÿ������ ����, ESC ������ Ÿ������ ����
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
        imageTower.sprite = currentTower.TowerImg;
        DamageTxt.text = "Damage : " + currentTower.Damage;
        RateTxt.text = "Rate : " + currentTower.Rate;
        RangeTxt.text = "Range : " + currentTower.Range;
        LVTxt.text = "Level : " + currentTower.Level;

        // ���׷��̵尡 �Ұ����� ��� ��ư ��Ȱ��ȭ
        upgradeBtn.interactable = currentTower.Level < currentTower.MaxLevel ? true : false; // 3�׽����� ������ ���̸� upgradeBtn.interactable�� true��,
                                                                                             // �����̸� false�� ���� 
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
        // Ÿ�� ���׷��̵� �õ�(true - ����, false, ����)
        bool isSuccess = currentTower.Upgrade();

        if(isSuccess == true)
        {
            // Ÿ���� ���׷��̵� �Ǿ����Ƿ� ����
            UpdataTowerData();
            // Ÿ�� �ֺ��� ���̴� ���ݹ����� ����
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);        
        }
        else
        {
            if (Cooldowntime > 3f)
            {
                isTxtPrint = true;
                // ��� ����
                StartCoroutine("ErrorTextPrint");
            }
        }
    }
    public void OnClickTowerSell()
    {
        // Ÿ�� �Ǹ�
        currentTower.Sell();
        // ������ Ÿ���� ������� Panel, ���ݹ��� off
        OffPanel();
    }
}
