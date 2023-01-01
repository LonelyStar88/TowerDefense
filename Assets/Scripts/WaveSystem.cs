using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] waves; // ���� ���������� ��� ���̺� ����
    [SerializeField] private EnemyController enemyController;
    int currentWaveIdx = -1;

    //���� ���̺�, �� ���̺� ������ �˱� ���� ������Ƽ
    public int CurrentWave => currentWaveIdx + 1;
    public int MaxWave => waves.Length;


    public void StartWave()
    {
        // ���� �ʿ� ���� ����, Wave�� �������� ���
        if(enemyController.EnemyList.Count == 0 && currentWaveIdx < waves.Length - 1)
        {
            currentWaveIdx++;
            // enemyController�� StartWave()�Լ� ȣ��, ���� ���̺� ���� ����
            enemyController.StartWave(waves[currentWaveIdx]);
        }
    }
    // System.Serializable �� ����ü �Ǵ� Ŭ������ ����ȭ �ϴ� ���(Inspector���� ���� ������ ����)
    [System.Serializable]
    public struct Wave
    {
        public float spawnTime; // ���� ���̺� �� ���� �ֱ�
        public int maxEnemyCnt; // ���� ���̺� �� ���� ����
        public GameObject[] enemies; // ���� ���̺� �� ����
    }
}
