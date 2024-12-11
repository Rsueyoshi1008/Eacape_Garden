using UnityEngine;

public class DetectionItem : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision c) 
    {
        if(c.gameObject.tag == "Ground")
        {
            if(!audioSource.isPlaying)
            {
                PlaySE();
            }
        }
    }

    private void PlaySE()
    {
        Debug.Log("クラス名: DetectionItem , 関数名: PlaySE");
        audioSource.Play();
    }

    public bool GetAudioIsPlaying()
    {
        return audioSource.isPlaying;
    }
}
