using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLevel : MonoBehaviour {
  public float transitionTime = 1.0f;
  public bool musicMuted = false ;
  public bool soundMuted = false ;
  public string[] snapshotsNames  ;
  private AudioMixerSnapshot[] snapshots  ;
  public void GameStart() {
    AudioMixer mixer = Resources.Load("MasterMixer") as AudioMixer;
    snapshots = new AudioMixerSnapshot[5] ;
    int i = 0 ;
    foreach( string name in snapshotsNames ) {
      snapshots[i++] = mixer.FindSnapshot( name ) ;
    }
    snapshots[0].TransitionTo( transitionTime ) ;
  }
  public void MuteMusic( bool mute ) {
    musicMuted = !musicMuted ;
    int value =  (musicMuted ? 2:0) + (soundMuted ? 1:0) ;
    snapshots[value].TransitionTo( transitionTime ) ;
  }
  public void MuteSound( bool mute ) {
    soundMuted = !soundMuted ;
    int value =  (musicMuted ? 2:0) + (soundMuted ? 1:0) ;
    snapshots[value].TransitionTo( transitionTime ) ;
  }
}
