﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class AssetbundlesMenuItems
{

    #region unity5 AssetBundles export

    [MenuItem("Assets/AssetBundles/Build AssetBundles", false, 2)]
    [MenuItem("AssetBundles/Build AssetBundles", false, 2)]
    static public void BuildAssetBundles()
    {
        BuildScript.BuildAssetBundles();
    }


    [MenuItem("Assets/AssetBundles/Set AssetBundle Name", false, 1)]
    static public void SetAssetBundlesName()
    {
        BuildScript.SetAssetBundleName();
    }

    [MenuItem("Assets/AssetBundles/ExportResource", false, 1)]
    static public void ExportResource()
    {
        BuildScript.ExportResource();
    }

    #endregion

    #region lua language config export
    [MenuItem("Hugula/", false, 11)]
    static void Breaker() { }

    [MenuItem("Hugula/export lua [Assets\\Lua]", false, 12)]
    public static void exportLua()
    {
        ExportResources.exportLua();
    }

    [MenuItem("Hugula/export config [Assets\\Config]", false, 13)]
    public static void exportConfig()
    {
        ExportResources.exportConfig();
    }

    [MenuItem("Hugula/export language [Assets\\Lan]", false, 14)]
    public static void exportLanguage()
    {
        ExportResources.exportLanguage();
    }

    [MenuItem("Hugula/", false, 15)]
    static void Breaker1() { }

    [MenuItem("Hugula/build for publish ", false, 16)]
    public static void exportPublish()
    {
        ExportResources.exportPublish();
    }

    [MenuItem("Hugula/clear all ", false, 16)]
    public static void clearAll()
    {
        ExportResources.clearAll();
    }

    #endregion

    #region 加密
    [MenuItem("Hugula AES/", false, 10)]
    static void Breaker2() { }

    [MenuItem("Hugula AES/GenerateKey", false, 12)]
    static void GenerateKey()
    {
        ExportResources.GenerateKey();
    }

    [MenuItem("Hugula AES/GenerateIV", false, 13)]
    static void GenerateIV()
    {
        ExportResources.GenerateIV();
    }

    #endregion
}
