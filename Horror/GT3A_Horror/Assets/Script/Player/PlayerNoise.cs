using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float noiseValue = 0.0f;
    [SerializeField] float moveNoise = 0.0f;
    [SerializeField] float actionNoise = 0.0f;
    public float baseNoiseSpeed = 2.0f; // ノイズが鳴る速度
    public float MaxNoiseValue = 5.0f; // ノイズの最大値
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveNoise();
    }
    // 移動によるノイズ更新
    void UpdateMoveNoise()
    {
        float speed = rb.linearVelocity.magnitude;
        if (speed < baseNoiseSpeed) 
        {
            moveNoise = 0.0f;
        }
        else
        {
            float normal = speed - baseNoiseSpeed;
            moveNoise = Mathf.Clamp(normal, 0, MaxNoiseValue);
        }
    }
    public float GetNoiseValue()
    {
        return moveNoise + actionNoise;
    }
}
