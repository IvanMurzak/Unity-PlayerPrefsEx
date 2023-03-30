using UnityEngine;
using UnityEngine.UI;
using Extensions.Unity.PlayerPrefsEx;
using TMPro;

public class SampleActions : MonoBehaviour
{
    void ToggleUniqueKey()
    {
        Debug.Log("Toggle Unique Key");
        PlayerPrefsEx.Settings.UniqueKeyPerDevice = !PlayerPrefsEx.Settings.UniqueKeyPerDevice;
        textToggleUniqueKey.text = $"Unique Key: {(PlayerPrefsEx.Settings.UniqueKeyPerDevice ? "Enabled" : "Disabled")}";
    }
    void DeleteAll()
    {
        Debug.Log("Delete All");
        PlayerPrefsEx.DeleteAll();
    }


    // Unity API --------------------------------------------------------//
    //                                                                   //
    public TextMeshProUGUI textToggleUniqueKey;                          //
    public Button btnToggleUniqueKey;                                    //
    public Button btnDeleteAll;                                          //
                                                                         //
    void OnEnable()                                                      //
    {                                                                    //
        btnToggleUniqueKey.onClick.AddListener(ToggleUniqueKey);         //
        btnDeleteAll.onClick.AddListener(DeleteAll);                     //
        textToggleUniqueKey.text = $"Unique Key: {(PlayerPrefsEx.Settings.UniqueKeyPerDevice ? "Enabled" : "Disabled")}";
    }                                                                    //
    void OnDisable()                                                     //
    {                                                                    //
        btnToggleUniqueKey.onClick.RemoveListener(ToggleUniqueKey);      //
        btnDeleteAll.onClick.RemoveListener(DeleteAll);                  //
    }                                                                    //
    //                                                                   //
    // ------------------------------------------------------------------//
}
