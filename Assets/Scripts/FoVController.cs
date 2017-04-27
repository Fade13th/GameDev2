using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class FoVController : MonoBehaviour {

    public bool PlayerSeen { get; set; }
    private int RaycastMask { get; set; }

    private Color _playerVisible, _playerNotVisible;
    private FoVRender _foVRender;

    public float sightBuffer = 1f;
    private float lastSeen;

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

    public float Rotation { get { return transform.rotation.eulerAngles.z; }
        set
        {
            transform.RotateAround(transform.parent.position, Vector3.forward, -value);
            _foVRender.NeedToUpdate = true;
    } }


    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("FoVColliders");
        PlayerSeen = false;
    }

    // Use this for initialization
    void Start()
    {
        _foVRender = transform.parent.GetComponentInChildren<FoVRender>();
        RaycastMask = 1 << LayerMask.NameToLayer("Walls") | 1 << LayerMask.NameToLayer("PlayerFoVDetection") | 1 << LayerMask.NameToLayer("Platforms");
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
    void FixedUpdate()
    {
        //PlayerSeen = false;
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (PlayerSeen) return;

        if (collider.tag == "PlayerFoVDetection") {
            Vector3 origin = transform.parent.position;
            Vector3 direction = collider.bounds.center - origin;
            RaycastHit2D raycast = Physics2D.Raycast(origin, direction, float.MaxValue, RaycastMask);
            if (raycast.collider.CompareTag("PlayerFoVDetection") || raycast.collider.CompareTag("Player"))
            {
                PlayerSeen = true;
                Debug.DrawLine(origin, raycast.point, Color.magenta);

                if (lastSeen > Time.time) return;
                if (GetComponentInParent<CCTVCamera>() != null) {
                    LevelManager.GetLevelManager().cameraSpot();
                }
                else if (GetComponentInParent<Enemy>() != null) {
                    LevelManager.GetLevelManager().guardSpot();
                    GetComponentInParent<Enemy>().walkChance = 0;
                }
                else if (GetComponentInParent<Toggle>() != null) {
                    LevelManager.GetLevelManager().laserSpot();
                }
            }
            else
            {
                Debug.DrawLine(origin, raycast.point, Color.red);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "PlayerFoVDetection" && PlayerSeen) {
            PlayerSeen = false;
            lastSeen = Time.time + sightBuffer;
        }
    }

    void OnEnable()
    {
        if ( _foVRender != null)
            _foVRender.gameObject.SetActive(true);
    }


    void OnDisable()
    {
        if ( _foVRender != null)
        _foVRender.gameObject.SetActive(false);
    }
}