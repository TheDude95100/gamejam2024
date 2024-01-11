using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    //Allumage des lumi�res
    public GameObject FireClose;
    public GameObject FireFar;

    //Gestion du Crane de Jergal
    public GameObject Eyes;
    public Material SkullMat;
    public GameObject SkullFlame;
    public Animator JergalAnimator;
    //Gestion des Dialogues
    public DialogueTrigger UnknownEntity;
    public DialogueTrigger Jergal;


    public DialogueManager dm;

    public bool LightFireFlag = false;
    public bool SkullFlag = false;
    public bool JergalDialFlag = false;
    public bool CoroutineCalled = false;


    public void Start()
    {
        SkullMat.SetFloat("_Speed", 1f);
        JergalAnimator.SetBool("OpenMouth", false);
        Intro();
    }

    void Intro()
    {
        UnknownEntity.TriggerDialogue();
    }
    public void FixedUpdate()
    {
        if(SkullFlag)
        {
            SkullFadeIn();
        }
        if (LightFireFlag) { return; }
        if (!dm.flag)
        {
            return;
        }
        if (!CoroutineCalled)
        {
           StartCoroutine(LightFire());
           SkullFlag = true;
        }
        if (JergalDialFlag)
        {
            if (dm.flag)
            {
                Jergal.TriggerDialogue();
                JergalDialFlag=false;
            }
        }
        if (!JergalDialFlag && dm.flag)
        {
            JergalAnimator.SetBool("OpenMouth", false);
            // SceneManager do the thing
        }
    }

    IEnumerator LightFire()
    {
        LightFireFlag = true;
        yield return new WaitForSeconds(1);
        FireClose.SetActive(true);
        yield return new WaitForSeconds(2);
        FireFar.SetActive(true);
        Eyes.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LightFireFlag= false;
        CoroutineCalled = true;
    }

    void SkullFadeIn()
    {
        if (SkullMat.GetFloat("_Speed") <= 0)
        {
            SkullFlag = false;
            SkullFlame.SetActive(true);
            JergalDialFlag = true;
            JergalAnimator.SetBool("OpenMouth", true);
        }
        SkullMat.SetFloat("_Speed", SkullMat.GetFloat("_Speed") - 0.003f);

    }
}
