using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ActiveModel : MonoBehaviour
{
    public TrackableBehaviour theTrackable;

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Model0(TrackableBehaviour tb)
    {
        string name = tb.TrackableName;
        tb.gameObject.active = !tb.gameObject.active;
        //Debug.Log("IMagem ativa:" + name);


    }
}
