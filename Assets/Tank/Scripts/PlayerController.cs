using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if TANK
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent; //NavMeshAgent ��ã�� ���̺귯��    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
        
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0�� ��Ŭ��, 1�� ��Ŭ��, 2�� ��ũ�ѹ�ư
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