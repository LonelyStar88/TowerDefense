using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // �� ����
    [SerializeField] private GameObject enemyHPSlider; // �� ü�¹�
    [SerializeField] private Transform canvasTrans; // ĵ���� Transform
    [SerializeField] private Transform Parent; // ������ ���� Clone �����
    [SerializeField] private float spawnTime; // ��Ÿ��
    [SerializeField] private Transform[] wayPoints; // ���� ���������� �̵� ���
    [SerializeField] private PlayerHP playerHP; // PlayerHP
    [SerializeField] private PlayerGold playergold; // Player Gold
    List<Enemy> enemyList; // �ʿ� �ִ� ��� ���� ����

    // ���� ������ ������ EnemyController���� �ϱ� ������ Set�� �ʿ����
    public List<Enemy> EnemyList => enemyList;

    void Awake()
    {
        enemyList = new List<Enemy>();
        // �� ���� �ڷ�ƾ ȣ�� => InkoveReating���ε� �����ҵ�
        StartCoroutine("SpawnEnemy");
    }
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject obj = Instantiate(enemyPrefab, Parent); // �� ����
            Enemy enemy = obj.GetComponent<Enemy>(); // ������ ���� Component

            enemy.Setup(this,wayPoints); // wayPoints�� ������ �Ű������� Setup() ȣ��
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(obj); // �� ü���� ��Ÿ���� Slider ���� �� ����
            yield return new WaitForSeconds(spawnTime); // ��Ÿ�� ���� ���
        }
    }

    void SpawnEnemyHPSlider(GameObject obj)
    {
        // �� ü�¹� ����
        GameObject sliderobj = Instantiate(enemyHPSlider);
        // Slider ������Ʈ�� parent���� => UI�� Canvas�� �ڽ����� �־�� ȭ�鿡 �����
        sliderobj.transform.SetParent(canvasTrans);
        // ���� �������� �ٲ� ũ�� �ʱ�ȭ
        sliderobj.transform.localScale = Vector3.one;

        //Slider�� �Ѿƴٴ� ����� �ڽ����� ����
        sliderobj.GetComponent<SliderPositionAutoSetter>().Setup(obj.transform);
        //�ڽ��� ü�� ������ Slider�� ǥ��
        sliderobj.GetComponent<EnemyHPView>().Setup(obj.GetComponent<EnemyHP>());
    }
    public void DestroyEnemy(EnemyDieType type, Enemy enemy, int gold)
    {
        //���� ��ǥ�������� �������� ��
        if(type == EnemyDieType.Arrive)
        {
            // �÷��̾� ü�� ����
            playerHP.Damaged(1);
        }
        else if(type == EnemyDieType.Dead)
        {
            // ���� ������ ���� ��� �� ��� ȹ��
            playergold.CurGold += gold;
        }
        // ����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        // �� ������Ʈ ����
        Destroy(enemy.gameObject);
    }
}
