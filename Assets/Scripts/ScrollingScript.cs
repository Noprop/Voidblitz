using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(2, 2);
    public Vector2 direction = new Vector2(-1, 0);
    public bool isLinkedToCamera = false;
    public bool isLooping = false;

    private List<SpriteRenderer> backgroundPart;

    void Start() {
        // find the three nebula backgrounds and add to a list
        if (isLooping) {
            backgroundPart = new List<SpriteRenderer>();

            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                if (r != null) {
                    backgroundPart.Add(r);
                }
            }

            backgroundPart = backgroundPart.OrderBy(
                t => t.transform.position.x
            ).ToList();
        }
    }

    void Update() {
        Vector2 movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y
        );

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera) {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping) {
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            // check if the first background is out of view, if so reposition it
            if (firstChild != null && firstChild.transform.position.x < Camera.main.transform.position.x) {
                // caution, it checks from any camera
                if (!firstChild.isVisible) {
                    SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                    Vector3 lastPosition = lastChild.transform.position;
                    Vector3 lastSize = lastChild.bounds.max - lastChild.bounds.min;

                    firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, 0);

                    backgroundPart.Remove(firstChild);
                    backgroundPart.Add(firstChild);
                }
            }
        }
    }
}
