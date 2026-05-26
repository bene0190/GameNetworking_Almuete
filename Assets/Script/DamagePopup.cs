using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;

    [SerializeField] private float floatUpSpeed = 1.5f;
    [SerializeField] private float lifetime = 1f;

    private Vector3 moveDirection;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void Setup(int damage)
    {
        damageText.text = damage.ToString();

        // random direction like WoW-style damage popups
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-0.5f, 0.5f);

        moveDirection = new Vector3(randomX, 1f, randomZ).normalized;

        transform.localScale = Vector3.zero;
        StartCoroutine(ScalePop());

        StartCoroutine(FadeAndDestroy());
    }

    private void Update()
    {
        transform.position += moveDirection * (floatUpSpeed * Time.deltaTime);
    }

    private IEnumerator FadeAndDestroy()
    {
        float timer = 0f;

        while (timer < lifetime)
        {
            timer += Time.deltaTime;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
            }

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator ScalePop()
    {
        float t = 0f;

        while (t < 0.15f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t / 0.15f);
            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}