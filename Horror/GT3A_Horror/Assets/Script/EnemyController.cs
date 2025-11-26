using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float viewAngle = 30, viewDistance = 10, // 視野角と距離
        seenDeathTime = 2.0f;                // 見られてから死亡するまでの時間
    float seenTimer = 0;    // プレイヤーに見られている時間
    bool isDead = false;    // 死亡フラグ
    Transform player;
    PlayerVision vision;
    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        vision = player.GetComponent<PlayerVision>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent == null || player == null || isDead) return;

        // プレイヤーから見られているかチェック
        bool seen = vision.isLookingAt(transform, viewAngle, viewDistance);

        // 見られている時間を加算
        if (seen)
        {
            seenTimer += Time.deltaTime;
            // 一定時間見られたら死亡処理
            if (seenTimer >= seenDeathTime)
            {
                StartCoroutine(DeathRouthine());
                return;
            }
        }
        // 見られてないので減算(半分の速度)
        else
        {
            seenTimer = Mathf.Max(0, seenTimer - Time.deltaTime * 0.5f);
        }
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
    IEnumerator DeathRouthine()
    {
        isDead = true;
        agent.isStopped = true;
        float time = 0;
        // 一定時間で縮小して消える
        Vector3 startScale = transform.localScale;
        while(time < 0.5f)
        {
            time += Time.deltaTime;
            float rate = Mathf.Lerp(1, 0, time / 0.5f);
            transform.localScale = startScale * rate;
            yield return null;
        }
        Destroy(gameObject);
    }
}
