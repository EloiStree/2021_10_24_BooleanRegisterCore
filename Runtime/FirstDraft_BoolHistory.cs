using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDraft_BoolHistory : MonoBehaviour
{

    public uint m_timeInMilliseconds;
    public int m_deltaTime;
    public DateTime m_start;
    public DateTime m_previous;
    public DateTime m_current;

    public uint m_line=64;
    public byte m_column=10;
    public ComputableBool m_array;
    public void Start()
    {
        m_start= m_previous= m_current = DateTime.Now;
        m_array = new ComputableBool(m_line, m_column);
    }


    public void SetState(uint index, bool value) {
        Set(index, value);
    }

    private void Update()
    {
        RefreshTime();
    }

    private void RefreshTime()
    {
        m_previous = m_current;
        m_current = DateTime.Now;
        m_timeInMilliseconds = (uint)((m_current - m_start).TotalMilliseconds);
        m_deltaTime = (int)((m_current - m_previous).TotalMilliseconds);
    }
    public void Set(uint index, bool value)
    {
        GetCurrentValue(in index, out int rawValue);
        if (rawValue == 0)
        {
            m_array.SetTime(in index, in m_timeInMilliseconds, value);
        }
        else {

            BoolIntHistoryUtility.IsTrue(in rawValue, out bool isTrue);
            if (isTrue != value)
            {

                m_array.GoNextColumn(index);
                m_array.SetTime(in index, in m_timeInMilliseconds, value);
            }

        }



       
    }

    private void GetCurrentValue(in uint index, out int rawValue)
    {
        m_array.GetValueAtLineIndex(in index, out rawValue);
    }

    private void IsTrue(uint index, out uint realIndex, out int valueInt, out bool isTrue)
    {
        m_array.GetArrayIndexWithOffset(index, 0, out realIndex);
        m_array.GetValueAtArrayIndex(in realIndex, out valueInt);
        BoolIntHistoryUtility.IsTrue(in valueInt, out isTrue);
    }
    private void IsTrue(uint index, out bool isTrue)
    {
        uint realIndex;
        int valueInt;
        m_array.GetArrayIndexWithOffset(index, 0, out realIndex);
        m_array.GetValueAtArrayIndex(in realIndex, out valueInt);
        BoolIntHistoryUtility.IsTrue(in valueInt, out isTrue);
    }
}




public class BoolIntHistoryUtility {

    public static bool IsTrue(in int value, out bool isTrue) => isTrue = value >0;
    public static bool IsFalse(in int value, out bool isFalse) => isFalse = value <= 0;
    public static void GetAbsoluteTimeOf(in int value, out int absolueValue) => absolueValue = value < 0 ? -value: value ;

    public static void SetSignFor(in bool value, in int timeInMilliseconds, out int valueSigned)
    {
        valueSigned = Math.Abs(timeInMilliseconds) * (value ? 1 : -1);
    }
    public static void SetSignFor(in bool value, in uint timeInMilliseconds, out int valueSigned)
    {
        valueSigned = ((int)timeInMilliseconds ) * (value ? 1 : -1);
    }
}


[System.Serializable]
public struct ComputableBool {
    
    public ComputableBoolHistoryIndexes m_indexes;
    public ComputableBoolArray m_values;
    public uint m_ligne;
    public byte m_column;

    public ComputableBool(uint ligne, byte column) {
        m_ligne = ligne;
        m_column = column;
        m_indexes = new ComputableBoolHistoryIndexes( ligne );
        m_values = new ComputableBoolArray( ligne , column );
    }

   

  //  public void GetArrayIndexWithOffsetNoControled(in uint index, out uint arrayIndex) => arrayIndex = (index * m_column) + (uint)m_indexes.m_indexes[index];


    public void GetArrayIndexWithOffset(in uint lineIndex,in  uint columnWantedIndex, out uint arrayIndex)
    {
        GetArrayIndexModuloed(lineIndex, m_indexes.m_indexes[lineIndex] + columnWantedIndex, out arrayIndex);
    }
    public void GetArrayIndexModuloed(in uint index, uint columnIndex, out uint arrayIndex)
    {
        while (columnIndex >= m_column)
            columnIndex -= m_column;
        arrayIndex = (index * m_column) + columnIndex;
    }

    public void GetValueAtArrayIndex(in uint arrayIndex, out int value) => value = (m_values.m_values[arrayIndex]);
    public void GetValueAtLineIndex(in uint lineIndex, out int value) {
        GetCurrentRealIndexOf(in lineIndex, out uint realIndex);
        GetValueAtArrayIndex(in realIndex, out value);


    }


    public void GoNextColumn(uint index)
    {
        m_indexes.m_indexes[index] = m_indexes.m_indexes[index] +1;
        if (m_indexes.m_indexes[index] >= m_column)
            m_indexes.m_indexes[index] = 0;
    }
    public void GoNextColumn(uint index, out uint newOffeset)
    {
        m_indexes.m_indexes[index] = m_indexes.m_indexes[index] + 1;
        if (m_indexes.m_indexes[index] >= m_column)
            m_indexes.m_indexes[index] = 0;
        newOffeset = m_indexes.m_indexes[index];
    }

    public void SwitchSignArrayIndex(in uint arrayIndex)
    {
        m_values.m_values[arrayIndex] *=-1;
    }

    public void SetTime(in uint lineIndex, in uint timeInMilliseconds, in bool value)
    {
        BoolIntHistoryUtility.SetSignFor(in value, in timeInMilliseconds, out int valueSigned);
        GetCurrentRealIndexOf(in lineIndex, out uint realIndex);
        m_values.m_values[realIndex] = valueSigned;
    }

    public void GetCurrentOffsetOf( in uint lineIndex, out uint offset)
    {
        offset = m_indexes.m_indexes[lineIndex];
    }
    public void GetCurrentRealIndexOf(in uint lineIndex, out uint realIndex)
    {
        realIndex= ( lineIndex * m_column ) +  m_indexes.m_indexes[lineIndex];
    }

   
}

[System.Serializable]
public struct ComputableBoolHistoryIndexes {
    public uint[] m_indexes ;

    public ComputableBoolHistoryIndexes(uint ligneCount)
    {
        m_indexes = new uint[ligneCount];
    }
    public void SetSize(int ligneCount)
    {
        m_indexes = new uint[ligneCount];
    }
}

[System.Serializable]
public struct ComputableBoolArray {

    public int[] m_values;

    public ComputableBoolArray(uint ligne, byte column)
    {
        m_values = new int[ligne * column];
    }
    public void SetSize(uint ligne, byte column)
    {
        m_values = new int[ligne * column];
    }
}
