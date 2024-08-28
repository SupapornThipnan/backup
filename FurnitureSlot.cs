using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ShopSlotData
{
    public Image image;
    public Image imageBG;
    public GameObject priceGO, statusGO , shopGO;
    public Image currentcyImage;
    public TMPro.TextMeshProUGUI priceTX;
}

[System.Serializable]
public class InventSlotData
{
    public Image image;
    public Image imageBG;
    public TMPro.TextMeshProUGUI amountTX;
    public GameObject inventGO;
}

public class FurnitureSlot : MonoBehaviour
{
    public FurnitureData furnitureData;
    public EquipmentData equipmentData;
    public InventoryData inventoryData;

    public bool isShop, isInventory,isFurniture , isBGfurniture;
    public ShopSlotData shopSlotData;
    public InventSlotData inventSlotData;
    public Color selectColor , not_selectColor;

    void Start()
    {
        
    }

    void Update()
    {
        if (isBGfurniture)
        {
            if (inventoryData.amount <= 0) this.gameObject.SetActive(false);
        }
    }

    public void SetFurnitureShopSlot(FurnitureData _furnitureData)
    {
        furnitureData = _furnitureData;
        shopSlotData.image.sprite = furnitureData.spriteIcon;
        shopSlotData.priceTX.text = furnitureData.price.amount.ToString();
        shopSlotData.currentcyImage.sprite = Database.Instance.GetCurrencySprite(furnitureData.price.currency);
        shopSlotData.statusGO.SetActive(true);
        shopSlotData.priceGO.SetActive(false);
        shopSlotData.shopGO.SetActive(true);
        inventSlotData.inventGO.SetActive(false);
        isShop = true;
        isInventory = false;
        isFurniture = true;
        isBGfurniture = false;
    }   
    
    public void SetBGFurnitureShopSlot(FurnitureData _furnitureData , InventoryData _inventoryData)
    {
        furnitureData = _furnitureData;
        inventoryData = _inventoryData;
        inventSlotData.image.sprite = furnitureData.spriteIcon;
        inventSlotData.amountTX.text = _inventoryData.amount.ToString();
        isShop = false;
        isInventory = false;
        isFurniture = false;
        isBGfurniture = true;

        if (furnitureData.furnitureType == FurnitureType.Background)
        {
            if (PlayerManager.Instance.furniturePlayerDatas[0].currentid == furnitureData.id)
            {
                inventSlotData.imageBG.color = selectColor;
            }
        }
        if (furnitureData.furnitureType == FurnitureType.Floor)
        {
            if (PlayerManager.Instance.furniturePlayerDatas[1].currentid == furnitureData.id)
            {
                inventSlotData.imageBG.color = selectColor;
            }
        }
    }

    public void SetFurnitureInventSlot(FurnitureData _furnitureData)
    {
        furnitureData = _furnitureData;
        inventSlotData.image.sprite = furnitureData.spriteIcon;
        shopSlotData.shopGO.SetActive(false);
        inventSlotData.inventGO.SetActive(true);
        isShop = false;
        isInventory = true;
        isFurniture = true;
        isBGfurniture = false;
    }

    public void SetFurnitureShopSlot(EquipmentData _equipmentData)
    {
        equipmentData = _equipmentData;
        shopSlotData.image.sprite = equipmentData.spriteIcon;
        shopSlotData.priceTX.text = equipmentData.price.amount.ToString();
        shopSlotData.currentcyImage.sprite = Database.Instance.GetCurrencySprite(equipmentData.price.currency);
        shopSlotData.statusGO.SetActive(true);
        shopSlotData.priceGO.SetActive(false);
        shopSlotData.shopGO.SetActive(true);
        inventSlotData.inventGO.SetActive(false);
        isShop = true;
        isInventory = false;
        isFurniture = false;
        isBGfurniture = false;
    }


    public void SetEquipmentInventSlot(EquipmentData _equipmentData)
    {
        equipmentData = _equipmentData;
        inventSlotData.image.sprite = equipmentData.spriteIcon;
        shopSlotData.shopGO.SetActive(false);
        inventSlotData.inventGO.SetActive(true);
        isShop = false;
        isInventory = true;
        isFurniture = false;
        isBGfurniture = false;
    }

    public void PushButton()
    {
        if (isShop)
        { 
        
        }

        if (isInventory)
        {
            if (isFurniture)
            {
                //สวมใส่เฟอร์นิเจอร์
                FurnitureManager.Instance.SelectFurnitureSlot(this);
            }
            else
            {
                //สวมใส่ชุด
                EquipmentManager.Instance.SelectEquipmentSlot(this);
            }

        }
        if (isBGfurniture)
        {
            //สร้าง BG , Floor
            if (furnitureData.furnitureType == FurnitureType.Background || furnitureData.furnitureType == FurnitureType.Floor)
            {
                Debug.Log("BG FurnitureType.Background");
                FurnitureManager.Instance.SelectFurnitureSlot(this);
            }
            //สร้างเฟอร์นิเจอร์
            else
            {
                GameObject furGO = BackgroundFurnitureManager.Instance.CreateFurnitureBG(furnitureData.id);
                BackgroundFurnitureManager.Instance.OpenSettingFur(furGO.GetComponent<DragAndDropBG>());
                BackgroundFurnitureManager.Instance.DecaseInventory(furnitureData.id);
                inventSlotData.amountTX.text = inventoryData.amount.ToString();
            }
        }
    }

    
}
