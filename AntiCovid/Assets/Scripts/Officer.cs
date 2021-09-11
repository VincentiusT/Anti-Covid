using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Officer : MonoBehaviour
{
    public OfficerData officerData;
    //private int level=1;
    //private float refillTime = 15f;
    private float refillTimeTemp;
    private int upgradePrice;
    [SerializeField] private Slider slider;
    [SerializeField] private OfficerLevelSystem[] officerLevelSystem;
    private SpriteRenderer sprite;
    private void Awake()
    {
        officerData.level = 1;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void AssignLevelSystem(OfficerLevelSystem[] lvl)
    {
        officerLevelSystem = lvl;
        upgradePrice = officerLevelSystem[1].price;
        officerData.refillTime = officerLevelSystem[0].refillTime;
        sprite.sprite = officerLevelSystem[0].sprite;
    }

    private void Start()
    {
        
        refillTimeTemp = officerData.refillTime;
        slider.maxValue = officerData.refillTime;
    }

    private void Update()
    {
        if (officerData.refillTime <= 0)
        {
            disbandCrowd();
        }
        else
        {
            officerData.refillTime -= Time.deltaTime;
        }
        slider.value = officerData.refillTime; 
    }

    private void disbandCrowd()
    {
        int n = Citizen.instance.crowds.Count;
        if (n > 0)
        {
            for(int i = 0; i < n; i++)
            {
                if (Citizen.instance.crowds[i] != null)
                {
                    Citizen.instance.crowds[i].DestroyCrowd();
                    officerData.refillTime = refillTimeTemp;
                    break;
                }
            }
        }
        
    }
    public void UpgradeOfficer()
    {
        officerData.level++;

        officerData.refillTime = officerLevelSystem[officerData.level - 1].refillTime;
        sprite.sprite = officerLevelSystem[officerData.level - 1].sprite;
        refillTimeTemp = officerData.refillTime;
        if (officerData.level >= officerLevelSystem.Length) return;
        upgradePrice = officerLevelSystem[officerData.level].price;
    }

    public bool CheckMaxLevel()
    {
        return officerData.level >= officerLevelSystem.Length;
    }

    public int Level
    {
        set { officerData.level = value; }
        get { return officerData.level; }
    }
    public int UpgradePrice
    {
        get { return upgradePrice; }
    }

    public float RefillTime
    {
        get { return officerData.refillTime; }
    }
    public Sprite GetSprite()
    {
        return sprite.sprite;
    }

    public OfficerLevelSystem GetNextValue(int x)
    {
        return officerLevelSystem[x];
    }
}
