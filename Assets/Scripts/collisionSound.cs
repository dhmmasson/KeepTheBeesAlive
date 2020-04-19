using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionSound : MonoBehaviour
{
    public AudioSource[] sounds ;
    void OnCollisionEnter(Collision collision){
      int index = Random.Range (0, sounds.Length);
      sounds[index].Play() ;
    }
}
