using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney Instance;

    private void Awake()
    {
        Instance = this;
        moneyText.text = $"{_moneyCount}$";
    }

    [SerializeField] private TextMeshProUGUI moneyText;
    
    private float _moneyCount;

    private Tween _addTween , _removeTween;

    public void AddMoney(int value)
    {
        if (_addTween == null)
        {
            _addTween = DOTween.To(x => _moneyCount = x, _moneyCount,  _moneyCount + value, 1f).SetEase(Ease.Linear)
                .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(_moneyCount)}$"));
        }
        else
        {
            _addTween.Complete();
            _addTween = DOTween.To(x => _moneyCount = x, _moneyCount, _moneyCount + value, 1f).SetEase(Ease.Linear)
                .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(_moneyCount)}$"));
        }
    }

    public void RemoveMoney(int value)
    {
        if (_removeTween == null)
        {
          _removeTween = DOTween.To(x => _moneyCount = x, _moneyCount, _moneyCount - value, 1f).SetEase(Ease.Linear)
                .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(_moneyCount)}$"));
        }
        else
        {
            _removeTween.Complete();
            _removeTween = DOTween.To(x => _moneyCount = x, _moneyCount, _moneyCount - value, 1f).SetEase(Ease.Linear)
                .OnUpdate((() => moneyText.text = $"{Mathf.RoundToInt(_moneyCount)}$"));
        }
        
    }
}
