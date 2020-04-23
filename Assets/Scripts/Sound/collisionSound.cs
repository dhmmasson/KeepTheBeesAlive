using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionSound : MonoBehaviour
{
    public AudioSource[] sounds ;
    void Start() {
      foreach( AudioSource source in sounds ) {
        source.enabled = true  ;
      }
    }
    void OnCollisionEnter(Collision collision){
      int index = Random.Range (0, sounds.Length);
      //sounds[index].Play() ;
    }

}
