using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class InGameDeathLog : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject inGameDeathLogPanel;

    [SerializeField]
    private GameObject killLog;

    private GameObject list;

    private void Start()
    {
        list = Instantiate(inGameDeathLogPanel, this.transform);
    }

    private void UpdateDeathLog()
    {
        
    }
}
