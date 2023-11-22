using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Slider bossHealthSlider;
    public Slider bossTimerSlider;

    void Start()
    {
        gameObject.SetActive(false);
        BossTotem.onBossStart += OnBossStart;
        Boss.onBossStop += OnBossDie;
    }

    void OnDestroy()
    {
        BossTotem.onBossStart -= OnBossStart;
        Boss.onBossStop -= OnBossDie;
    }

    public void OnBossStart()
    {
        UpdateBossHealthSlider(1f, 1f);
        UpdateBossTimerSlider(1f, 1f);
        gameObject.SetActive(true);
    }

    public void OnBossDie()
    {
        gameObject.SetActive(false);
    }
    public void UpdateBossHealthSlider(float _health, float _maxHealth)
    {
        bossHealthSlider.maxValue = _maxHealth;
        bossHealthSlider.value = _health;
        gameObject.SetActive(true);
    }

    public void UpdateBossTimerSlider(float _timeLeft, float _maxTime)
    {
        bossTimerSlider.maxValue = _maxTime;
        bossTimerSlider.value = _timeLeft;
    }

    void OnEnable()
    {
        Boss.onBossHealthChange += UpdateBossHealthSlider;
        Boss.onBossTimerChange += UpdateBossTimerSlider;

    }
    void OnDisable()
    {
        Boss.onBossHealthChange += UpdateBossHealthSlider;
        Boss.onBossTimerChange += UpdateBossTimerSlider;
    }
}
