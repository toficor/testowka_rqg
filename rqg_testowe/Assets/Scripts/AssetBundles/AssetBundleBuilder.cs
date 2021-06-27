using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuilder : MonoBehaviour
{
#if UNITY_EDITOR
	[MenuItem("AssetBundles", menuItem = "Assets/AssetBundles")]

	static void BuildAllAssetBundles()
	{
		BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
	}

#endif
}
