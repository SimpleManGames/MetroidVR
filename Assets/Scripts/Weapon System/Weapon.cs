using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponBase> weaponList;

    private WeaponBase _currentWeapon;
    public WeaponBase CurrentWeapon
    {
        get
        {
            if (_currentWeapon == null)
                if (weaponList != null)
                    _currentWeapon = weaponList.First();
                else
#if UNITY_EDITOR
                    Debug.Log("Could not return the CurrentWeapon or a default value for it");
#endif

            return _currentWeapon;
        }
        set
        {
            _currentWeapon = value;
        }
    }

    public Vector3 weaponFirePointOffset;

    GameObject newProjectile;

    private bool _charging = false;
    private float _chargeTimer = 0f;
    Vector3 currentScale = Vector3.zero, wantedScale = Vector3.zero;

    private float _cooldownTimer = 0f;

    void Update()
    {
        _cooldownTimer -= Time.deltaTime;
        if (_cooldownTimer < 0)
            HandleWeaponAction();
    }

    void HandleWeaponAction()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            /// TODO: Set an offset, parent it to the hand
            if ((!_charging || !CurrentWeapon.Chargable) && newProjectile == null)
            {
                newProjectile = Instantiate(CurrentWeapon.Prefab, this.transform.position + weaponFirePointOffset, Quaternion.identity, this.transform);
            }

            if (CurrentWeapon.Chargable)
            {
                _charging = true;
                _chargeTimer += Time.deltaTime;
                newProjectile.transform.localScale = currentScale;
                if (CurrentWeapon.ChargeTime > _chargeTimer)
                {
                    currentScale = newProjectile.transform.localScale;
                    wantedScale = CurrentWeapon.Prefab.transform.localScale * CurrentWeapon.ChargeScale;
                    currentScale = Vector3.Lerp(currentScale, wantedScale, CurrentWeapon.ChargeRate);
                }
            }
        }
        else if (newProjectile != null)
        {
            /// TODO: Implement the limited weapon ammo

            Reset();
        }
    }

    void Reset()
    {
        _chargeTimer = 0f;
        _cooldownTimer = CurrentWeapon.CooldownAmount;
        _charging = false;
        currentScale = Vector3.zero;
        wantedScale = Vector3.zero;
        newProjectile = null;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position + weaponFirePointOffset, .1f);
    }
}
