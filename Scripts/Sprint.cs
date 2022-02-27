using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    public float full_stamina, current_stamina, stamina_factor;

    [SerializeField]
    UIGameScreen uIGameScreen;
    public bool increasing = false;
    // Start is called before the first frame update
    void Start()
    {
        current_stamina = full_stamina;

    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterMovement.isSprinting)
        {
            decreaseStamina();
        }
        else
        {
            increaseStamina();
        }
    }

    void decreaseStamina()
    {
        if (current_stamina > 0)
        {
            increasing = false;
            current_stamina -= Time.deltaTime * stamina_factor;
            float oldWidth = GetComponent<RectTransform>().rect.width;
            float newWidth = oldWidth * current_stamina / full_stamina;
            uIGameScreen.sprint_front.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
            //sprint_front.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprint_front.rectTransform.rect.width * (current_stamina / full_stamina));
        }
    }

    void increaseStamina()
    {
        if (current_stamina < full_stamina)
        {
            increasing = true;
            current_stamina += Time.deltaTime * stamina_factor;
            float oldWidth = GetComponent<RectTransform>().rect.width;
            float newWidth = oldWidth * current_stamina / full_stamina;
            uIGameScreen.sprint_front.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);

        }
    }
}
