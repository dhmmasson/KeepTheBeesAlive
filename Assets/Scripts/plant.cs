﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public int waterLevel = 0 ;
    public FlowerStage[] stages ;


    private List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
      collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject particuleSystemObject) {
      int numCollisionEvents = particuleSystemObject.GetComponent<ParticleSystem>().GetCollisionEvents(this.gameObject, collisionEvents);
      waterLevel += numCollisionEvents ;
      foreach( FlowerStage stage in stages) {
        if( waterLevel > stage.waterLevel ) {
          MeshFilter mf = GetComponent<MeshFilter>() ;
          mf.mesh = Instantiate(stage.mesh) ;
        }
      }
    }
    [System.Serializable]
    public class FlowerStage {
      public int waterLevel = 0 ;
      public Mesh mesh ;
      public int flowerCount = 0 ;
    }
}