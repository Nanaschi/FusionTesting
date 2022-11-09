using Fusion;
using UnityEngine;

public class HPHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(ChangeHP))]
    private byte HpAmount { get; set; }

    [Networked(OnChanged = nameof(DeathLogic))]
    private bool IsDead { get; set; }


    [SerializeField] private byte _maxHP;


    private void Start()
    {
        HpAmount = _maxHP;
    }

    public void TakeDamage(byte amountOfDamage)
    {
        if (IsDead) return;
        HpAmount -= amountOfDamage;
        if (HpAmount <= 0) IsDead = true;
    }

    static void DeathLogic(Changed<HPHandler> changed)
    {
        print($"Your vitality status is {changed.Behaviour.IsDead}");
    }


    static void ChangeHP(Changed<HPHandler> changed)
    {
        print($"Current hp is {changed.Behaviour.HpAmount}");
    }
}