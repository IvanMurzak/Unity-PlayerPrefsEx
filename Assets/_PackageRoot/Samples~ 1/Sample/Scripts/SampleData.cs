using System;
using UnityEngine;

public struct SampleData
{
    public string name;
    public Color color;
    public int count;
    public int[] array;

    public override string ToString()
    {
        return $"name={name}; color={color}; count={count}; array={string.Join(",", array)}";
    }
}
