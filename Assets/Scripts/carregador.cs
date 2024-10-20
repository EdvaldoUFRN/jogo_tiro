using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carregador : MonoBehaviour
{
private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
