using UnityEditor;

public class CreateWeaponBaseAsset
{
    [MenuItem("MetroidVR/Weapon/Base")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<WeaponBase>();
    }
}
