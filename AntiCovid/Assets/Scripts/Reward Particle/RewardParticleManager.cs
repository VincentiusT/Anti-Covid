using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardParticleManager : MonoBehaviour
{
    private static RewardParticleManager instance;

    public static RewardParticleManager Instance { get {
            if(instance == null)
            {
                instance = FindObjectOfType<RewardParticleManager>();
            }
            return instance;
        } }

    GameObject ParticleContainer;

    [Header("Attributes")]
    [SerializeField] private int particleAmountInOneReward;

    [Header("Reference")]
    [SerializeField] private RewardParticle rewardParticlePrefab;
    [SerializeField] private RectTransform MoneyPanel, VaccinePanel;
    [SerializeField] private Sprite moneySprite, vaccineSprite;

    private List<RewardParticle> rewardParticlePool = new List<RewardParticle>();

    private void Start()
    {
        ParticleContainer = new GameObject("Particle Container");
        ParticleContainer.transform.SetParent(transform, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            PlayRewardParticle(RandomEventType.MONEY);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayRewardParticle(RandomEventType.VACCINE);
        }
    }

    public void PlayRewardParticle(RandomEventType randomEventType)
    {
        StartCoroutine(SpawnRewaredParticle(randomEventType));
    }

    private IEnumerator SpawnRewaredParticle(RandomEventType randomEventType)
    {
        for (int i = 0; i < particleAmountInOneReward; i++)
        {
            RewardParticle particle = GetOrCreateRewardParticle(randomEventType == RandomEventType.MONEY ? moneySprite : vaccineSprite);
            Vector2 target = randomEventType == RandomEventType.MONEY ? MoneyPanel.transform.position : VaccinePanel.transform.position;
            particle.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            particle.gameObject.SetActive(true);

            //GANTI KALAU RANDOM EVENT NYA LEBIH DARI 2
            //Vector2 target = MoneyPanel.transform.position;

            particle.PlayParticle(target);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private RewardParticle GetOrCreateRewardParticle(Sprite rewardSprite)
    {
        RewardParticle rewardParticle = rewardParticlePool.Find(r => !r.gameObject.activeSelf);
        if(rewardParticle == null)
        {
            rewardParticle = Instantiate(rewardParticlePrefab, ParticleContainer.transform).GetComponent<RewardParticle>();
            rewardParticlePool.Add(rewardParticle);
        }
        rewardParticle.GetComponent<Image>().sprite = rewardSprite;
        return rewardParticle;
    }
}
