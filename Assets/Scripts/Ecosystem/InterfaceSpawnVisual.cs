using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InterfaceSpawnVisual : MonoBehaviour
{
    [SerializeField] private VolumeProfile volumeProfile;

    private void Update()
    {
        if (volumeProfile.TryGet(out LensDistortion lensDistotion))
        {
            lensDistotion.intensity.value = Utils.Map(CellManager.Instance.GetSpawnTimer(), 0, CellManager.Instance.GetMawSpawnTimer(), -0.25f, -0.5f);
        }
    }
}
