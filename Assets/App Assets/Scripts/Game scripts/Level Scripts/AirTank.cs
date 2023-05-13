using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirTank : MonoBehaviour
{
    [SerializeField] [Range(5f, 200f)] private float m_reduceAirSpeed = 40f;
    [SerializeField] [Range(20f, 200f)] private float m_addAirSpeed = 55f;
    public float ReduceAirSpeed
    {
        get {return m_reduceAirSpeed;}
        set {m_reduceAirSpeed = value;}
    }
    public float AddAirSpeed
    {
        get {return m_addAirSpeed;}
        set {m_addAirSpeed = value;}
    }
    [SerializeField] TextMeshProUGUI AirPercentage;
    [SerializeField] private Image AirAmountImage;

    public int AirAmount{set;get;} = 100; 
    private float AIR_DELTA;
    private IEnumerator addAirCoroutine;
    private IEnumerator reduceAirCoroutine;
    public event Action AirFinished;

    private void Start()
    {
        AIR_DELTA = AirAmountImage.rectTransform.sizeDelta.x / 100f;
        addAirCoroutine = addAir();
        reduceAirCoroutine = reduceAir();
        updatePercentageTextToAirAmount();
    }

    protected void OnAirFinished()
    {
        AirFinished?.Invoke();
    }

    private float timeBetweenReduces()
    {
        return 1f / ReduceAirSpeed;
    }

    private float timeBetweenIncrements()
    {
        return 1f / AddAirSpeed;
    }

    private void updatePercentageTextToAirAmount()
    {
        string percentageText = AirAmount.ToString() + "%";
        AirPercentage.SetText(percentageText);
    }

    private IEnumerator reduceAir()
    {
        while(AirAmount != 0)
        {
            yield return new WaitForSeconds(timeBetweenReduces());
            AirAmount--;
            updatePercentageTextToAirAmount();
            addWidthToImage(AirAmountImage, -AIR_DELTA);
        }

        if (AirAmount == 0)
        {
            OnAirFinished();
        }
    }

    private IEnumerator addAir()
    {
        while(AirAmount != 100)
        {
            yield return new WaitForSeconds(timeBetweenIncrements());
            AirAmount++;
            updatePercentageTextToAirAmount();
            addWidthToImage(AirAmountImage, AIR_DELTA);
        }
    }

    private void addWidthToImage(Image image, float widthToAdd)
    {
        Vector2 curSize = image.rectTransform.sizeDelta;
        image.rectTransform.sizeDelta = new Vector2(curSize.x + widthToAdd, curSize.y);
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
