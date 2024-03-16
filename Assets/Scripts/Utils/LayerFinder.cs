using UnityEngine;
public enum Layer
{
    Default = 0,
    Ground = 6,
    Unvisible = 7,
}
public static class LayerFinder 
{
    public static LayerMask Get(Layer layer)
    {
        return LayerMask.GetMask(layer.ToString());
    }
    public static int GetIndex(Layer layer)
    {
        return LayerMask.NameToLayer(layer.ToString());
    }
}
