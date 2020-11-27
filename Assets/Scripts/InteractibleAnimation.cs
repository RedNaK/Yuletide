using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class InteractibleAnimation : MonoBehaviour
{

    private bool IsMouseOver = false;
    private Animator animator;
    private AudioSource AS;
    public int step = 0;
    public int targetStep = 0;
    public int maxStep = 1 ;

    private bool inAnimation = false;

    // Start is called before the first frame update    
    void Start()
    {
        animator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }

    public void nextAnimation()
    {
        if (targetStep > step)
        {
            animateIt();
            inAnimation = true;
        }
    }

    private void animateIt()
    {
        AS.Play();
        animator.SetTrigger("clic");
        GameManager.instance.animating++;
    }

    public void endStep()
    {
        GameManager.instance.animating--;
        inAnimation = false;
        step++;
        if (targetStep > step)
        {
            nextAnimation();
        }
    }


    void OnMouseOver()
    {
        if (GameManager.instance.overlayActive != true && GameManager.instance.paused != true && !inAnimation)
        {
            if (!IsMouseOver && maxStep > step)
            {
                //Debug.Log("Mouse is over GameObject.");
                GetComponent<ShakeShake>().Begin();
                IsMouseOver = true;
            }
            
        }
    }

    void OnMouseExit()
    {
        //Debug.Log("Mouse is no longer on GameObject.");
        IsMouseOver = false;
    }

    void OnMouseDown()
    {
        if (GameManager.instance.overlayActive != true && GameManager.instance.paused != true && !inAnimation)
        {
            if (maxStep > step)
            {
                targetStep = step + 1;
                nextAnimation();
            }


            //The mouse is no longer hovering over the GameObject so output this message each frame
            //Debug.Log("Mouse is clicking on GameObject.");
        }
    }
}
