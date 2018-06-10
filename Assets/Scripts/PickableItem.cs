using UnityEngine;
using System.Collections;

public class PickableItem : MonoBehaviour {

	public InventoryItem inventoryItem;
	private Inventory inventory;
	private Transform player;
    private HoverEffects hoverEffects;

	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").transform;
		inventory = player.GetComponent<Inventory>();
        hoverEffects = GetComponent<HoverEffects>();

	}
    void Update()
    {
        //Clicked();
    }
    void OnMouseDown() 
	{
		if (inventory.AddItem (inventoryItem)) {				
			if (inventoryItem.picSound != null)
				AudioSource.PlayClipAtPoint(inventoryItem.picSound,player.position);

			Destroy(gameObject);
		}		
	}
    void Clicked ()
    {
        if (Input.GetButtonDown("Fire1")&&hoverEffects.lookingAt)
        {

            if (inventory.AddItem(inventoryItem))
            {
                if (inventoryItem.picSound != null)
                    AudioSource.PlayClipAtPoint(inventoryItem.picSound, player.position);

                Destroy(gameObject);
            }
        }
    }
}