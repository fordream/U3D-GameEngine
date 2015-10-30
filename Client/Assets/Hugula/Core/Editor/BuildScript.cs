// Copyright (c) 2014 hugula
// direct https://github.com/Hugulor/Hugula
//

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildScript
{

    #region 配置变量
    public const string outPath = "Assets/StreamingAssets";
    public const string suffix = Common.ASSETBUNDLE_SUFFIX;

    public const BuildAssetBundleOptions optionsDefault = BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets; //
    private const BuildAssetBundleOptions optionsDependency = BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.CollectDependencies;// 
#if UNITY_IPHONE
	public const BuildTarget target=BuildTarget.iOS;
#elif UNITY_ANDROID
    public const BuildTarget target = BuildTarget.Android;
#elif UNITY_WP8
	public const BuildTarget target=BuildTarget.WP8Player;
#elif UNITY_METRO
    public const BuildTarget target = BuildTarget.MetroPlayer;
#elif UNITY_STANDALONE_OSX
     public const BuildTarget target = BuildTarget.StandaloneOSXIntel;
#else
    public const BuildTarget target=BuildTarget.StandaloneWindows;
#endif

    #endregion

    public static void BuildAssetBundles()
    {
        CheckstreamingAssetsPath();

        BuildPipeline.BuildAssetBundles(GetOutPutPath(target), optionsDefault, target);
    }

    public static void ExportResource()
    {
        string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "u3d");
        if (path.Length == 0)
            return;
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
        Selection.objects = selection;
    }

    #region 
    /// <summary>
    /// 检查输出目标
    /// </summary>
    static void CheckstreamingAssetsPath()
    {
        string dircAssert = string.Format("{0}/{1}", Application.streamingAssetsPath, target);
        // Application.streamingAssetsPath+"/AssetBundles/"+target.ToString();
        if (!Directory.Exists(dircAssert))
        {
            Directory.CreateDirectory(dircAssert);
        }

        Debug.Log(string.Format("current BuildTarget ={0}", target));
    }

    static string GetOutPutPath(BuildTarget buildTarget)
    {
        return Path.Combine(outPath, buildTarget.ToString());// outPath + "/" + buildTarget.ToString();
    }

    static public BuildTarget GetTarget()
    {
        return target;
    }
    #endregion
}
