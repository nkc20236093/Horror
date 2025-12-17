using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerVision : MonoBehaviour
{
    [Header("PostPrcess設定")]
    [SerializeField] PostProcessVolume volume;
    LensDistortion lens;
    ChromaticAberration chroma;
    Vignette vignette;
    [Header("確認用パラメーター")]
    [SerializeField] float viewCounter = 0;
    [SerializeField] float maxCount = 5;
    [SerializeField] float decreaseRate = 0.5f;
    [SerializeField] float increaseRate = 1;
    [SerializeField] int seenNum = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume.profile.TryGetSettings(out lens);
        volume.profile.TryGetSettings(out chroma);
        volume.profile.TryGetSettings(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if (seenNum != 0) // 見ているので加算
        {
            viewCounter += increaseRate * Time.deltaTime;
        }
        else // 見てないので減算
        {
            viewCounter -= decreaseRate * Time.deltaTime;
        }
        viewCounter = Mathf.Clamp(viewCounter, 0, maxCount);
        ApplyEffect(); // ポストプロセス適応
        seenNum = 0;   // 見た目数をリセット
    }

    void ApplyEffect()
    {
        float rate = viewCounter / maxCount;
        chroma.intensity.value = Mathf.Lerp(0, 1.0f, rate);
        vignette.intensity.value = Mathf.Lerp(0.0f, 0.7f, rate);
        lens.intensity.value = Mathf.Lerp(0.0f, 100.0f, rate);
        lens.scale.value = Mathf.Lerp(1.0f, 1.2f, rate);
        // 発狂による効果音変化
        BGMManager.instance.SetMadnessLevel(rate);
    }

    public void AddSeenNum()
    {
        seenNum++;
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
