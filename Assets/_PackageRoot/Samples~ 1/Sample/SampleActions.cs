using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Extensions.Unity.PlayerPrefsEx;
using BigInt = System.Numerics.BigInteger;
using Random = UnityEngine.Random;

public class SampleActions : MonoBehaviour
{    
    void DeleteAll()
    {
        Debug.Log("Delete All");
        PlayerPrefsEx.DeleteAll();
    }


    // Unity API --------------------------------------------------------//
    //                                                                   //
    public Button btnDeleteAll;                                          //
                                                                         //
    void OnEnable()                                                      //
    {                                                                    //
        btnDeleteAll.onClick.AddListener(DeleteAll);                     //
    }                                                                    //
    void OnDisable()                                                     //
    {                                                                    //
        btnDeleteAll.onClick.RemoveListener(DeleteAll);                  //
    }                                                                    //
    //                                                                   //
    // ------------------------------------------------------------------//
}
