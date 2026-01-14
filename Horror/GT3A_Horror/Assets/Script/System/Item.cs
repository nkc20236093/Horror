using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] AudioClip hitSEClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // トリガー判定をチェック
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーのタグとチェック
        if (other.gameObject.tag == "Player")
        {
            // 接触したのでアイテム数加算
            GameManager.Instance.AddItemCounter(1);
            // 場所を指定してSE再生
            AudioSource.PlayClipAtPoint(hitSEClip, transform.position);
            // 消える
            Destroy(this.gameObject);
        }
    }
}
