using UnityEngine;

// ref: http://wiki.unity3d.com/index.php?title=IsVisibleFrom
public static class RendererExtensions {

    public static bool isVisibleFrom(this Renderer renderer, Camera camera) {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
