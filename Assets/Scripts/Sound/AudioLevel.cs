using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLevel : MonoBehaviour {

  public AudioMixerSnapshot UiSnapshot;
  public AudioMixerSnapshot gameSnapshot ;

  public float transitionTime = 1.0f;

  public void GameStart() {
    gameSnapshot.TransitionTo( transitionTime ) ;
  }
}
