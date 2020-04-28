using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int waterLevel = 0 ;
    public FlowerStage[] stages ;
    public int nbFlower = 0 ;
    FlowerStage currentStage ;
    private MeshFilter mf ;
    private Mesh[] meshes ;
    private List<ParticleCollisionEvent> collisionEvents;
    void Start()
    {
      mf = GetComponent<MeshFilter>() ;
      collisionEvents = new List<ParticleCollisionEvent>();
      CreateMesh() ;
      updateMesh( ) ;
    }
    void CreateMesh() {
      meshes = new Mesh[ stages.Length ] ;
      int i = 0 ;
      foreach( FlowerStage stage in stages) {
        meshes[ i++ ] = Instantiate(stage.mesh) ;
        
      }

    }
    void updateMesh() {
      int i = -1 ;
      foreach( FlowerStage stage in stages) {
        if( waterLevel >= stage.waterLevel ) {
          i++ ;
        }
      }
      mf.mesh = meshes[i] ;
      nbFlower = stages[i].flowerCount ;
    }
    void OnParticleCollision(GameObject particuleSystemObject) {
      int numCollisionEvents = particuleSystemObject.GetComponent<ParticleSystem>().GetCollisionEvents(this.gameObject, collisionEvents);
      if( particuleSystemObject.CompareTag( "Rain") ) {
        StartCoroutine( water( numCollisionEvents ) );
      }

    }
    [System.Serializable]
    public class FlowerStage {
      public int waterLevel = 0 ;
      public Mesh mesh ;
      public int flowerCount = 0 ;
    }
    IEnumerator water( int qtt  ) {
      waterLevel += qtt ;
      updateMesh() ;
      yield return new WaitForSeconds(10);
      waterLevel -= qtt ;
      updateMesh() ;
    }
}
