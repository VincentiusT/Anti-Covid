using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Officer : MonoBehaviour
{
    private int level=1;
    private float refillTime = 15f;
    private float refillTimeTemp;
    private int upgradePrice;
    [SerializeField] private Slider slider;
    [SerializeField] private OfficerLevelSystem[] officerLevelSystem;
    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void AssignLevelSystem(OfficerLevelSystem[] lvl)
    {
        officerLevelSystem = lvl;
        upgradePrice = officerLevelSystem[1].price;
        sprite.sprite = officerLevelSystem[0].sprite;
    }

    private void Start()
    {
        refillTime = officerLevelSystem[0].refillTime;
        refillTimeTemp = refillTime;
        slider.maxValue = refillTime;
    }

    private void Update()
    {
        if (refillTime <= 0)
        {
            disbandCrowd();
        }
        else
        {
            refillTime -= Time.deltaTime;
        }
        slider.value = refillTime; 
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
                    refillTime = refillTimeTemp;
                    break;
                }
            }
        }
        
    }
    public void UpgradeOfficer()
    {
        level++;

        refillTime = officerLevelSystem[level - 1].refillTime;
        sprite.sprite = officerLevelSystem[level - 1].sprite;
        refillTimeTemp = refillTime;
        if (level >= officerLevelSystem.Length) return;
        upgradePrice = officerLevelSystem[level].price;
    }

    public bool CheckMaxLevel()
    {
        return level >= officerLevelSystem.Length;
    }

    public int Level
    {
        set { level = value; }
        get { return level; }
    }
    public int UpgradePrice
    {
        get { return upgradePrice; }
    }

    public float RefillTime
    {
        get { return refillTime; }
    }
    public Sprite GetSprite()
    {
        return sprite.sprite;
    }
}
