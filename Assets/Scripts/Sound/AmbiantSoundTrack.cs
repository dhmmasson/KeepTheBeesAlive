using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbiantSoundTrack : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip start ;
    public AudioClip[] ambiants ;
    public AudioClip end ;
    public float maxRunTime = 20.0f ;
    [System.Serializable]
    public enum State { Start, Ambiant, End, Stop }  ;
    public State currentState ;
    private int index ;
    private AudioSource[] sources ;

    private float totalRunTime ;
    // Update is called once per frame

    double offset ;
    double nextEventTime ;
    void Start() {
      sources= GetComponents<AudioSource>() ;
      index = 0 ;
      offset =  AudioSettings.dspTime;
    }
    void Update()
    {
      AudioSource currentSource ;
      double time = AudioSettings.dspTime;
      if( currentState != State.Stop ) {
        if (time + 2f > nextEventTime) {
          currentSource = sources[ index ] ;
          currentSource.clip = NextClip() ;
          currentSource.PlayScheduled( nextEventTime ) ;
          nextEventTime += currentSource.clip.length - 0.1f ;
          currentSource.SetScheduledEndTime( nextEventTime  ) ;
          index = 1 - index ;

          // State machine, init timer
          switch( currentState ) {
            case State.Start :
              totalRunTime = currentSource.clip.length ;
              currentState = State.Ambiant ;
              break ;
            case State.End :
              currentState = State.Stop ;
              break ;
            default :
              totalRunTime += currentSource.clip.length ;
              break ;
          }
          //Ensure to schedule end clip to finish in time
          if( totalRunTime + end.length > maxRunTime ) {
            //Should trim previous clip...
            currentState = State.End ;
          }
        }
      }
    }
    public void Play( float time ) {
      nextEventTime = AudioSettings.dspTime;
      maxRunTime = time ;
      currentState = State.Start ;
    }
    public AudioClip NextClip( ) {
      AudioClip nextClip ;
      switch( currentState ) {
        case State.Start :
          nextClip = start ;
        break ;
        case State.Ambiant :

        int next = (int) Random.Range(0, ambiants.Length ) ;
          nextClip = ambiants[next] ;
        break ;
        default :
          nextClip = end ;
        break ;
      }
      return nextClip ;
    }

}
