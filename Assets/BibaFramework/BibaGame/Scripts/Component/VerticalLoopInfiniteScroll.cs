using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class VerticalLoopInfiniteScroll : MonoBehaviour {
 
	private ScrollRect _scrollRect;
	private ContentSizeFitter _contentSizeFitter; 
	private VerticalLayoutGroup _verticalLayoutGroup;
	private HorizontalLayoutGroup _horizontalLayoutGroup;
	private GridLayoutGroup _gridLayoutGroup;
	private bool _isVertical = false; 
	private bool _isHorizontal = false; 
	private float _disableMarginX = 0;
	private float _disableMarginY = 0;
	private bool _hasDisabledGridComponents = false; 
	private List <RectTransform> items = new List<RectTransform>();
	private Vector2 _newAnchoredPosition = Vector2.zero;
	private float _treshold = 100f; 
	private int _itemCount = 0;
	private float _recordOffsetX = 0;
	private float _recordOffsetY = 0;

	void Awake()
	{
		if(GetComponent<ScrollRect>() != null) {
			_scrollRect = GetComponent<ScrollRect>();
			_scrollRect.onValueChanged.AddListener(OnScroll);
			_scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

			for(int i=0;i<_scrollRect.content.childCount;i++) {
				items.Add(_scrollRect.content.GetChild(i).GetComponent<RectTransform>());
			}
			if(_scrollRect.content.GetComponent<VerticalLayoutGroup>() != null) {
				_verticalLayoutGroup = _scrollRect.content.GetComponent<VerticalLayoutGroup>();
			}
			if(_scrollRect.content.GetComponent<HorizontalLayoutGroup>() != null) {
				_horizontalLayoutGroup = _scrollRect.content.GetComponent<HorizontalLayoutGroup>();
			}

			_isHorizontal = _scrollRect.horizontal;
			_isVertical = _scrollRect.vertical;

			_itemCount = _scrollRect.content.childCount;
		}
		else {
			Debug.LogError("No ScrollRect component found");
		}
	}

	void DisableGridComponents()
	{
		if(_isVertical) {
			_recordOffsetY = items[0].GetComponent<RectTransform>().anchoredPosition.y - items[1].GetComponent<RectTransform>().anchoredPosition.y;
			_disableMarginY = _recordOffsetY * _itemCount /2;
		}
		if(_isHorizontal) {
			_recordOffsetX = items[1].GetComponent<RectTransform>().anchoredPosition.x - items[0].GetComponent<RectTransform>().anchoredPosition.x;
			_disableMarginX = _recordOffsetX * _itemCount /2;
		}
		if(_verticalLayoutGroup) {
			_verticalLayoutGroup.enabled = false; 
		}
		if(_horizontalLayoutGroup) {
			_horizontalLayoutGroup.enabled = false; 
		}
		_hasDisabledGridComponents = true; 
	}

	public void OnScroll(Vector2 pos) {

		if(!_hasDisabledGridComponents) DisableGridComponents();

		for(int i=0;i<items.Count;i++) {
			if(_isHorizontal) {
				if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x > _disableMarginX + _treshold) {
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.x -= _itemCount * _recordOffsetX;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(_itemCount-1).transform.SetAsFirstSibling();
				}
				else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x < -_disableMarginX) {
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.x += _itemCount * _recordOffsetX;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(0).transform.SetAsLastSibling();
				}
			}

			if(_isVertical) {
				if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y > _disableMarginY + _treshold) {
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.y -= _itemCount * _recordOffsetY;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(_itemCount-1).transform.SetAsFirstSibling();
				}
				else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y < -_disableMarginY) {
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.y += _itemCount * _recordOffsetY;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(0).transform.SetAsLastSibling();
				}
			}
		}
	}
}