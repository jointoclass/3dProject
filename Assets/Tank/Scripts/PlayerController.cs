using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if TANK
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent; //NavMeshAgent 길찾기 라이브러리    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
        
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0은 좌클릭, 1은 우클릭, 2는 스크롤버튼
        {
            MoveToClickPoint();
        }
    }

    void MoveToClickPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
#endif