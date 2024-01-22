using System;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Serializable]
    private class CanvasView
    {
        public CanvasViewType ViewType;
        public BaseCanvas Canvas;
    }

    [SerializeField] private CanvasView[] listOfCanvases;

    public void ShowCanvasView(BaseCanvasConfig config)
    {
        foreach (var canvas in listOfCanvases)
        {
            if (canvas.ViewType == config.Type)
            {
                canvas.Canvas.Setup(config);
                canvas.Canvas.gameObject.SetActive(true);
            }
            else
            {
                canvas.Canvas.gameObject.SetActive(false);
            }
        }
    }
}
