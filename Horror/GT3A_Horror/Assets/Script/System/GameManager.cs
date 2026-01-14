using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int ItemCounter = 0; // アイテム取得数
    public int CluearItemNum = 5; // クリアに必要なアイテム数
    void Awake()
    {
        // シングルトン対策(簡易)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddItemCounter(int num = 1)
    {
        ItemCounter += num;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemCounter >= CluearItemNum)
        {
            Debug.Log("クリア！");
            RessetScene();
        }
    }
    public void RessetScene()
    {
        // 自身のシーンを読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
