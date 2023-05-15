using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerUp : MonoBehaviour
{
    private Image m_Image;
    [SerializeField] Image m_TimeCircleFill;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public int PowerTime { get; set; }

    public Sprite Image
    {
        get => m_Image.sprite;
        set => m_Image.sprite = value;
    }

    public void startTimer()
    {
        StartCoroutine(timerCorutine());
    }

    private IEnumerator timerCorutine()
    {
        float totalTime = PowerTime;
        float elapsedTime = 0f;
        while (elapsedTime < totalTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            m_TimeCircleFill.fillAmount = elapsedTime / totalTime;
        }
        Destroy(gameObject);
    }
}
