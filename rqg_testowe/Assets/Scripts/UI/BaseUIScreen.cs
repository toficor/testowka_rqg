using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup screenCanvasGroup;

    public virtual void DoJob() 
    {

    }

    public virtual void EnableScreen()
    {
        screenCanvasGroup.alpha = 1f;
        screenCanvasGroup.blocksRaycasts = true;
        screenCanvasGroup.interactable = true;
    }

    public virtual void DisableScreen()
    {
        screenCanvasGroup.alpha = 0f;
        screenCanvasGroup.blocksRaycasts = false;
        screenCanvasGroup.interactable = false;
    }
}
