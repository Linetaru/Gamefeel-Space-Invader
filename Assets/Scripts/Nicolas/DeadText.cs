using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DeadText : MonoBehaviour
{
    public Text leftText;
    private Vector3 leftTPos;
    public Text rightText;
    private Vector3 rightTPos;
    public Text upText;
    private Vector3 upTPos;
    public Text downText;
    private Vector3 downTPos;

    // Start is called before the first frame update
    void Start()
    {
        leftTPos = leftText.rectTransform.position;
        rightTPos = rightText.rectTransform.position;
        upTPos = upText.rectTransform.position;
        downTPos = downText.rectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveText()
    {
        int random = Random.Range(1, 5);
        switch(random)
        {
            case 1:
                leftText.gameObject.SetActive(true);
                leftText.text = "WHY";
                leftText.rectTransform.position = leftTPos;
                leftText.rectTransform.DOMoveX(700, 1f);
                leftText.DOFade(0, 1f).OnComplete(LeftTextCallback);
                break;
            case 2:
                rightText.gameObject.SetActive(true);
                rightText.text = "WHY";
                rightText.rectTransform.position = rightTPos;
                rightText.rectTransform.DOMoveX(1200, 1f);
                rightText.DOFade(0, 1f).OnComplete(RightTextCallback);
                break;
            case 3:
                upText.gameObject.SetActive(true);
                upText.text = "WHY";
                upText.rectTransform.position = upTPos;
                upText.rectTransform.DOMoveY(500, 1f);
                upText.DOFade(0, 1f).OnComplete(UpTextCallback);
                break;
            case 4:
                downText.gameObject.SetActive(true);
                downText.text = "WHY";
                downText.rectTransform.position = downTPos;
                downText.rectTransform.DOMoveY(500, 1f);
                downText.DOFade(0, 1f).OnComplete(DownTextCallback);
                break;
        }
    }

    void LeftTextCallback()
    {
        leftText.color = Color.white;
        leftText.gameObject.SetActive(false);
    }

    void RightTextCallback()
    {
        rightText.color = Color.white;
        rightText.gameObject.SetActive(false);
    }

    void UpTextCallback()
    {
        upText.color = Color.white;
        upText.gameObject.SetActive(false);
    }

    void DownTextCallback()
    {
        downText.color = Color.white;
        downText.gameObject.SetActive(false);
    }
}
