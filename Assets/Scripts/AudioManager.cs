using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioclip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [System.Serializable]
public class Sound
    {
        public string name;
        public AudioClip audioclip;
        [Range(0f,1f)] public float volume;
        [Range(0.1f, 3f)] public float pitch;
        public bool loop;

        [HideInInspector] public AudioSource source;

    }
}
