using GLTFast;
using UnityEngine;

public class GLBContent : DynamicContent
{
    private void Start()
    {
        Load(FilesIO.GetContentPathById(_id));
    }

    public async override void Load(string path)
    {
        var gltf = new GltfImport();

        var settings = new ImportSettings
        {
            AnimationMethod = AnimationMethod.None,
            NodeNameMethod = NameImportMethod.OriginalUnique
        };

        var success = await gltf.Load(path, settings);

        if (success)
        {
            var gameObject = new GameObject($"Model[{_id}]");
            gameObject.transform.parent = transform;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            gameObject.transform.localScale = Vector3.one;

            await gltf.InstantiateMainSceneAsync(gameObject.transform);
        }
        else
        {
            Debug.LogError($"Loading glTF{_id} failed!");
        }
    }
}
