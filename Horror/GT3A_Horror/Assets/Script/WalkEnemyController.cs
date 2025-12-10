using UnityEngine;
using UnityEngine.AI;
public class WalkEnemyController : MonoBehaviour
{
    [Header("巡回地点")]
    [SerializeField] Transform[] wayPoints;
    int currentIndex = 0;
    Transform player;
    NavMeshAgent agent;
    AudioSource arart;
    PlayerNoise noise;

    public enum State
    {
        Patrol,
        Chase,
    }
    [SerializeField] State currentState = State.Patrol;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        noise = player.GetComponent<PlayerNoise>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Patrol:
                PatrolUpdate();
                SearchPlayer();
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
    void SearchPlayer()
    {
        // 距離計算
        float distance = Vector3.Distance(player.position, transform.position);
        bool isNoise = noise.GetNoiseValue() > 0.0f;

        // 仮(プレイヤーが音を立てているかチェック)
        if (distance <= 10.0f && isNoise)
        {
            currentState = State.Chase;
            agent.SetDestination(player.position);
            arart.Play();
        }
    }
    void ChaseUpdate() 
    {
        agent.SetDestination(player.position);
    }
}
