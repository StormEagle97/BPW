using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMoveMash : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Zombie Zombie;
    Rigidbody m_Rigidbody;
    NavMeshAgent _navMeshAgent;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Zombie = GetComponent<Zombie>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        SetDestination();
    }

    private void SetDestination()
    {
        if (player != null)
        {
            if (Zombie.shouldMove && _navMeshAgent != null)
            {
                Vector3 targetVector = player.transform.position;
                _navMeshAgent.SetDestination(targetVector);
            }
            else
            {
                if(_navMeshAgent != null)
                    Destroy(_navMeshAgent);
                //m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                //m_Rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            }
        }
    }
}
