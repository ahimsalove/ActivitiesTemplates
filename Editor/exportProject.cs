using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetDatabaseExamples : MonoBehaviour
{
    [MenuItem("Add Life/Export")]
    static void Export()
    {
        var exportedPackageAssetList = new List<string>();
        exportedPackageAssetList.Add("Packages");
        exportedPackageAssetList.Add("Library");
        exportedPackageAssetList.Add("Assets/Add-Life Essentials");
        exportedPackageAssetList.Add("Assets/Scenes");
        exportedPackageAssetList.Add("Assets/StreamingAssets");
        exportedPackageAssetList.Add("Assets/Textures");
        exportedPackageAssetList.Add("Assets/Plugins");
        exportedPackageAssetList.Add("Assets/TextMesh Pro");
        exportedPackageAssetList.Add("Assets/Resources");
        exportedPackageAssetList.Add("Assets/Renderer");
        exportedPackageAssetList.Add("Assets/Add-Life Activities");
        exportedPackageAssetList.Add("Assets/PolygonIcons");
        exportedPackageAssetList.Add("Assets/3rdParty");
        exportedPackageAssetList.Add("Assets/Oculus");
        exportedPackageAssetList.Add("Assets/UI Toolkit");
        exportedPackageAssetList.Add("Assets/XR");
        exportedPackageAssetList.Add("Assets/Prefabs");

        AssetDatabase.ExportPackage(exportedPackageAssetList.ToArray(), "/Add-Life Activities - Full.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Interactive | ExportPackageOptions.IncludeLibraryAssets);
    }
}