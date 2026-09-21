using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private ClickController clickController;
    [SerializeField] private CoinsUIHandler coinsUIHandler;
    [SerializeField] private ClickVFXHandler clickVFXHandler;

    public void Awake()
    {
        var coinsManager = new CoinsManager();

        clickController.Initialize(coinsManager);
        coinsUIHandler.Initialzie(coinsManager);
        clickVFXHandler.Initialize(clickController);
    }
}
