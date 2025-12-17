using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    [SerializeField] float stepInterval = 1.0f; // ‘«‰¹ŠÔŠu
    float timer = 0.0f;
    Rigidbody rb;
    AudioSource footAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        footAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel =rb.linearVelocity;
        float speed = new Vector3(vel.x, 0, vel.z).magnitude;
        // ‚Ù‚Ú“®‚¢‚Ä‚¢‚È‚¯‚ê‚Î‰½‚à‚µ‚È‚¢
        if (speed < 0.1f) { timer=0.0f; return; }
        float interval = stepInterval / speed;
        timer += Time.deltaTime;
        if (timer > interval)
        {
            timer = 0.0f;
            footAudio.Play();
        }
    }
}
