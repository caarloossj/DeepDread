using UnityEngine;

//Code by AndersMalmgren (UnityForum)
public class AudioPool : PollingPool<AudioSource>
    {
        public AudioPool(AudioSource prefab) : base(prefab)
        {
        }
 
        protected override bool IsActive(AudioSource component)
        {
            return component.isPlaying;
        }
 
        public AudioSource GetAudioSource(Vector3 point)
        {
            var source = Get();
 
            source.transform.position = point;

            return source;
        }
    }