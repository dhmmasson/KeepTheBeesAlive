using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu()]
public class WeatherEvents : ScriptableObject
{
  public WeatherEvent[] elements ;

  [System.Serializable]
  public class WeatherEvent {
    public string label  ;
    public GameObject particle ;
    [Range(10,30)]
    public int Duration ;
    public PostProcessProfile  profile ;
  }

}
