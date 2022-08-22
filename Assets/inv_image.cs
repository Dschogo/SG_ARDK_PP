using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inv_image : MonoBehaviour
{
    private Canvas canvas;
    private bool big = false;
    private Vector3 my_transform;

    [SerializeField] private Image content_go;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        //loadedObject = new OBJLoader().Load(objPath);
        // content_go.sprite = Resources.Load<Sprite>("Objs/" + this.name + ".obj") as Sprite;

        // Mesh holderMesh = new Mesh();
        // ObjImporter newMesh = new ObjImporter();
        // holderMesh = newMesh.ImportFile("Obj/0.obj");

        // MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        // MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        // filter.mesh = holderMesh;

    }

    public void make_big()
    {
        if (big)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.position = my_transform;
            big = false;
        }
        else
        {
            my_transform = this.transform.position;
            this.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            this.transform.position = new Vector3(canvas.transform.position.x, canvas.transform.position.y, -30);
            big = true;
        }
    }
}
