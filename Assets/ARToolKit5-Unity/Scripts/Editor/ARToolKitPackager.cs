using UnityEditor;

internal class ARToolKitPackager
{
    private const string MAIN_DIRECTORY = "ARToolKit5-Unity";
    private const string PLUGINS_DIRECTORY = "Plugins";
    private const string STREAMINGASSETS_DIRECTORY = "StreamingAssets";

    public static void CreatePackage()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        string fileName = args[args.Length - 1];
        AssetDatabase.ExportPackage(
            AssetDatabase.GetAllAssetPaths(),
            fileName,
            UnityEditor.ExportPackageOptions.Recurse);
    }
}