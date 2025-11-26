using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// プレイヤーがターゲットを見たかチェック
    /// </summary>
    /// <param name="target">ターゲットのトランスフォーム</param>
    /// <param name="viewAngle">視野角制限</param>
    /// <param name="viewDistance">距離制限</param>
    /// <returns></returns>
    public bool isLookingAt(Transform target, float viewAngle, float viewDistance)
    {
        // プレイヤーからターゲットの位置を計算
        Vector3 dir = (target.position - transform.position).normalized;
        // 視野角を計算
        float angle = Vector3.Angle(transform.forward, dir);
        // チェックする角度より大きければ見ていない
        if (angle > viewAngle) { return false; }
        // ターゲットまでの距離を計算
        float distance = Vector3.Distance(transform.position, target.position);
        // 距離が遠ければ見ていない
        if(distance > viewDistance) { return false; }
        // 壁判定でrayを飛ばす
        Vector3 start = transform.position;
        // 高さはターゲットに合わせる
        start.y = target.position.y;
        Vector3 direction = (target.position - start).normalized;
        Debug.DrawRay(start, direction * distance, Color.red, 0.1f);
        if (Physics.Raycast(start, direction, out RaycastHit hit, distance))
        {
            // 接触してるので当たったものをチェック
            if (hit.transform == target)
            {
                // 敵と接触しているので見ている
                return true;
            }
            else
            {
                return false;
            }
        }
        // 見た
        Debug.Log("PLが敵を見た");
        return true;
    }
}
