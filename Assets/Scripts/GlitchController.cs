using UnityEngine;

public class GlitchController : MonoBehaviour
{
    public Material GlitchMat;
    public float NoiseAmount;
    public float GlitchStrength;
    public AnimationClip AnimationClip;

    private void Start()
    {
        Destroy(gameObject, AnimationClip.length);
    }

    // Update is called once per frame
    void Update()
    {
        GlitchMat.SetFloat("_NoiseAmount", NoiseAmount);
        GlitchMat.SetFloat("_GlitchStrength", GlitchStrength);
    }
    private void OnDestroy()
    {
        GlitchMat.SetFloat("_NoiseAmount", 0);
        GlitchMat.SetFloat("_GlitchStrength", 0);
    }
}
