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
        // 메인 카메라 
        // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();와 동일
        maincamera = Camera.main;   
    }
    // Update is called once per frame
    void Update()
    {
        // 마우스 좌클릭
        if(Input.GetMouseButtonDown(0))
        {
            // 카메라 위치에서 화면의 마우스 위치를 관통하는 광선 생성
            // ray.origin : 광선의 시작위치(=카메라 위치)
            // ray.direction : 광선의 진행방향
            ray = maincamera.ScreenPointToRay(Input.mousePosition);

            // 2D 모니터를 통해 3D 월드의 오브젝트를 마우스로 선택하는 방법
            // 광선에 부딪히는 오브젝트를 검색해서 hit에 저장
            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                // 광선에 부딪힌 오브젝트의 Tag가 Tile이면 타워생성
                if(hit.transform.CompareTag("Tile"))
                {
                    towerController.TowerSpawn(hit.transform);
                }
                // 타워를 선택하면 해당 타워 정보를 출력하는 타워 정보창 On
                else if(hit.transform.CompareTag("Tower"))
                {
                    towerDataviewer.OnPanel(hit.transform);
                }
            }
        }
    }
}
