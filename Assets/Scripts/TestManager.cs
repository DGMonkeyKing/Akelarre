using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestManager : Singleton<TestManager>
{
    public PlayersManager PM;

    public void NewRandomCard()
    {
        PM.PlayerGetCard();
    }

    private static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for(int i =0;i<guids.Length;i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }
}
