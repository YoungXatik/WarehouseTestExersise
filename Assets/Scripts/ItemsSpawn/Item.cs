using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField] public int ItemCost { get; private set; }

    private void Start()
    {
        gameObject.transform.DOScale(1, 0.2f).From(0).SetEase(Ease.Linear);
    }
}
