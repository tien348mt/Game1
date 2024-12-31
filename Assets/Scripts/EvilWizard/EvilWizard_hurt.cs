using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard_hurt : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EvilWizard_heal heal;

    private void Awake()
    {
        heal = GetComponent<EvilWizard_heal>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        heal.DetectDeath();
    }
}
