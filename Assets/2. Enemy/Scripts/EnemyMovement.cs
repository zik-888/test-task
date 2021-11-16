using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovement : MonoBehaviour
    {
        public float distanceThreshold = 0.5f;
        
        private NavMeshAgent _navMeshAgent;
        private Transform _heroTransform;
        [SerializeField]
        public bool isFinding = true;

        private EnemyLeaveMarker _enemyLeaveMarker;
        private ILevelEventHandler _levelEventHandler;
        [SerializeField] private float maxWaitForSeconds = 5;
        [SerializeField] private float minWaitForSeconds = 15;
        [SerializeField] private float randomWait;

        [Inject]
        private void Construct(Detection heroDetection, ILevelEventHandler levelEventHandler, EnemyLeaveMarker[] leaveMarkers)
        {
            _heroTransform = heroDetection.transform;
            _levelEventHandler = levelEventHandler;
            
            
            if(leaveMarkers.Length > 0)
                _enemyLeaveMarker = leaveMarkers[Random.Range(0, leaveMarkers.Length - 1)];
        }

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            randomWait = Random.Range(minWaitForSeconds, maxWaitForSeconds);
            StartFinding();
        }

        public void OffendedLeave()
        {
            if (_enemyLeaveMarker != null) 
                _navMeshAgent.SetDestination(_enemyLeaveMarker.LeavePosition);
        }
        public void StartFinding() => StartCoroutine(StartFindingCoroutine());

        private IEnumerator StartFindingCoroutine()
        {
            yield return new WaitForSeconds(randomWait);

            while (isFinding)
            {
                if (Vector3.Distance(transform.position, _heroTransform.position) > distanceThreshold)
                {
                    _navMeshAgent.SetDestination(_heroTransform.position);
                }
                else
                {
                    isFinding = false;
                    _levelEventHandler.MissionEventInvoke(MissionPart.EnemyReachedHero, gameObject);
                }
                
                yield return new WaitForFixedUpdate();
            }

        }
    }
}