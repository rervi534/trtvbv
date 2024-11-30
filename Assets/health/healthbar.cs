using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public health playerHealth;
    public Image totalHealtBar;
    public Image currentHealtBar;
    // Start is called before the first frame update
    void Start()
    {
        totalHealtBar.fillAmount = playerHealth.currentHealth / 10 ;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealtBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
