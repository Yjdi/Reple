using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image itemSlider;
    float itemCooldownTime = 10.0f;
    float updateTime = 0.0f;

    void Start()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5f);

        for(int i = 0; i < 3; i++)
        {
            while (updateTime < itemCooldownTime)
            {
                updateTime += Time.deltaTime;
                itemSlider.fillAmount = 1.0f - (Mathf.Lerp(0, 100, updateTime / itemCooldownTime) / 100);
                yield return null;
            }

            updateTime = 0.0f;
            itemSlider.fillAmount = 0.0f;
        }
    }
}
