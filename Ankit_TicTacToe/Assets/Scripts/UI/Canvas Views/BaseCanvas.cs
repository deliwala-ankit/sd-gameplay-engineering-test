using UnityEngine;

public abstract class BaseCanvas : MonoBehaviour
{
    protected BaseCanvasConfig Config;

    public virtual void Setup(BaseCanvasConfig config)
    {
        Config = config;
    }
}
