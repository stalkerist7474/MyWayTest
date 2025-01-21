using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FXAddCoinValue : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 3;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color ExtraColor;
    public TMP_Text coinValue;
    private bool isExtraCoin = false;


    public void StartShow(bool isExtra)
    {
        isExtraCoin = isExtra;

        coinValue.color = normalColor; //��������� ����� ������
        if (isExtraCoin)
        {
            coinValue.color = ExtraColor;
        }

        StartCoroutine(Show());
    }


    //���� �������� �������� ������ ������ �������� � ���������, ����� CanvasGroup
    private IEnumerator Show()
    {

        float alpha = 0f;
        if (canvasGroup != null)
        {
            while (alpha < 1f)
            {
                alpha += fadeSpeed * Time.deltaTime;
                canvasGroup.alpha = alpha;
                yield return null;
            }
        }
        
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());

    }

    private IEnumerator FadeOut()
    {
        float alpha = 1f;
        if (canvasGroup != null)
        {
            while (alpha > 0f)
            {
                alpha -= fadeSpeed * Time.deltaTime;
                canvasGroup.alpha = alpha;
                yield return null;
            }
        }
        gameObject.transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
