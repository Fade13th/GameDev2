using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class FoVController : MonoBehaviour {

    public bool PlayerSeen { get; set; }

    private Color _playerVisible, _playerNotVisible;

    public List<Vector2> Points {
        get {
            Vector2[] localPoints = GetComponent<PolygonCollider2D>().points;
            List<Vector2> worldPoints = new List<Vector2>();
            var thisMatrix = transform.localToWorldMatrix;
            for(int i = 0; i < localPoints.Length; i++) {
                worldPoints.Add(thisMatrix.MultiplyPoint3x4(localPoints[i]));
            }
            return worldPoints;
        }
    }

    public List<Line> Edges {
        get {
            List<Line> lines = new List<Line>();
            int length = Points.Count;
            lines.Add(new Line(Points[length - 1], Points[0]));
            for(int i = 0; i < Points.Count - 1; i++) {
                lines.Add(new Line(Points[i], Points[i + 1]));
            }
            return lines;
        }
    }

    public Vector3 Offset { get; set; }
    public Color Color { get { return PlayerSeen ? _playerVisible : _playerNotVisible; } }
    public Vector2 Size { get; private set; }


    // Use this for initialization
    void Start() {
        Offset = transform.localPosition;
        _playerVisible = Color.red;
        _playerVisible.a = 128 / 255f;
        _playerNotVisible = Color.red;
        _playerNotVisible.a = 40 / 255f;

        Vector2 smallest = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 largest = new Vector2(-float.MaxValue, -float.MaxValue);
        foreach (Vector2 point in Points)
        {
            if (smallest.x > point.x)
                smallest.x = point.x;
            if (smallest.y > point.y)
                smallest.y = point.y;
            if (largest.x < point.x)
                largest.x = point.x;
            if (largest.y < point.y)
                largest.y = point.y;
        }

        Size =  new Vector2(largest.x - smallest.x, largest.y - smallest.y);
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = transform.parent.parent.position + Offset;
   }

    void OnTriggerStay2D(Collider2D collider) {
        if(collider.tag == "Player")
        {
            Vector3 origin = transform.parent.parent.position;
            Vector3 direction = collider.transform.position - origin;
            RaycastHit2D raycast = Physics2D.Raycast(origin, direction);
            if(raycast.collider.CompareTag("Player")) { 
                PlayerSeen = true;
            }
            else {
                PlayerSeen = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.tag == "Player") {
            PlayerSeen = false;
        }
    }

}