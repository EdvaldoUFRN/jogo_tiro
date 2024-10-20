using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour

{

    public float energia = 1.49f; // energia em joules
    public float massa = 0.0002f;
    public float BackspinDrag = 0.001f;
    private Rigidbody rb;
    private float VelocidadeInicial;
    private float forca;
    private Vector3 Sustentacao;
    void Start()
    {
        VelocidadeInicial = Mathf.Sqrt((2 * energia) / massa);
        rb = GetComponent<Rigidbody>();
        forca = Mathf.Sqrt(VelocidadeInicial) * BackspinDrag;
        Sustentacao = Vector3.up * forca;
    }

    void FixedUpdate()
    {
        rb.AddForce(Sustentacao * Time.deltaTime, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("target"))
        {
            other.gameObject.tag = "Untagged";
            print("hit " + other.gameObject.name + " !");
            StartCoroutine(DestroyAfterDelay(gameObject, 1f));
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(target);
    }
}
