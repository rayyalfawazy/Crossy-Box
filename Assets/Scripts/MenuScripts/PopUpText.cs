using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField] TMP_Text GameOverText;
    // Start is called before the first frame update
    void Start()
    {
        GameOverText.transform.localScale = Vector3.zero;
        GameOverText.transform.DOScale(new Vector3(1.5f,2f), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
