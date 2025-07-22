using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New GameObject Tile", menuName = "2D/Tiles/GameObject Tile")]
public class GameObjectTile : TileBase
{
    public GameObject m_Prefab;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (m_Prefab != null)
        {
            GameObject instance = Object.Instantiate(m_Prefab);
            instance.transform.position = position + tilemap.GetComponent<Tilemap>().tileAnchor;
            instance.transform.SetParent(tilemap.GetComponent<Tilemap>().transform);
        }
        return true;
    }
}
