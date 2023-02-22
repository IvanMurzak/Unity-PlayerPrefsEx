using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Extensions.Unity.PlayerPrefsEx;
using BigInt = System.Numerics.BigInteger;
using Random = UnityEngine.Random;

public class SampleStaticAPI : MonoBehaviour
{
    void Start()
    {
        SetAll();
        PrintAll();
    }
    
    void DeleteAll()
    {
        textKey.text = $"Key = {key}";
        Debug.Log("Delete All");
        PlayerPrefsEx.DeleteAll();
    }
    void SetAll()
    {
        textKey.text = $"Key = {key}";
        Debug.Log("Set All");
        PlayerPrefsEx.SetString(key, $"{Random.value * Random.Range(10, 1000)}");
        PlayerPrefsEx.SetInt(key, Random.Range(10, 1000));
        PlayerPrefsEx.SetFloat(key, Random.Range(10, 1000) * Random.value);
        PlayerPrefsEx.SetBool(key, Random.value > 0.5f);
        PlayerPrefsEx.SetBigInt(key, BigInt.One * Random.Range(10, 1000));
        PlayerPrefsEx.SetDateTime(key, DateTime.Now - TimeSpan.FromHours(Random.Range(10, 1000)));
        PlayerPrefsEx.SetVector2(key, Vector2.one * Random.Range(10, 1000));
        PlayerPrefsEx.SetVector2Int(key, Vector2Int.one * Random.Range(10, 1000));
        PlayerPrefsEx.SetVector3(key, Vector3.one * Random.Range(10, 1000));
        PlayerPrefsEx.SetVector3Int(key, Vector3Int.one * Random.Range(10, 1000));
    }
    void PrintAll()
    {
        textKey.text = $"Key = {key}";
        Debug.Log(" ");
        Debug.Log("Print All");
        Debug.Log("--------------------------");
        Debug.Log($"String: ----- {PlayerPrefsEx.GetString(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<string>(key)}");
        Debug.Log($"Int: -------- {PlayerPrefsEx.GetInt(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<int>(key)}");
        Debug.Log($"Float: ------ {PlayerPrefsEx.GetFloat(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<float>(key)}");
        Debug.Log($"Bool: ------- {PlayerPrefsEx.GetBool(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<bool>(key)}");
        Debug.Log($"BigInt: ----- {PlayerPrefsEx.GetBigInt(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<BigInt>(key)}");
        Debug.Log($"DateTime: --- {PlayerPrefsEx.GetDateTime(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<DateTime>(key)}");
        Debug.Log($"Vector2: ---- {PlayerPrefsEx.GetVector2(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<Vector2>(key)}");
        Debug.Log($"Vector2Int: - {PlayerPrefsEx.GetVector2Int(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<Vector2Int>(key)}");
        Debug.Log($"Vector3: ---- {PlayerPrefsEx.GetVector3(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<Vector3>(key)}");
        Debug.Log($"Vector3Int: - {PlayerPrefsEx.GetVector3Int(key)}, Key = {PlayerPrefsEx.GetEncryptedKey<Vector3Int>(key)}");
        Debug.Log("-------------------------- end");
    }


    // Unity API --------------------------------------------------------//
    //                                                                   //
    public string key = "myKey";                                         //
    public Button btnSetAll;                                             //
    public Button btnPrintAll;                                           //
    public Button btnDeleteAll;                                          //
    public TextMeshProUGUI textKey;                                      //
                                                                         //
    void OnEnable()                                                      //
    {                                                                    //
        btnSetAll.onClick.AddListener(SetAll);                           //
        btnPrintAll.onClick.AddListener(PrintAll);                       //
        btnDeleteAll.onClick.AddListener(DeleteAll);                     //
    }                                                                    //
    void OnDisable()                                                     //
    {                                                                    //
        btnSetAll.onClick.RemoveListener(SetAll);                        //
        btnPrintAll.onClick.RemoveListener(PrintAll);                    //
        btnDeleteAll.onClick.RemoveListener(DeleteAll);                  //
    }                                                                    //
    //                                                                   //
    // ------------------------------------------------------------------//
}
