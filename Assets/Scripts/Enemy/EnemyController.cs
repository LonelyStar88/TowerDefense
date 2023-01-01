using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveSystem;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private GameObject enemyPrefab; // �� ����
    [SerializeField] private GameObject enemyHPSlider; // �� ü�¹�
    [SerializeField] private Transform canvasTrans; // ĵ���� Transform
    [SerializeField] private Transform Parent; // ������ ���� Clone �����
    //[SerializeField] private float spawnTime; // ��Ÿ��
    [SerializeField] private Transform[] wayPoints; // ���� ���������� �̵� ���
    [SerializeField] private PlayerHP playerHP; // PlayerHP
    [SerializeField] private PlayerGold playergold; // Player Gold
    Wave currentWave; // ���� ���̺� ����
    int currentEnemycount; // ���� ���̺꿡 �����ִ� �� ���� (���̺� ���۽� �ִ�, �� ������� 1���� ����)
    List<Enemy> enemyList; // �ʿ� �ִ� ��� ���� ����

    // ���� ������ ������ EnemyController���� �ϱ� ������ Set�� �ʿ����
    public List<Enemy> EnemyList => enemyList;
    // ���� ���̺��� �����ִ� ��, �ִ� �� ����
    public int CurrentEnemyCount => currentEnemycount;
    public int MaxEnemyCount => currentWave.maxEnemyCnt;
    void Start()
    {
        enemyList = new List<Enemy>();
        // �� ���� �ڷ�ƾ ȣ�� => InkoveReating���ε� �����ҵ�
        //StartCoroutine("SpawnEnemy");
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        currentEnemycount = currentWave.maxEnemyCnt;
        // ���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }
    IEnumerator SpawnEnemy()
    {
        // ���� ���̺꿡�� ������ �� ����
        int spawnEnemycount = 0;
        while(spawnEnemycount < currentWave.maxEnemyCnt)
        {
            //GameObject obj = Instantiate(enemyPrefab, Parent); // �� ����
            // ���̺꿡 �����ϴ� ���� ������ ���� ������ �� ������ ���� �����ϵ��� ����, �� ������Ʈ ����
            int enemyIdx = Random.Range(0, currentWave.enemies.Length);
            GameObject obj = Instantiate(currentWave.enemies[enemyIdx]);
            Enemy enemy = obj.GetComponent<Enemy>(); // ������ ���� Component

            enemy.Setup(this,wayPoints); // wayPoints�� ������ �Ű������� Setup() ȣ��
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(obj); // �� ü���� ��Ÿ���� Slider ���� �� ����

            spawnEnemycount++;
            yield return new WaitForSeconds(currentWave.spawnTime); // ��Ÿ�� ���� ���
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

        // �� ����� ���� ���̺��� ���� �� ����(UIǥ�ÿ�)
        currentEnemycount--;
        // ����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        // �� ������Ʈ ����
        Destroy(enemy.gameObject);
    }
}
