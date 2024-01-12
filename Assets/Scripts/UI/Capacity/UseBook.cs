using UnityEngine;

public class UseBook : MonoBehaviour
{
    private Animator m_Animator;
    private int state = 0;
    private bool move = false;
    private Vector3 pos0;
    private Vector3 pos1;

    float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool fadeOutPage1 = false;
    private bool fadeInPage1 = false;
    private bool fadeOutPage2 = false;
    private bool fadeInPage2 = false;

    [SerializeField] private GameObject activeCapacity;
    [SerializeField] private GameObject passiveCapacity;
    private CanvasGroup activeCanvasGroup;
    private CanvasGroup passiveCanvasGroup;

    [SerializeField] private CanvasGroup capacityCanvasGroup;

    void Start()
    {
        m_Animator = GetComponent<Animator>();  

        pos0 = target = transform.localPosition;
        pos1 = new Vector3(-40, -85, 50);
        SetDestination(pos1, 1);

        activeCanvasGroup = activeCapacity.GetComponent<CanvasGroup>();
        passiveCanvasGroup = passiveCapacity.GetComponent<CanvasGroup>();

        passiveCapacity.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if((state == 0 || state==3) && !move)
            {
                move = true;
                m_Animator.SetInteger("OpenPage", 1);
                state = 1;
            }
        }

        if (!fadeOut)
        {
            if (fadeIn)
            {
                FadeIn();
            }

            if (fadeOutPage2)
            {
                Fade1To2();
            }

            if (fadeOutPage1)
            {
                Fade2To1();
            }
        }
        else
        {
            FadeOut();
        }
        if (move) 
        {
            t += Time.deltaTime / timeToReachTarget;
            transform.localPosition = Vector3.Lerp(startPosition, target, t);
            if(transform.localPosition == target)
            {
                move = false;
                if (transform.localPosition == pos1)
                {
                    SetDestination(pos0, 1);
                    fadeIn = true;
                }
                else
                {
                    SetDestination(pos1, 1);
                }
                
            }
        }
    }

    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.localPosition;
        timeToReachTarget = time;
        target = destination;
    }

    private void FadeIn()
    {
        if (activeCanvasGroup.alpha < 1)
        {
            activeCanvasGroup.alpha += Time.deltaTime;
            if (activeCanvasGroup.alpha >= 1)
            {
                fadeIn = false;
            }
        }
    }

    private void FadeOut()
    {
        if (capacityCanvasGroup.alpha >= 0)
        {
            capacityCanvasGroup.alpha -= 4 * Time.deltaTime;
            if (capacityCanvasGroup.alpha == 0)
            {
                fadeOut = false;
                activeCanvasGroup.alpha = 0;
                passiveCanvasGroup.alpha = 0;
                capacityCanvasGroup.alpha = 1;
                activeCapacity.SetActive(true);
                passiveCapacity.SetActive(false);
                fadeInPage1 = false;
                fadeOutPage1 = false;
                fadeInPage2 = false;
                fadeOutPage2 = false;
            }
        }
    }

    private void Fade1To2()
    {
        if (activeCanvasGroup.alpha >= 0 && !fadeInPage2)
        {
            activeCanvasGroup.alpha -= 2 * Time.deltaTime;
            if (activeCanvasGroup.alpha == 0)
            {
                fadeInPage2 = true;
                activeCapacity.SetActive(false);
            }
        }

        if (passiveCanvasGroup.alpha <= 1 && fadeInPage2)
        {
            passiveCanvasGroup.alpha += Time.deltaTime;
            if (passiveCanvasGroup.alpha >= 1)
            {
                fadeOutPage2 = false;
                fadeInPage2 = false;
            }
        }
    }

    private void Fade2To1()
    {
        if (passiveCanvasGroup.alpha >= 0 && !fadeInPage1)
        {
            passiveCanvasGroup.alpha -= 2 * Time.deltaTime;
            if (passiveCanvasGroup.alpha == 0 && !fadeOut)
            {
                fadeInPage1 = true;
                passiveCapacity.SetActive(false);
            }
        }

        if (activeCanvasGroup.alpha <= 1 && fadeInPage1)
        {
            activeCanvasGroup.alpha += Time.deltaTime;
            if (activeCanvasGroup.alpha >= 1)
            {
                fadeOutPage1 = false;
                fadeInPage1 = false;
                passiveCanvasGroup.alpha = 0;
            }
        }
    }

    public void NextPage()
    {
        if (state == 1)
        {
            fadeOutPage2 = true;
            passiveCapacity.SetActive(true);
            m_Animator.SetInteger("OpenPage", 2);
            state = 2;
        }
    }

    public void PreviousPage()
    {
        if (state == 2)
        {
            state = 1;
            fadeOutPage1 = true;
            activeCapacity.SetActive(true);
            m_Animator.SetInteger("OpenPage", 3);
        }
    }

    public void Quit()
    {
        if ((state == 1 || state == 2) && !move)
        {
            move = true;
            m_Animator.SetInteger("OpenPage", 0);
            state = 0;
            fadeOut = true;
        }
    }
}
