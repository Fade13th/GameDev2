using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class FoVRender : MonoBehaviour {
    private class Line {
        public Vector2 a, b;

        public Line(Vector2 a, Vector2 b) {
            this.a = a;
            this.b = b;
        }
    }

    private class NoIntersectException : Exception {
        public NoIntersectException(string message) : base(message) { }
    }

    private List<Vector2[]> polygons;

    public float angleStart = 0;
    public float angleFinish = Mathf.PI * 0.5f;
    public float radius = 10;

    private int WALL_LAYER = 9;

    List<Vector2> wallObjectCorners;
    List<Line> wallObjectSegments;

    MeshFilter filter;

    // Use this for initialization
    void Start() {
        polygons = new List<Vector2[]>();

        List<GameObject> wallObjects = getWallLayerObjects();

        wallObjectCorners = getCorners(wallObjects);
        wallObjectSegments = getWallSegments(wallObjects);

        filter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update() {
        polygons.Clear();

        //Get polygons
        //Only need one sight polygon, where the object is located"
        /*
        for (float angle = angleStart; angle < angleFinish; angle += (Mathf.PI*2)/10) {
            float dx = Mathf.Cos(angle) * radius;
            float dy = Mathf.Sin(angle) * radius;

            polygons.Add(getSightPolygon(20 + dx, transform.position.y + dy));
            polygons.Add(getSightPolygon(820 + dx, 360 - transform.position.y + dy));
        }
        */

        Vector2[] polygon = getSightPolygon(transform.parent.position.x, transform.parent.position.y);

        //Draw polygons
        Triangulator tr = new Triangulator(polygon);
        int[] indices = tr.Triangulate();

        Vector3[] vertices = new Vector3[polygon.Length];
        for(int i = 0; i < vertices.Length; i++) {
            vertices[i] = new Vector3(polygon[i].x, polygon[i].y, 0);
        }


         Mesh mesh = new Mesh();
        transform.localPosition = -transform.parent.position;
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        filter.mesh = mesh;
    }

    private Vector3 getIntersection(Line a, Line b) {
        float a_px = a.a.x;
        float a_py = a.a.y;
        float a_dx = a.b.x - a.a.x;
        float a_dy = a.b.y - a.a.y;

        float b_px = b.a.x;
        float b_py = b.a.y;
        float b_dx = b.b.x - b.a.x;
        float b_dy = b.b.y - b.a.y;

        float a_mag = Mathf.Sqrt(a_dx * a_dx + a_dy * a_dy);
        float b_mag = Mathf.Sqrt(b_dx * b_dx + b_dy * b_dy);

        if(a_dx / a_mag == b_dx / b_mag && a_dy / a_mag == b_dy / b_mag) {
            throw new NoIntersectException("No intersection");
        }

        float t2 = (a_dx * (b_py - a_py) + a_dy * (a_px - b_px)) / (b_dx * a_dy - b_dy * a_dx);
        float t1 = (b_px + b_dx * t2 - a_px) / a_dx;

        if(t1 < 0 || (t2 < 0 || t2 > 1)) {
            throw new NoIntersectException("Not within parameter");
        }

        return new Vector3(a_px + a_dx * t1, a_py + a_dy * t1, t1);
    }

    private Vector2[] getSightPolygon(float sightX, float sightY) {
        List<float> angles = new List<float>();
        foreach(Vector2 point in wallObjectCorners) {
            float angle = Mathf.Atan2(point.y - sightY, point.x - sightX);
            angles.Add(angle);
        }

        Dictionary<float, Vector3> intersects = new Dictionary<float, Vector3>();
        //Rays in each angle
        foreach(float angle in angles) {
            float dx = Mathf.Cos(angle);
            float dy = Mathf.Sin(angle);

            Line ray = new Line(new Vector2(sightX, sightY), new Vector2(sightX + dx, sightY + dy));

            Vector3 closestIntersect = new Vector3(0, 0, float.MaxValue);
            foreach(Line line in wallObjectSegments) {
                try {
                    Vector3 intersect = getIntersection(ray, line);

                    if(intersect.z < closestIntersect.z) {
                        closestIntersect = intersect;
                    }
                }
                catch(NoIntersectException e) {
                    //Dirty using an exception for this, but it won't let you return null
                    continue;
                }
            }

            if(closestIntersect.z == float.MaxValue) continue;

            intersects.Add(angle, closestIntersect);
        }

        //Sort by angle
        int i = 0;
        List<float> keys = new List<float>();
        foreach(float key in intersects.Keys) {
            keys.Add(key);
            i++;
        }

        keys.Sort();

        Vector2[] sorted = new Vector2[i];

        for(int j = 0; j < i; j++) {
            float key = keys[j];
            sorted[j] = new Vector2(intersects[key].x, intersects[key].y);
        }

        return sorted;
    }

    private List<GameObject> getWallLayerObjects() {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        List<GameObject> wallObjects = new List<GameObject>();

        foreach(GameObject obj in allObjects) {
            if(obj.layer == WALL_LAYER)
                wallObjects.Add(obj);
        }

        return wallObjects;
    }

    private List<Vector2> getCorners(List<GameObject> objects) {
        List<Vector2> corners = new List<Vector2>();

        foreach(GameObject obj in objects) {
            BoxCollider2D box = obj.GetComponent<BoxCollider2D>();
            PolygonCollider2D poly = obj.GetComponent<PolygonCollider2D>();

            if(poly) {
                foreach(Vector2 point in poly.points) {
                    corners.Add(point);
                }
            }
            else if(box) {
                float width = box.size.x/2;
                float height = box.size.y/2;

                GameObject boxObject = box.gameObject;
                var thisMatrix = boxObject.transform.localToWorldMatrix;

                Vector2[] points = new Vector2[4];
                //width + height are from centre point.
                points[0] = thisMatrix.MultiplyPoint3x4(new Vector2(width, height));
                points[1] = thisMatrix.MultiplyPoint3x4(new Vector2(width, -height));
                points[2] = thisMatrix.MultiplyPoint3x4(new Vector2(-width, -height));
                points[3] = thisMatrix.MultiplyPoint3x4(new Vector2(-width, height));


                foreach (Vector2 point in points)
                {
                    corners.Add(point);
                }
            }
        }

        return corners;
    }

    private List<Line> getWallSegments(List<GameObject> objects) {
        List<Line> lines = new List<Line>();

        foreach(GameObject obj in objects) {
            BoxCollider2D box = obj.GetComponent<BoxCollider2D>();
            PolygonCollider2D poly = obj.GetComponent<PolygonCollider2D>();

            if(poly) {
                Vector2[] points = poly.points;

                for(int i = 0; i < points.Length; i++) {
                    if(i + 1 == points.Length) {
                        lines.Add(new Line(points[i], points[0]));
                    }
                    else {
                        lines.Add(new Line(points[i], points[i + 1]));
                    }
                }
            }
            else if(box) {
                float width = box.size.x/2;
                float height = box.size.y/2;

                GameObject boxObject = box.gameObject;
                var thisMatrix = boxObject.transform.localToWorldMatrix;

                Vector2[] points = new Vector2[4];
                //width + height are from centre point.
                points[0] = thisMatrix.MultiplyPoint3x4(new Vector2(width, height));
                points[1] = thisMatrix.MultiplyPoint3x4(new Vector2(width, -height));
                points[2] = thisMatrix.MultiplyPoint3x4(new Vector2(-width, -height));
                points[3] = thisMatrix.MultiplyPoint3x4(new Vector2(-width, height));

                lines.Add(new Line(points[0], points[1]));
                lines.Add(new Line(points[1], points[2]));
                lines.Add(new Line(points[2], points[3]));
                lines.Add(new Line(points[3], points[0]));
            }
        }

        return lines;
    }
}
