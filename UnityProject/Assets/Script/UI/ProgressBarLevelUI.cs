using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarLevelUI : MonoBehaviour
{
    public static ProgressBarLevelUI Instance { get; private set; }
    public event EventHandler OnLevelEndUIActive;

    [SerializeField] Image progressBarImages;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        progressBarImages.fillAmount = (float)1 - GameManager.Instance.GetProgressBarImagesUINormalized();
        if (progressBarImages.fillAmount >= 1)
        {
            OnLevelEndUIActive?.Invoke(this, EventArgs.Empty);
        }
    }
}
