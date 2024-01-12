using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CapacityButton : MonoBehaviour
{
    private int skillPoint = 100; //temporaire

    [SerializeField] private bool unlock;
    [SerializeField] private bool learned;

    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Image background;
    [SerializeField] private Image logoObject;
    [SerializeField] private Image frame;

    [SerializeField] private int indexStat;
    [SerializeField] private int value;
    [SerializeField] private GameObject showBuy;

    [SerializeField] private GameObject[] nextCapacity;
    [SerializeField] private GameObject lockCapacity;

    [SerializeField] private int coutSkillPoint;

    [SerializeField] private Sprite silverFrame;
    [SerializeField] private Sprite goldFrame;

    [SerializeField] private Sprite logo;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private AbilityData dataCapacity;

    private Color32 backgroundColorNotLearn = new Color32(120, 29, 29, 255);
    private Color32 backgroundColorLearn = new Color32(203, 51, 51, 255);
    private Color32 colorBuyedCapacity = new Color32(114, 114, 114, 255);

    public void Start()
    {
        transform.GetComponent<Image>().color = backgroundColorNotLearn;
        logoObject.sprite = logo;
        if(unlock)
        {
            lockIcon.SetActive(false);
        }
    }

    public void Buy()
    {
        if(unlock)
        {
            if(skillPoint > coutSkillPoint) 
            {
                background.color = backgroundColorLearn;
                frame.sprite = goldFrame;
                logoObject.color = colorBuyedCapacity;
                if(nextCapacity.Length > 0)
                {
                    foreach (GameObject capacity in nextCapacity)
                    {
                        capacity.GetComponent<CapacityButton>().Unlock();
                    }
                }
                if (lockCapacity != null)
                {
                    lockCapacity.GetComponent<CapacityButton>().Lock();
                }
                /**
                switch (indexStat)
                {
                    case 0: playerManager.SetAttaqueLegere(value + dataCapacity.BaseDamage);break;
                    case 1: playerManager.SetAttaqueLourde(value + dataCapacity.BaseDamage); break;
                    case 2: playerManager.SetTournade(value + dataCapacity.BaseDamage); break;
                    case 3: playerManager.SetFrenesie(value + dataCapacity.BonusAttackSpeed); break;
                    case 4: playerManager.SetVie(value + dataCapacity.BonusLife); break;
                    case 5: playerManager.SetVitesse(value + dataCapacity.BonusMovementSpeed); break;
                    case 6: playerManager.SetDegat(value + dataCapacity.BonusDamage); break;
                    case 7: playerManager.SetArmure(value + dataCapacity.BonusDefense); break;
                }**/
            }
        }
    }

    public void Unlock()
    {
        unlock = true;
        lockIcon.SetActive(false);
    }

    public void Lock()
    {
        unlock = false;
        lockIcon.SetActive(true);
    }

    public void ShowBuyHint()
    {
        if (unlock)
        {
            showBuy.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().text = value.ToString();
            showBuy.SetActive(true);
        }
    }

    public void HideBuyHint()
    {
        if (unlock)
        {
            showBuy.SetActive(false);
        }
    }
}
