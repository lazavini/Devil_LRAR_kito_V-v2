using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class ModelSwapper : MonoBehaviour
{
    public TrackableBehaviour theTrackable;
   
    private bool mSwapModel = false;
    // Use this for initialization
    void Start()
    {
        Debug.Log("estou no start!!!");
        //if (theTrackable == null)
        //{
        //    Debug.Log("Warning: Trackable not set !!");
        //}
    }
    // Update is called once per frame
    void Update()
    {
        //if (mSwapModel && theTrackable != null)
        //{
        //    StateManager sm = TrackerManager.Instance.GetStateManager();
        //    IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();
        //    foreach (TrackableBehaviour tb in tbs)
        //    {
        //        string name = tb.TrackableName;
        //        Debug.Log("Imagem ativa:" + name);
        //        SwapModel(tb);

        //    }
        //    mSwapModel = false;
        //}
    }
   
         public void SwapModel(TrackableBehaviour tb)
        {
                string name = tb.TrackableName;
                tb.gameObject.active = !tb.gameObject.active;
                Debug.Log("IMagem ativa:" + name);
            
            
        }
}