using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float minForce = 1000f;
    [SerializeField] private float maxForce = 2000f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject _go = Instantiate(projectile, Camera.main.transform.position, Quaternion.identity);
            _go.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * Random.Range(minForce, maxForce));
            StartCoroutine(DestroyProjectile(_go));
        }
    }

    private IEnumerator DestroyProjectile(GameObject _projectile)
    {
        yield return new WaitForSeconds(1);
        Destroy(_projectile);
    }
}
