using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFiring : MonoBehaviour
{
    public GameObject projectile;

    public float fireRate;

    private float internalCD;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && internalCD <= 0)
        {
            GameObject new_projectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition) + "|" +  gameObject.transform.position);
            new_projectile.GetComponent<Projectile>().SetDirection((Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position));
            internalCD = fireRate;
        }
        if (internalCD > 0)
        {
            internalCD -= Time.deltaTime;
        }
    }
}
