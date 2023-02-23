using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Extensions.Unity.PlayerPrefsEx;
using BigInt = System.Numerics.BigInteger;
using Random = UnityEngine.Random;

public class SampleVariablesAPI : MonoBehaviour
{
    const string Key = "variableKey";

    PlayerPrefsString variableString = new PlayerPrefsString(Key);
    PlayerPrefsInt variableInt = new PlayerPrefsInt(Key);
    PlayerPrefsFloat variableFloat = new PlayerPrefsFloat(Key);
    PlayerPrefsBool variableBool = new PlayerPrefsBool(Key);
    PlayerPrefsBigInt variableBigInt = new PlayerPrefsBigInt(Key);
    PlayerPrefsDateTime variableDateTime = new PlayerPrefsDateTime(Key);
    PlayerPrefsVector2 variableVector2 = new PlayerPrefsVector2(Key);
    PlayerPrefsVector2Int variableVector2Int = new PlayerPrefsVector2Int(Key);
    PlayerPrefsVector3 variableVector3 = new PlayerPrefsVector3(Key);
    PlayerPrefsVector3Int variableVector3Int = new PlayerPrefsVector3Int(Key);

    void Start()
    {
        SetAll();
        PrintAll();
    }
    void SetAll()
    {
        textKey.text = $"Key = {key}";
        Debug.Log("Set All - Variables API");
        variableString.Value = $"{Random.value * Random.Range(10, 1000)}";
        variableInt.Value = Random.Range(10, 1000);
        variableFloat.Value = Random.Range(10, 1000) * Random.value;
        variableBool.Value = Random.value > 0.5f;
        variableBigInt.Value = BigInt.One * Random.Range(10, 1000);
        variableDateTime.Value = DateTime.Now - TimeSpan.FromHours(Random.Range(10, 1000));
        variableVector2.Value = Vector2.one * Random.Range(10, 1000);
        variableVector2Int.Value = Vector2Int.one * Random.Range(10, 1000);
        variableVector3.Value = Vector3.one * Random.Range(10, 1000);
        variableVector3Int.Value = Vector3Int.one * Random.Range(10, 1000);
    }
    void PrintAll()
    {
        textKey.text = $"Key = {key}";
        Debug.Log(" ");
        Debug.Log("Print All - Variables API");
        Debug.Log("--------------------------");
        Debug.Log($"String: ----- {variableString.Value}, Key = {variableString.InternalKey}");
        Debug.Log($"Int: -------- {variableInt.Value}, Key = {variableInt.InternalKey}");
        Debug.Log($"Float: ------ {variableFloat.Value}, Key = {variableFloat.InternalKey}");
        Debug.Log($"Bool: ------- {variableBool.Value}, Key = {variableBool.InternalKey}");
        Debug.Log($"BigInt: ----- {variableBigInt.Value}, Key = {variableBigInt.InternalKey}");
        Debug.Log($"DateTime: --- {variableDateTime.Value}, Key = {variableDateTime.InternalKey}");
        Debug.Log($"Vector2: ---- {variableVector2.Value}, Key = {variableVector2.InternalKey}");
        Debug.Log($"Vector2Int: - {variableVector2Int.Value}, Key = {variableVector2Int.InternalKey}");
        Debug.Log($"Vector3: ---- {variableVector3.Value}, Key = {variableVector3.InternalKey}");
        Debug.Log($"Vector3Int: - {variableVector3Int.Value}, Key = {variableVector3Int.InternalKey}");
        Debug.Log("-------------------------- end");
    }


    // Unity API --------------------------------------------------------//
    //                                                                   //
    public string key = "myKey";                                         //
    public Button btnSetAll;                                             //
    public Button btnPrintAll;                                           //
    public TextMeshProUGUI textKey;                                      //
                                                                         //
    void OnEnable()                                                      //
    {                                                                    //
        btnSetAll.onClick.AddListener(SetAll);                           //
        btnPrintAll.onClick.AddListener(PrintAll);                       //
    }                                                                    //
    void OnDisable()                                                     //
    {                                                                    //
        btnSetAll.onClick.RemoveListener(SetAll);                        //
        btnPrintAll.onClick.RemoveListener(PrintAll);                    //
    }                                                                    //
    //                                                                   //
    // ------------------------------------------------------------------//
}
