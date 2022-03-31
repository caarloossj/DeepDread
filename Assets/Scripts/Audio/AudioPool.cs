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
 
        public void PlayAt(Vector3 point, Quaternion rotation)
        {
            var system = Get();
 
            system.transform.position = point;
            system.transform.rotation = rotation;
        }
    }