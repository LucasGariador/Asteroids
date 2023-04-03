using UnityEngine;

//To test on the editor, dont let open the scene tab during the play mode
//or the render.isVisible will not work properly.
//This doesnt happend on maximized mode or on build

public class ScreenWrapping : MonoBehaviour
{
    Renderer[] renderers;

    bool isWrappingX = false;
    bool isWrappingY = false;

    Camera cam;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

        cam = Camera.main;
    }

    private void Update()
    {
        ScreenWrap();
    }

    void ScreenWrap()
    {
        foreach (var renderer in renderers)
        {
            if (renderer.isVisible)
            {
                isWrappingX = false;
                isWrappingY = false;
                return;
            }
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        Vector3 newPosition = transform.position;


        Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);


        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;


            isWrappingX = true;
        }

        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;

            isWrappingY = true;
        }

        transform.position = newPosition;
    }
}
