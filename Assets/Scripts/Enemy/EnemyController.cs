using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // 적 유닛
    [SerializeField] private GameObject enemyHPSlider; // 적 체력바
    [SerializeField] private Transform canvasTrans; // 캔버스 Transform
    [SerializeField] private Transform Parent; // 생성된 유닛 Clone 저장소
    [SerializeField] private float spawnTime; // 젠타임
    [SerializeField] private Transform[] wayPoints; // 현재 스테이지의 이동 경로
    [SerializeField] private PlayerHP playerHP; // PlayerHP
    [SerializeField] private PlayerGold playergold; // Player Gold
    List<Enemy> enemyList; // 맵에 있는 모든 적의 정보

    // 적의 생성과 삭제는 EnemyController에서 하기 때문에 Set은 필요없음
    public List<Enemy> EnemyList => enemyList;

    void Awake()
    {
        enemyList = new List<Enemy>();
        // 적 생성 코루틴 호출 => InkoveReating으로도 가능할듯
        StartCoroutine("SpawnEnemy");
    }
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject obj = Instantiate(enemyPrefab, Parent); // 적 생성
            Enemy enemy = obj.GetComponent<Enemy>(); // 생성된 적의 Component

            enemy.Setup(this,wayPoints); // wayPoints의 정보를 매개변수로 Setup() 호출
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(obj); // 적 체력을 나타내는 Slider 생성 및 설정
            yield return new WaitForSeconds(spawnTime); // 젠타임 동안 대기
        }
    }

    void SpawnEnemyHPSlider(GameObject obj)
    {
        // 적 체력바 생성
        GameObject sliderobj = Instantiate(enemyHPSlider);
        // Slider 오브젝트의 parent설정 => UI는 Canvas의 자식으로 있어야 화면에 노출됨
        sliderobj.transform.SetParent(canvasTrans);
        // 계층 설정으로 바뀐 크기 초기화
        sliderobj.transform.localScale = Vector3.one;

        //Slider가 쫓아다닐 대상을 자신으로 설정
        sliderobj.GetComponent<SliderPositionAutoSetter>().Setup(obj.transform);
        //자신의 체력 정보를 Slider에 표시
        sliderobj.GetComponent<EnemyHPView>().Setup(obj.GetComponent<EnemyHP>());
    }
    public void DestroyEnemy(EnemyDieType type, Enemy enemy, int gold)
    {
        //적이 목표지점까지 도착했을 때
        if(type == EnemyDieType.Arrive)
        {
            // 플레이어 체력 감소
            playerHP.Damaged(1);
        }
        else if(type == EnemyDieType.Dead)
        {
            // 적의 종류에 따라 사망 시 골드 획득
            playergold.CurGold += gold;
        }
        // 리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);
        // 적 오브젝트 삭제
        Destroy(enemy.gameObject);
    }
}
