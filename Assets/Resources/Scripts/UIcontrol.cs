using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;
using UnityEngine.UI;

public class UIcontrol : MonoBehaviour {

    public float scalingspeed = 0.9f;
    bool ScaleUp = false;
    bool ScaleDown = false;
    // public float x,y,z =
    public Button Play_btn;
    // Update is called once per frame
    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        
    }

    void Update()
    {
        
    }

    public void Play()
    {
        
    }

     
    public void ScaleUpButton()
    {
       
        GameObject.FindWithTag("Devil").transform.localScale += new Vector3(scalingspeed, scalingspeed, scalingspeed); 

    }
    public void ScaleDownButton()
    {
        GameObject.FindWithTag("Devil").transform.localScale += new Vector3(-scalingspeed, -scalingspeed, -scalingspeed);
    }
    public void Up()
    {
         ScaleUp = true;
         ScaleDown = false;
    }
    public void Down()
    {
        ScaleUp = false;
        ScaleDown = true;
    }
    public void Stop()
    {
        ScaleUp = false;
        ScaleDown = false;
    }
}
