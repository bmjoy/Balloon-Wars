using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirTank : MonoBehaviour
{
    public static AirTank Instance {get; private set;}
    [SerializeField] [Range(20f, 200f)] float reduceAirSpeed = 120f;
    [SerializeField] [Range(20f, 200f)] float addAirSpeed = 120f;
    [SerializeField] TextMeshProUGUI AirPercentage;
    [SerializeField] private Image AirAmountImage;


    public int AirAmount{set;get;} = 100; 
    private float AIR_DELTA;
    private IEnumerator addAirCoroutine;
    private IEnumerator reduceAirCoroutine;
    public event Action AirFinished;

    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

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
        return 1f / reduceAirSpeed;
    }

    private float timeBetweenIncrements()
    {
        return 1f / addAirSpeed;
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
        image.rectTransform.position = new Vector3(
        AirAmountImage.rectTransform.position.x + widthToAdd / 2f, AirAmountImage.rectTransform.position.y, 0);
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
