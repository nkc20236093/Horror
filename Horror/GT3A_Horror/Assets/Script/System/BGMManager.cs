using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public float baseVolume = 0.2f;
    public float madnessVolume = 0.8f;
    public float changeSpeed = 0.5f;
    AudioSource audioSource;
    float targetVolume;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        targetVolume = baseVolume;
        audioSource.volume = targetVolume;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, changeSpeed * Time.deltaTime);
    }
    // 外部から正気のの値(0～1)を設定し、環境音を調整
    public void SetMadnessLevel(float rate)
    {
        targetVolume = Mathf.Lerp(baseVolume, madnessVolume, rate);
    }
}
