using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SnowPickup : MonoBehaviour
    {
        private Snowball _pickedSnowball;
        public int SortingLayerShift;
        [SerializeField] private Transform _snowPickupTransform;
        public void PickSnowball(Snowball snowball)
        {
            if (_pickedSnowball != null)
            {
                DropSnowball(_pickedSnowball);
            }

            _pickedSnowball = snowball;
            snowball.transform.parent = _snowPickupTransform;
            snowball.transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        private void Update()
        {
            if (_pickedSnowball != null)
            {
                _pickedSnowball.ChangeSortingLayer(SortingLayerShift);
            }
        }

        public void DropSnowball(Snowball snowball)
        {
            _pickedSnowball = null;
            snowball.ChangeSortingLayer(0);
            snowball.transform.parent = null;
            snowball.SetPicked(false);
        }

        private void OnMouseDown()
        {
            if (_pickedSnowball != null) DropSnowball(_pickedSnowball);
        }

        public bool HasSnowball()
        {
            return _pickedSnowball != null;
        }
    }
}