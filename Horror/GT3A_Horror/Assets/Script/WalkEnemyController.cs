using UnityEngine;
using UnityEngine.AI;
public class WalkEnemyController : MonoBehaviour
{
    [Header("巡回地点")]
    [SerializeField] Transform[] wayPoints;
    int currentIndex = 0;
    Transform player;
    NavMeshAgent agent;

    public enum State
    {
        Patrol,
        Chase,
    }
    [SerializeField] State currentState = State.Patrol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Patrol:
                PatrolUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
        }
    }
    void PatrolUpdate() 
    {
        // エージェントが計算中でないとき、目標地点が近くなったら
        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            SetNextPoint();
        }
    }
    void SetNextPoint()
    {
        agent.SetDestination(wayPoints[currentIndex].position);
        currentIndex = (currentIndex + 1) % wayPoints.Length;
    }
    void ChaseUpdate() { }
}
