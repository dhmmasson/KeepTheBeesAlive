using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class collisionSound : MonoBehaviour
{
    public AudioClip[] sounds ;
  //  private AudioSource source  ;
    public float pitch = 1 ;
    private int counter = 0 ;
    void Awake() {
      // AudioMixer mixer = Resources.Load("MasterMixer") as AudioMixer;
      // source = gameObject.AddComponent<AudioSource>() as AudioSource ;
      // source.volume = 0.1f ;
      // source.outputAudioMixerGroup = mixer.FindMatchingGroups("Collision")[0] ;
      // source.spatialize = true ;
      // source.spatialBlend = 1.0f ;
      // source.pitch = 1 ;
      // source.playOnAwake = false ;
    }
    void OnCollisionEnter(Collision collision){
      // int index = Random.Range (0, sounds.Length);
      // source.pitch = pitch + (counter % 3 ) / 30.0f ;
      // source.PlayOneShot( sounds[ index ], 0.1f * collision.impulse.magnitude ) ;
    }

}
