using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyAndStart : MonoBehaviour
{
    public Image readyImage;
    public Image startImage;

    void Start()
    {
        readyImage.enabled = false;
        startImage.enabled = false;
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(1f);
        readyImage.enabled = true;
        startImage.enabled = false;

        yield return new WaitForSeconds(3f);

        readyImage.enabled = false;
        startImage.enabled = true;
        yield return new WaitForSeconds(1f);
        startImage.enabled = false;
    }
}
