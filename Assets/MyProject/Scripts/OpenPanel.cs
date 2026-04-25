using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject panel;

    public void SwitchActive()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
