using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class VictoriaUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image fondoNegro;
    [SerializeField] private TMP_Text textoVictoria;
    [SerializeField] private GameObject btnref;
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuracion = 1.5f;
    private void Awake()
    {
        SetAlpha(0f);
        btnref.SetActive(false);
    }
    public void IniciarFade()
    {
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        float tiempo = 0f;

        while (tiempo < fadeDuracion)
        {
            tiempo += Time.deltaTime;
            float alpha = tiempo / fadeDuracion;

            SetAlpha(alpha);

            yield return null;
        }
        SetAlpha(1f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        btnref.SetActive(true);
    }
    private void SetAlpha(float alpha)
    {
        if (fondoNegro != null)
            fondoNegro.color = new Color(0, 0, 0, alpha);

        if (textoVictoria != null)
            textoVictoria.color = new Color(1, 1, 1, alpha);
    }
}