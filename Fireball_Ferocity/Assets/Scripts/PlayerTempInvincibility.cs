using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempInvincibility : MonoBehaviour
{
    public float invincibilityTime;
    // Start is called before the first frame update
    void Start()
    {
        invincibilityTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        invincibilityTime -= Time.deltaTime;
    }
}
