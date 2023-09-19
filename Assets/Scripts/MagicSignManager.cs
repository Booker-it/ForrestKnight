using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicSignManager : MonoBehaviour
{
    public int magicSignCount;
    public TextMeshProUGUI magicSigntext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        magicSigntext.text = ": " +magicSignCount.ToString();
    }
}
