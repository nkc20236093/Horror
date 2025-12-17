using UnityEngine;

public class NotEye_EnemyController : MonoBehaviour
{
    Transform player;
    PlayerVision vision;
    [SerializeField] 
    float viewAngle = 30, viewDistance = 10; // 視野角と距離

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").transform;
        vision = player.GetComponent<PlayerVision>();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーから見られているかチェック
        bool seen = vision.isLookingAt(transform, viewAngle, viewDistance);
        // プレイヤーから見られたらプレイヤー側に通知
        if (seen)
        {
            // ここでプレイヤーに通知
            vision.AddSeenNum();
        }
    }
}
