using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] _weaponType;
    [SerializeField] GameObject _currentWeapon;

    private void Start()
    {
        foreach (GameObject weapon in _weaponType)
        {
            weapon.SetActive(true);
            weapon.SetActive(false);
        }

        int randomIndex = Random.Range(0, _weaponType.Length);
        Debug.Log(randomIndex);
        _currentWeapon = _weaponType[randomIndex];
        _currentWeapon.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            foreach (GameObject weapon in _weaponType)
            {
                if (collision.gameObject.name.Replace("(Clone)", "").Trim() == weapon.name)
                {
                    _currentWeapon.SetActive(false);
                    weapon.SetActive(true);
                    _currentWeapon = weapon;
                    break;
                }
            }
            Destroy(collision.gameObject);
        }
    }

    public string GetCurrentWeaponName()
    {
        return _currentWeapon != null ? _currentWeapon.name : string.Empty;
    }


}
