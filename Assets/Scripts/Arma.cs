using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Arma : MonoBehaviour
{
    public Text texto; public Text texto2;
    public Text vitoria;
    public GameObject projetil;
    public GameObject carregador;
    public float balas = 12f;
    public Transform saida;
    private float targets;
    private bool pode = true;
    public float coldownn = 1f;
    public float energia = 1.49f; // energia em joules
    public float massa = 0.0002f; // massa em quilogramas (0.2g)
    public float tempoDeVida = 20f;
    private bool carregado = true;
    void Start()
    {

        targets = GameObject.FindGameObjectsWithTag("target").Length;        // Atribua a string à propriedade text do componente Text
        texto.text = "Munição " + balas + "/12";
        vitoria.enabled = false;
    }
    // Calcula a velocidade inicial da BB
    public float VelocidadeInicial()
    {
        return Mathf.Sqrt((2 * energia) / massa);
    }

    // Converte m/s para fps
    private float ConverterParaFPS(float velocidadeEmMS)
    {
        return velocidadeEmMS * 3.28084f;
    }

    // Update is called once per frame
    void Update()
    {
        VerificarTarget();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float velocidadeEmMS = VelocidadeInicial();
            float velocidadeEmFPS = ConverterParaFPS(velocidadeEmMS);
            print("Velocidade inicial de " + velocidadeEmFPS + "FPS");
            Tiro();
        }
        if (VerificarCarregador() && carregado)
        {
            balas = 12;
            carregado = false;

        }
        texto.text = "Munição " + balas + "/12";

    }

    private void VerificarTarget()
    {
        targets = GameObject.FindGameObjectsWithTag("target").Length;
        texto2.text = "Alvos restantes: "+targets ;
        if (targets == 0)
    {
        vitoria.enabled = true;
    }
    }
    private bool VerificarCarregador()
    {
        return GameObject.FindGameObjectsWithTag("carregador").Length == 0;
    }

    private void Tiro()
    {
        if (pode && balas > 0)
        {
            balas--;
            texto.text = "Munição " + balas + "/12";
            GameObject bala = Instantiate(projetil, saida.position, saida.rotation);
            // Usar saida.up se o eixo Y estiver apontando na direção desejada
            bala.GetComponent<Rigidbody>().AddForce(saida.forward.normalized * VelocidadeInicial(), ForceMode.Impulse);
            // destruir a bala depois de um certo tempo
            StartCoroutine(DestruirBala(bala, tempoDeVida));
            StartCoroutine(tirotempo(coldownn));
            pode = false;
        }
        else
        {
            return;
        }
    }

    private IEnumerator tirotempo(float tempoDeVida)
    {
        yield return new WaitForSeconds(tempoDeVida);
        pode = true;
    }

    private IEnumerator DestruirBala(GameObject bala, float tempoDeVida)
    {
        yield return new WaitForSeconds(tempoDeVida);
        Destroy(bala);
    }
}
