using UnityEngine;

public class SoundColorLogic : MonoBehaviour
{
    [Header("dependencies")]
    public PlayerController playerScript;
    public LayerMask bulletLayer;
    
    [Header("audio sources")]
    public AudioSource magentaThreatSource;
    public AudioSource cyanThreatSource;
    public AudioSource yellowThreatSource;
    // public AudioSource matchHarmonySource;

    [Header("settings")]
    public float sensorRadius = 3.5f;
    
    private Collider2D[] results = new Collider2D[5]; 

    void Update()
    {
        ScanAndPlayAudio();
    }

    void ScanAndPlayAudio()
    {
        // to find bullets
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, sensorRadius, results, bulletLayer);

        if (count > 0)
        {
            Collider2D closest = GetClosestBullet(count);

            if (closest != null && closest.TryGetComponent<Bullet>(out Bullet b))
            {
                ProcessAudio(b, closest.transform.position);
            }
        }
    }

    void ProcessAudio(Bullet b, Vector3 bulletPos)
    {
        if (playerScript.playerColor != b.bc)
        {
            AudioSource threatSource = GetThreatSource(b.bc);
            
            if (threatSource != null)
            {
                float pan = Mathf.Clamp((bulletPos.x - transform.position.x) / 2f, -1f, 1f);
                threatSource.panStereo = pan;

                Debug.Log("Pan for " + b.bc + " is " + pan);

                if (!threatSource.isPlaying)
                {
                    Debug.Log("Playing audio for " + b.bc);
                    threatSource.Play();
                }
            }
        }
    }

    Collider2D GetClosestBullet(int count)
    {
        Collider2D closest = null;
        float minDst = float.MaxValue;
        
        for(int i=0; i<count; i++)
        {
            float d = (results[i].transform.position - transform.position).sqrMagnitude;
            if(d < minDst)
            {
                minDst = d;
                closest = results[i];
            }
        }
        return closest;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }

    AudioSource GetThreatSource(BulletColor color)
    {
        return color switch
        {
            BulletColor.Magenta => magentaThreatSource,
            BulletColor.Cyan => cyanThreatSource,
            BulletColor.Yellow => yellowThreatSource,
            _ => null,
        };
    }
}
