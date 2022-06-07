using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager instance;
    private PostProcessVolume postVolume;
    private ChromaticAberration chromaticAb;
    private Vignette vg;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        postVolume = GetComponent<PostProcessVolume>();
        postVolume.profile.TryGetSettings(out chromaticAb);
        postVolume.profile.TryGetSettings(out vg);
    }


    private void FixedUpdate()
    {
        vg.intensity.value = Mathf.Lerp(vg.intensity.value, 0, Time.deltaTime * 0.4f);
        chromaticAb.intensity.value = Mathf.Lerp(chromaticAb.intensity.value, 0, Time.deltaTime * 0.9f);
        postVolume.weight = Mathf.Lerp(postVolume.weight, 0.62f, Time.deltaTime * 0.9f);

    }

    public void GoToVignette(float val)
    {
        vg.intensity.value = val;

    }
    public void GoToChromaticAbb(float val)
    {
        chromaticAb.intensity.value = val;
        postVolume.weight = 0.7f;
    }


}
