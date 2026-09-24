using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ADButton : MonoBehaviour
{
    [SerializeField] private ClickController controller;

    private Coroutine coroutine;

    [SerializeField] private Button button;

    private void Start()
    {
        button.gameObject.SetActive(true);

        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        button.gameObject.SetActive(false);

        YG2.RewardedAdvShow("");

        controller.SetAdBonus();

        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(ShowCoroutine());
    }

    private IEnumerator ShowCoroutine()
    {
        yield return new WaitForSeconds(120);
        button.gameObject.SetActive(true);
    }
}
