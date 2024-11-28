using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.FilePathAttribute;

namespace LevelCreation
{
    public class LC_AddOnComponent : LC_Component, IBeginDragHandler
    {
        AddOnDirection dir = AddOnDirection.South;
        bool dragging = false;

        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {
            if(dragging & Input.GetKeyDown(KeyCode.Space))
            {
                ShiftDirection();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragging = true;
        }

        public override void OnDrag (PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }

        void ShiftDirection()
        {
            switch(dir)
            {
                case AddOnDirection.North:
                    dir = AddOnDirection.East;
                    break;
                case AddOnDirection.East:
                    dir = AddOnDirection.South;
                    break;
                case AddOnDirection.South:
                    dir = AddOnDirection.West;
                    break;
                case AddOnDirection.West:
                    dir = AddOnDirection.North;
                    break;
            }

            transform.Rotate(new Vector3(0, 0, -90)); 
        }

        public override void RemoveFromCurrentTile()
        {
            currentGridTile.addOns[(int)dir] = null;
            currentGridTile = null;
        }

        protected override void Place(LC_GridTile t)
        {
            PlaceAddOn(t);
            dragging = false;
        }

        void PlaceAddOn(LC_GridTile t)
        {
            Vector3 location = t.gameObject.transform.position;
            float locationOffset = Const.TileSize/2 + GetComponentInChildren<RectTransform>().sizeDelta.y / 2 - 1;

            int dirNumber = 0;

            switch (dir)
            {
                case AddOnDirection.North:
                    location.y += locationOffset;
                    break;
                case AddOnDirection.East:
                    location.x += locationOffset;
                    dirNumber = 1;
                    break;
                case AddOnDirection.South:
                    location.y -= locationOffset;
                    dirNumber = 2;
                    break;
                case AddOnDirection.West:
                    location.x -= locationOffset;
                    dirNumber = 3;
                    break;
            }

            transform.position = location;

            if(t.addOns[dirNumber] != null)
                Destroy(t.addOns[dirNumber].gameObject);

            t.addOns[dirNumber] = this;
        }
    }
}

