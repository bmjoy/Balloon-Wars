using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirTank : MonoBehaviour
{
    [SerializeField] [Range(2f, 8f)] private float m_reduceAirTime = 5f;
    [SerializeField] [Range(1f, 5f)] private float m_addAirTime = 3f;
    public float ReduceAirTime
    {
        get => m_reduceAirTime;
        set => m_reduceAirTime = value;
    }
    public float AddAirTime
    {
        get => m_addAirTime;
        set => m_addAirTime = value;
    }
    [SerializeField] TextMeshProUGUI m_AirPercentageTxt;
    [SerializeField] private Image m_AirAmountImage;
    public int AirAmount{set;get;} = 100; 
    private float AIR_DELTA;
    private IEnumerator addAirCoroutine;
    private IEnumerator reduceAirCoroutine;
    public event Action AirFinished;

    private void Start()
    {
        AIR_DELTA = m_AirAmountImage.rectTransform.sizeDelta.x / 100f;
        addAirCoroutine = addAir();
        reduceAirCoroutine = reduceAir();
        updatePercentageTextToAirAmount();
    }

    protected void OnAirFinished()
    {
        AirFinished?.Invoke();
    }

    private void updatePercentageTextToAirAmount()
    {
        string percentageText = AirAmount.ToString() + "%";
        m_AirPercentageTxt.SetText(percentageText);
    }

    private IEnumerator reduceAir()
    {
        while(AirAmount > 0)
        {
            yield return null;
            float newFillAmount = m_AirAmountImage.fillAmount - Time.deltaTime / ReduceAirTime;
            m_AirAmountImage.fillAmount = newFillAmount > 0? newFillAmount : 0;
            AirAmount = (int)Math.Ceiling(m_AirAmountImage.fillAmount * 100);
            updatePercentageTextToAirAmount();
        }

        if (AirAmount == 0)
        {
            OnAirFinished();
        }
    }

    private IEnumerator addAir()
    {
        while(AirAmount < 100)
        {
            yield return null;
            float newFillAmount = m_AirAmountImage.fillAmount + Time.deltaTime / AddAirTime;
            m_AirAmountImage.fillAmount = newFillAmount < 1f? newFillAmount : 1f;
            AirAmount = (int)(m_AirAmountImage.fillAmount * 100);
            updatePercentageTextToAirAmount();
        }
    }

    public void StartReduceAir()
    {
        StartCoroutine(reduceAirCoroutine);
    }

    public void StartAddAir()
    {
        StartCoroutine(addAirCoroutine);
    }

    public void StopReduceAir()
    {
        StopCoroutine(reduceAirCoroutine);
        reduceAirCoroutine = reduceAir();
    }

    public void StopAddAir()
    {
        StopCoroutine(addAirCoroutine);
        addAirCoroutine = addAir();
    }
}
