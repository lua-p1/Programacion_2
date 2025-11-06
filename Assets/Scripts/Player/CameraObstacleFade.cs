using System.Collections.Generic;
using UnityEngine;
public class CameraObstacleFade : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _fadeSpeed = 5f;
    [SerializeField] private LayerMask _obstacleLayers;
    [SerializeField] private float _minFadeDistance = 2f;
    private List<Renderer> _fadedObjects = new List<Renderer>();
    private Dictionary<Renderer, List<Material>> _originalMaterials = new Dictionary<Renderer, List<Material>>();
    private void Update()
    {
        FadeObstacles();
    }
    private void FadeObstacles()
    {
        Vector3 direction = _player.position - transform.position;
        float distance = Vector3.Distance(transform.position, _player.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, _obstacleLayers);
        HashSet<Renderer> currentHits = new HashSet<Renderer>();
        foreach (var hit in hits)
        {
            if (Vector3.Distance(transform.position, hit.point) < _minFadeDistance) continue;
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend == null) continue;
            currentHits.Add(rend);
            if (!_originalMaterials.ContainsKey(rend))
            {
                _originalMaterials[rend] = new List<Material>(rend.materials);
            }
            foreach (var mat in rend.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.color;
                    c.a = Mathf.MoveTowards(c.a, 0.2f, Time.deltaTime * _fadeSpeed);
                    mat.color = c;
                }
            }
            if (!_fadedObjects.Contains(rend))
                _fadedObjects.Add(rend);
        }
        for (int i = _fadedObjects.Count - 1; i >= 0; i--)
        {
            Renderer rend = _fadedObjects[i];
            if (rend == null)
            {
                _fadedObjects.RemoveAt(i);
                continue;
            }
            if (!currentHits.Contains(rend))
            {
                foreach (var mat in rend.materials)
                {
                    if (mat.HasProperty("_Color"))
                    {
                        Color c = mat.color;
                        c.a = Mathf.MoveTowards(c.a, 1f, Time.deltaTime * _fadeSpeed);
                        mat.color = c;
                    }
                }
                if (rend.materials.Length > 0 && rend.materials[0].color.a >= 0.99f)
                {
                    _fadedObjects.RemoveAt(i);
                }
            }
        }
    }
}

