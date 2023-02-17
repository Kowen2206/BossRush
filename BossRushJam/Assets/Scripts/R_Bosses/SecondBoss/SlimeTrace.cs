using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(CreateColliderBetweenPoints))]
public class SlimeTrace : MonoBehaviour
{
    LineRenderer _line;
    Vector3 _lastSlimeTrace;
    [SerializeField] float _slimeOffset = 2, _distanceToDisappear = 30;
    [SerializeField] List<Vector3> _slimeVertexsPositions = new List<Vector3>();
    [SerializeField] CreateColliderBetweenPoints _collidersCreator;
    [SerializeField] int _maxSlimeCount = 8;
    [SerializeField] GameObject _residue;
    List<GameObject> _createdColliders = new List<GameObject>();
    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _collidersCreator = GetComponent<CreateColliderBetweenPoints>();
    }

    void Start()
    {
        _slimeVertexsPositions.Add(transform.position);
        _line.SetPosition(0, transform.position);
        _lastSlimeTrace = transform.position;
        _slimeVertexsPositions.Add(_lastSlimeTrace);
        _line.SetPosition(1, _lastSlimeTrace);
    }

    void Update()
    {   
        _line.SetPosition(_slimeVertexsPositions.Count-1, transform.position);
        
        if(Vector3.Distance(transform.position, _lastSlimeTrace) > _slimeOffset)
        {
            _slimeVertexsPositions.Add(transform.position);
            _line.positionCount = _slimeVertexsPositions.Count;
            _line.SetPositions(_slimeVertexsPositions.ToArray());
            _lastSlimeTrace = _slimeVertexsPositions[_slimeVertexsPositions.Count-1];
            _createdColliders.Add(
                _collidersCreator.CreateCollider( _slimeVertexsPositions[_slimeVertexsPositions.Count-2], _slimeVertexsPositions[_slimeVertexsPositions.Count-3])
            );
        }

        if(Vector3.Distance(_line.GetPosition(0), transform.position) > _distanceToDisappear || _slimeVertexsPositions.Count > _maxSlimeCount)
        {
            if(_residue)Instantiate(_residue, _slimeVertexsPositions[0], Quaternion.identity);
            _slimeVertexsPositions.RemoveAt(0);
            _line.positionCount = _slimeVertexsPositions.Count;
            _line.SetPositions(_slimeVertexsPositions.ToArray());
            _createdColliders[0].GetComponent<DestroyObject>().DestroyWithDelay(.5f);
            _createdColliders.RemoveAt(0);
        }
    }

}
