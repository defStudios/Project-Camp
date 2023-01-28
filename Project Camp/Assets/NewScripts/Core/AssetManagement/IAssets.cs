using BeaconProject.Core.Services;
using UnityEngine;

namespace BeaconProject.Core.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
    }
}