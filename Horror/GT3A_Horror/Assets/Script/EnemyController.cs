using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float viewAngle = 30, viewDistance = 7;
    Transform player;
    PlayerVision vision;
    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        vision = GetComponent<PlayerVision>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent == null) return;

        // プレイヤーから見られているかチェック
        bool seen = vision.isLockingAt(transform, viewAngle, viewDistance);
        if (seen) // 見られている
        {
            agent.isStopped = true;
        }
        else　　　// 見られていない
        {
            agent.isStopped = false;
        }
        agent.SetDestination(player.transform.position);
    }
}
