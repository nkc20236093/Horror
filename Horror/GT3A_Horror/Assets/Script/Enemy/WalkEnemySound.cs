using UnityEngine;

public class WalkEnemySound : MonoBehaviour
{
    [SerializeField] float maxDistance = 20;
    [SerializeField] float minDistance = 3;
    AudioSource audioSource;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;// ”O‚Ì‚½‚ßƒ‹[ƒvÝ’è
        audioSource.Play();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > maxDistance)
        {
            audioSource.volume = 0;
        }
        float rate = Mathf.InverseLerp(maxDistance, minDistance, distance);
        audioSource.volume = rate;
    }
}
