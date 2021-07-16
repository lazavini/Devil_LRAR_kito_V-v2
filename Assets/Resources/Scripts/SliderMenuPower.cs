using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenuPower : MonoBehaviour
{
    public GameObject PanelMenuPower;

    public void ShowHideMenuPower()
    {
        if (PanelMenuPower != null)
        {
            Animator animator = PanelMenuPower.GetComponent<Animator>();
            if(animator!=null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}
