using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDetector : MonoBehaviour
{
    [SerializeField] private TowerController towerController;
    [SerializeField] private TowerDataUI towerDataviewer;

    private Camera maincamera;
    private Ray ray;
    private RaycastHit hit;

    void Awake()
    {
        // ���� ī�޶� 
        // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();�� ����
        maincamera = Camera.main;   
    }
    // Update is called once per frame
    void Update()
    {
        // ���콺 ��Ŭ��
        if(Input.GetMouseButtonDown(0))
        {
            // ī�޶� ��ġ���� ȭ���� ���콺 ��ġ�� �����ϴ� ���� ����
            // ray.origin : ������ ������ġ(=ī�޶� ��ġ)
            // ray.direction : ������ �������
            ray = maincamera.ScreenPointToRay(Input.mousePosition);

            // 2D ����͸� ���� 3D ������ ������Ʈ�� ���콺�� �����ϴ� ���
            // ������ �ε����� ������Ʈ�� �˻��ؼ� hit�� ����
            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                // ������ �ε��� ������Ʈ�� Tag�� Tile�̸� Ÿ������
                if(hit.transform.CompareTag("Tile"))
                {
                    towerController.TowerSpawn(hit.transform);
                }
                // Ÿ���� �����ϸ� �ش� Ÿ�� ������ ����ϴ� Ÿ�� ����â On
                else if(hit.transform.CompareTag("Tower"))
                {
                    towerDataviewer.OnPanel(hit.transform);
                }
            }
        }
    }
}
