using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waves; // 현재 스테이지의 모든 웨이브 정보
    [SerializeField] private EnemyController enemyController;
    int currentWaveIdx = -1;

    //현재 웨이브, 총 웨이브 정보를 알기 위한 프로퍼티
    public int CurrentWave => currentWaveIdx + 1;
    public int MaxWave => waves.Length;


    public void StartWave()
    {
        // 현재 맵에 적이 없고, Wave가 남아있을 경우
        if(enemyController.EnemyList.Count == 0 && currentWaveIdx < waves.Length - 1)
        {
            currentWaveIdx++;
            // enemyController의 StartWave()함수 호출, 현재 웨이브 정보 제공
            enemyController.StartWave(waves[currentWaveIdx]);
        }
    }
    // System.Serializable 은 구조체 또는 클래스를 직렬화 하는 명령(Inspector에서 직접 수정이 가능)
    [System.Serializable]
    public struct Wave
    {
        public float spawnTime; // 현재 웨이브 적 생성 주기
        public int maxEnemyCnt; // 현재 웨이브 적 등장 숫자
        public GameObject[] enemies; // 현재 웨이브 적 종류
    }
}
