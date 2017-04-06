using UnityEngine;

[SerializeField]
public class WeaponBase : ScriptableObject
{
    public GameObject Prefab;
    public ParticleSystem ParticleSystem;

    public bool Chargable;
    public float ChargeTime;
    [Range(0, 1)]
    public float ChargeRate;
    public float ChargeScale;

    public bool Unlimited;
    public int UseCount;

    public float CooldownAmount;

    public virtual void Fire()
    {

    }
}
