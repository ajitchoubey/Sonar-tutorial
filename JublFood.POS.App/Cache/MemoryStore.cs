using Jublfood.AppLogger;
using JublFood.POS.App.API;
using JublFood.POS.App.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.Cache
{
    public class MemoryStore
    {
        public void LoadMemory()
        {
            try
            {
                Session.OrderType = APILayer.GetOrderTypes();
                Session.CatalogAddressTypes = APILayer.GetAddressTypes();
                Session.catalogControlPropeties = APILayer.GetControlSetting("frmAddress");
                Session.catalogControlText = APILayer.GetControlText("frmAddress");
                Session.catalogAllButtonText = APILayer.GetAllButtonText();


                //Session.catalogTexts = APILayer.GetAllCatalogText();
                //Session.ToppingSizes = APILayer.GetToppingSizes();
                //Session.SpecialtyPizzaText = APILayer.GetSpecialtyPizzaText();
                //Session.ItemParts = APILayer.GetItemParts();
                //Session.CartCaptions = APILayer.GetCartCaptions();
                //Session.MenuTypes = APILayer.GetMenuTypes();
                //Session.EstimatedDeliveryTime = APILayer.GetEstimatedDeliveryTime();
                //Session.EstimatedCarryOutTime = APILayer.GetEstimatedCarryOutTime();
                //Session.UpsellReminder = APILayer.LoadUpsellReminder();
                //LoadMenuCategories();
                //Session.AllCatalogMenuItems = APILayer.GetAllMenuItems();
                //Session.catalogOptionGroups = APILayer.GetOptionGroups("");
                //Session.AllCatalogMenuItemSizes = APILayer.GetAllMenuItemSizes();
                //Session.OrderType = APILayer.GetOrderTypes();
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "MemoryStore-LoadMemory(): " + ex.Message, ex, true);
            }
        }

        private void LoadMenuCategories()
        {
            int MenuTypeId = 0;
            if (Session.MenuTypes == null || Session.MenuTypes.Count <= 0) return;
            if (Session.MenuTypes.Count == 1)
            {
                MenuTypeId = Session.MenuTypes[0].Menu_Type_ID;
            }
            else
            {
                foreach (CatalogMenuTypes _catalogMenuTypes in Session.MenuTypes)
                {
                    if (_catalogMenuTypes.Default_Menu_Type == 1)
                        MenuTypeId = _catalogMenuTypes.Menu_Type_ID;
                }
            }

            if (MenuTypeId > 0)
                Session.menuCategories = APILayer.GetMenuCategories(MenuTypeId);           
        }

        public async void LoadMemoryAsync()
        {
            try
            {
                await LoadSourceNameAsync();
                //Session.StreetLookUpResponseAll = await APILayer.StreetLookUpAsync();                
                Session.catalogTexts = await APILayer.GetAllCatalogTextAsync();
                Session.ToppingSizes = await APILayer.GetToppingSizesAsync();
                Session.SpecialtyPizzaText = await APILayer.GetSpecialtyPizzaTextAsync();
                Session.ItemParts = await APILayer.GetItemPartsAsync();
                Session.CartCaptions = await APILayer.GetCartCaptionsAsync();
                Session.MenuTypes = await APILayer.GetMenuTypesAsync();
                Session.EstimatedDeliveryTime = await APILayer.GetEstimatedDeliveryTimeAsync();
                Session.EstimatedCarryOutTime = await APILayer.GetEstimatedCarryOutTimeAsync();
                Session.UpsellReminder = await APILayer.LoadUpsellReminderAsync();                
                await LoadBussinessUnitAsync();                
                await LoadMenuCategoriesAsync();
                Session.AllCatalogMenuItems = await APILayer.GetAllMenuItemsAsync();
                Session.AllCatalogMenuItemSizes = await APILayer.GetAllMenuItemSizesAsync();
                Session.catalogOptionGroups = await APILayer.GetOptionGroupsAsync(String.Empty);
                Session.catalogOrderPayTypeCodeResponse = await APILayer.GetOrderPayTypeCodesAsync();
                Session.catalogUpsellData = await APILayer.GetUpsellDataAsync();
                 

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", "MemoryStore-LoadMemoryAsync(): " + ex.Message, ex, true);
            }
        }

        private async Task<int> LoadMenuCategoriesAsync()
        {
            int MenuTypeId = 0;
            if (Session.MenuTypes == null || Session.MenuTypes.Count <= 0) return 0;
            if (Session.MenuTypes.Count == 1)
            {
                MenuTypeId = Session.MenuTypes[0].Menu_Type_ID;
            }
            else
            {
                foreach (CatalogMenuTypes _catalogMenuTypes in Session.MenuTypes)
                {
                    if (_catalogMenuTypes.Default_Menu_Type == 1)
                        MenuTypeId = _catalogMenuTypes.Menu_Type_ID;
                }
            }

            if (MenuTypeId > 0)                
                Session.menuCategories = await APILayer.GetMenuCategoriesAsync(MenuTypeId);

            return 0;
        }

        private async Task<int> LoadBussinessUnitAsync()
        {
            
                Session.businessUnits = await APILayer.LoadBussinessUnitAsync(Session._LocationCode);

            return 0;
        }

        private async Task<int> LoadSourceNameAsync()
        {
                
                Session.SourceName = await APILayer.LoadSourceNameAsync(Session._LocationCode);

            
            return 0;
        }
    }
}

          