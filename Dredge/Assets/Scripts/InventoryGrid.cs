using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryGrid : MonoBehaviour
{
    public Sprite square;
    private SpriteRenderer lastSelected;

    
    private int[,] grid = {{0, 0, 1, 1, 0, 0},
                           {0, 1, 1, 1, 1, 0},
                            {1, 1, 1, 1, 1, 1},
                            {1, 1, 1, 1, 1, 1},
                            {1, 1, 1, 1, 1, 1},
                            {1, 1, 1, 1, 1, 1},
                            {0, 1, 1, 1, 1, 0},
                            {0, 0, 1, 1, 0, 0}};
                            
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                if(grid[i, j] == 1)
                {
                    GameObject squareObject = new GameObject("Square");
                    squareObject.transform.SetParent(this.transform);
                    squareObject.transform.localPosition = new Vector3(j, -i, 0);
                    SpriteRenderer renderer = squareObject.AddComponent<SpriteRenderer>();
                    renderer.sprite = square;
                    BoxCollider collider = squareObject.AddComponent<BoxCollider>();
                    collider.isTrigger = true;  
                }
                Debug.Log(grid[i, j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            SpriteRenderer currentSelected = hit.collider.GetComponent<SpriteRenderer>();

            if(lastSelected != currentSelected)
            {
                if (lastSelected != null)
                {
                    lastSelected.color = Color.white;
                }

                currentSelected.color = Color.red;
                lastSelected = currentSelected;
            }
        }
        else
        {
            if(lastSelected != null)
            {
                lastSelected.color = Color.white;
                lastSelected = null;
            }
        }

    }
}
