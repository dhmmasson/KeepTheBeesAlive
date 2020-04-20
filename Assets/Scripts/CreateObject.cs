using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class CreateObject : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public int width = 10;
    public int height = 4;
    public buildingBlocks level ;


    public TMPro.TextMeshProUGUI nextEventTextBox ;
    int nextEvent = 60 ;
    int currentEvent = 0 ;
    public WeatherEvents weathers ;
    public PostProcessVolume volume ;
    public void onStartLevel() {
      createObject() ;

      InvokeRepeating( "checkWeather", 0, 1 ) ;
    }
    void createObject() {
      foreach (buildingBlocks.elementSettings element in level.elements) {
        for( int i = 0 ; i < element.repetition ; i++ ){
          float x = Random.Range(-2, 2) * element.spreadRadius  ;
          float y = i*.5f ;
          Instantiate(element.asset, element.dropCenter + new Vector3(x,y,-0.5f), Quaternion.identity);
        }
      }
    }
    void checkWeather() {
      if( nextEvent == 0 ) {
        volume.profile =( weathers.elements[ currentEvent ].profile ) ;
        GameObject target = weathers.elements[ currentEvent ].particle ;
        nextEvent = weathers.elements[ currentEvent ].Duration ;
        if( target != null ) StartCoroutine( waitForTheRain( target , nextEvent ) ) ;
        currentEvent = (currentEvent + 1 ) % weathers.elements.Length ;
      }
      nextEventTextBox.SetText( weathers.elements[ currentEvent ].label +" in " + nextEvent + "s" ) ;
      nextEvent -- ;
    }

    IEnumerator waitForTheRain( GameObject target, int time ) {
      GameObject weatherSystem = Instantiate( target );
      yield return new WaitForSeconds(time);
      Destroy( weatherSystem ) ;
    }

}
