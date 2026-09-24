using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TabButton : MonoBehaviour
{
    [SerializeField] private UpgradeType tabType;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(Click);
    }

    private void Click()
    {
        YG2.InterstitialAdvShow();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonSound);
        TabsController.Instance.ChangeTab(tabType);
    }
}
