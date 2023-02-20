using Jublfood.AppLogger;
using JublFood.Order.Models;
using JublFood.POS.App.Cache;
using JublFood.POS.App.Class;
using JublFood.POS.App.Models;
using JublFood.POS.App.Models.Cart;
using JublFood.POS.App.Models.Catalog;
using JublFood.POS.App.Models.Customer;
using JublFood.POS.App.Models.Employee;
using JublFood.POS.App.Models.Order;
using JublFood.POS.App.Models.Payment;
using JublFood.POS.App.Models.Printing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace JublFood.POS.App.API
{
    public static class APILayer
    {
        private static int StatusCode = 0;
        public static string StatusMsg = string.Empty;
        public static Exception exception;

        public enum CallType
        {
            GET,
            POST,
            PUT,
            DELETE
        };        

        //public static  async Task<IEnumerable<CatalogItems>> GetCategories()
        //{
        //    IEnumerable<CatalogItems> catalogItems = Enumerable.Empty<CatalogItems>();

        //    CatalogItemsRequest catalogItemsRequest = new CatalogItemsRequest() { Location_Code = Session._LocationCode, Menu_Type_ID = 1 };

        //    using (var httpClient = new HttpClient())
        //    {
        //        String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCategories";
        //        //var catalogs = new List<CatalogItems>();

        //        httpClient.BaseAddress = new Uri(urlGetCatalog);
        //        httpClient.DefaultRequestHeaders.Accept.Clear();
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + catalogItemsRequest.Location_Code + "&Menu_Type_ID=" + catalogItemsRequest.Menu_Type_ID);

        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            catalogItems = responseMessage.Content.ReadAsAsync<IEnumerable<CatalogItems>>().Result;
        //        }
        //    }
        //    return catalogItems;
        //}

        public static List<CatalogMenuCategory> GetMenuCategories(int MenuTypeID)
        {
            List<CatalogMenuCategory> catalogMenuCategories = new List<CatalogMenuCategory>();
            string requestString = "";
            try
            {
                CatalogMenuCategoryResponse catalogMenuCategoryResponse = new CatalogMenuCategoryResponse();

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuCategories";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Type_ID=" + MenuTypeID;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Type_ID=" + MenuTypeID).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuCategoryResponse = responseMessage.Content.ReadAsAsync<CatalogMenuCategoryResponse>().Result;
                        if (catalogMenuCategoryResponse.catalogMenuCategories != null)
                            catalogMenuCategories = catalogMenuCategoryResponse.catalogMenuCategories;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuCategories", requestString, string.Empty, ex);
            }

            return catalogMenuCategories;
        }

        
        public static List<CatalogMenuItems> GetMenuItems(string Menu_Category_Code, string Order_Type_Code)
        {
            CatalogMenuItemsResponse catalogMenuItemsResponse = new CatalogMenuItemsResponse();
            List<CatalogMenuItems> catalogMenuItems = new List<CatalogMenuItems>();
            string requestString = "";
            try
            {
                if (Session.AllCatalogMenuItems != null && Session.AllCatalogMenuItems.Count > 0)
                {
                    List<CatalogMenuItems> items = Session.AllCatalogMenuItems.FindAll(x => x.Menu_Category_Code == Menu_Category_Code && x.Order_Type_Code == Order_Type_Code);
                    if (items != null && items.Count > 0) return items;
                }


                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuItems";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Category_Code=" + Menu_Category_Code + "&Order_Type_Code=" + Order_Type_Code;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Category_Code=" + Menu_Category_Code + "&Order_Type_Code=" + Order_Type_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemsResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemsResponse>().Result;

                        if (catalogMenuItemsResponse.catalogMenuItems != null)
                        {
                            catalogMenuItems = catalogMenuItemsResponse.catalogMenuItems;
                            if (Session.AllCatalogMenuItems == null) Session.AllCatalogMenuItems = new List<CatalogMenuItems>();
                            Session.AllCatalogMenuItems.AddRange(catalogMenuItems);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuItems", requestString, string.Empty, ex);
            }

            return catalogMenuItems;
        }

        public static List<CatalogMenuItemSizes> GetMenuItemSizes(string Menu_Code, string Order_Type_Code)
        {
            CatalogMenuItemSizesResponse catalogMenuItemSizesResponse = new CatalogMenuItemSizesResponse();
            List<CatalogMenuItemSizes> catalogMenuItemSizes = new List<CatalogMenuItemSizes>();
            string requestString = "";
            try
            {
                if (Session.AllCatalogMenuItemSizes != null && Session.AllCatalogMenuItemSizes.Count > 0)
                {
                    List<CatalogMenuItemSizes> itemSizes = Session.AllCatalogMenuItemSizes.FindAll(x => x.Menu_Code == Menu_Code && x.Order_Type_Code == Order_Type_Code);
                    if (itemSizes != null && itemSizes.Count > 0) return itemSizes;
                }

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuItemSizes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code + "&Order_Type_Code=" + Order_Type_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code + "&Order_Type_Code=" + Order_Type_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemSizesResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemSizesResponse>().Result;
                        if (catalogMenuItemSizesResponse.catalogMenuItemSizes != null)
                            catalogMenuItemSizes = catalogMenuItemSizesResponse.catalogMenuItemSizes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuItemSizes", requestString, string.Empty, ex);
            }

            return catalogMenuItemSizes;
        }


        public static Cart Add2Cart(Cart cartRequest)
        {
            Cart cartResult = new Cart();
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "Add2Cart";
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cartRequest), Encoding.UTF8, "application/json");
                CartResponse cartResponse = new CartResponse();

                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        cartResponse = JsonConvert.DeserializeObject<CartResponse>(jsonResponse);

                        if (cartResponse.ResponseCode == "1")
                        {
                            cartResult = new Cart();
                            cartResult.cartHeader = cartResponse.cartHeader;
                            cartResult.Customer = cartResponse.Customer;
                            cartResult.cartItems = cartResponse.cartItems;
                            cartResult.orderGiftCards = cartResponse.orderGiftCards;
                            cartResult.orderUDT = cartResponse.orderUDT;
                            cartResult.itemCombos = cartResponse.itemCombos;
                            cartResult.orderPayments = cartResponse.orderPayments;
                            cartResult.orderCreditCards = cartResponse.orderCreditCards;
                            cartResult.orderReasons = cartResponse.orderReasons;
                            cartResult.orderAdditionalCharges = cartResponse.orderAdditionalCharges;
                        }
                        else
                        {
                            cartResult = Session.cart;
                        }

                    }
                    else
                    {
                        cartResult = Session.cart;
                    }
                }
            }
            catch (Exception ex)
            {
                cartResult = Session.cart;
                Logging("ERROR", "Add2Cart", JsonConvert.SerializeObject(cartRequest), string.Empty, ex);
            }

            return cartResult;

        }


        //public static Cart DiscardCart(string CartID, Cart cart)
        //{
        //    Cart cartResult = new Cart();
        //    int RowsAffected = 0;
        //    try
        //    {
        //        String Uri = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "DiscardCart";
        //        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(CartID), Encoding.UTF8, "application/json");



        //        using (var client = new HttpClient())
        //        {
        //            var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseContent = response.Content;
        //                string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
        //                RowsAffected = JsonConvert.DeserializeObject<int>(jsonResponse);

        //            }
        //            else
        //            {
        //                RowsAffected = 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        RowsAffected = 0;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    if (RowsAffected > 0)
        //        return null;
        //    else
        //        return cart;

        //}

        public static Cart InsertCartOnHold(CartOnHoldRequest cartOnHoldRequest)
        {
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "InsertCartOnHold";
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cartOnHoldRequest), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }

                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "InsertCartOnHold", JsonConvert.SerializeObject(cartOnHoldRequest), string.Empty, ex);
                return Session.cart;
            }

            return null;

        }

        public static Cart UpdateCartOnHold(CartOnHoldRequest cartOnHoldRequest)
        {
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "UpdateCartOnHold";
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cartOnHoldRequest), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "UpdateCartOnHold", JsonConvert.SerializeObject(cartOnHoldRequest), string.Empty, ex);
                return Session.cart;
            }

            return null;

        }

        public static int UpdateCartItemBasedOnTopping(UpdateCartItemBasedOnToppingRequest request)
        {
            String Uri = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "UpdateCartItemBasedOnTopping";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            int RowsAffected = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        RowsAffected = JsonConvert.DeserializeObject<int>(jsonResponse);

                    }
                    else
                    {
                        RowsAffected = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                RowsAffected = 0;
                Logging("ERROR", "UpdateCartItemBasedOnTopping", JsonConvert.SerializeObject(request), string.Empty, ex);
            }

            return RowsAffected;

        }

        public static CartOnHoldResponse GetCartOnHold()
        {
            CartOnHoldResponse cartOnHoldResponse = new CartOnHoldResponse();
            List<CartOnHoldModel> cartOnHoldModels = new List<CartOnHoldModel>();
            try
            {

                using (var httpClient = new HttpClient())
                {
                    String url = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "GetCartOnHold";

                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(url).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        cartOnHoldResponse = responseMessage.Content.ReadAsAsync<CartOnHoldResponse>().Result;
                        cartOnHoldModels = cartOnHoldResponse.cartOnHolds;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCartOnHold", string.Empty, string.Empty, ex);
            }

            return cartOnHoldResponse;
        }

        public static CartResponse GetCart(string cartId)
        {
            CartResponse cartResponse = new CartResponse();
            List<CartOnHoldModel> cartOnHoldModels = new List<CartOnHoldModel>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String url = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "GetCart";

                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(url + "?cartId=" + cartId).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        cartResponse = responseMessage.Content.ReadAsAsync<CartResponse>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCart", cartId, string.Empty, ex);
            }

            return cartResponse;
        }

        public static List<CatalogText> GetToppingSizes()
        {
            List<CatalogText> toppingSizes = new List<CatalogText>();

            if (Session.ToppingSizes != null && Session.ToppingSizes.Count > 0)
            {
                return Session.ToppingSizes;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetToppingSizes";


                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        toppingSizes = responseMessage.Content.ReadAsAsync<List<CatalogText>>().Result;
                        Session.ToppingSizes = toppingSizes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetToppingSizes", string.Empty, string.Empty, ex);
            }

            return toppingSizes;
        }

        public static List<CatalogText> GetItemParts()
        {
            List<CatalogText> itemParts = new List<CatalogText>();

            if (Session.ItemParts != null && Session.ItemParts.Count > 0)
            {
                return Session.ItemParts;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetItemParts";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        itemParts = responseMessage.Content.ReadAsAsync<List<CatalogText>>().Result;
                        Session.ItemParts = itemParts;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetItemParts", string.Empty, string.Empty, ex);
            }

            return itemParts;
        }

        public static List<CatalogText> GetSpecialtyPizzaText()
        {
            List<CatalogText> specialityPizza = new List<CatalogText>();

            if (Session.SpecialtyPizzaText != null && Session.SpecialtyPizzaText.Count > 0)
            {
                return Session.SpecialtyPizzaText;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetSpecialtyPizzaText";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        specialityPizza = responseMessage.Content.ReadAsAsync<List<CatalogText>>().Result;
                        Session.SpecialtyPizzaText = specialityPizza;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetSpecialtyPizzaText", string.Empty, string.Empty, ex);
            }

            return specialityPizza;
        }

        public static List<CatalogToppings> GetToppings(string Menu_Code, string Menu_Option_Group_Code)
        {
            List<CatalogToppings> catalogToppings = new List<CatalogToppings>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetToppings";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code + "&Menu_Option_Group_Code=" + Menu_Option_Group_Code;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code + "&Menu_Option_Group_Code=" + Menu_Option_Group_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogToppings = responseMessage.Content.ReadAsAsync<List<CatalogToppings>>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetToppings", requestString, string.Empty, ex);
            }

            return catalogToppings;
        }

        public static List<CatalogOptionGroups> GetOptionGroups(string Menu_Code)
        {
            List<CatalogOptionGroups> catalogOptionGroups = new List<CatalogOptionGroups>();
            string requestString = "";
            try
            {
                if (Session.catalogOptionGroups != null && Session.catalogOptionGroups.Count > 0)
                {
                    List<CatalogOptionGroups> optionGroups = Session.catalogOptionGroups.FindAll(x => x.Menu_Code == Menu_Code);
                    if (optionGroups != null && optionGroups.Count > 0) return optionGroups;
                }

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetOptionGroups";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogOptionGroups = responseMessage.Content.ReadAsAsync<List<CatalogOptionGroups>>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetOptionGroups", requestString, string.Empty, ex);
            }

            return catalogOptionGroups;
        }

        public static List<CatalogSpecialtyPizzas> GetSpecialtyPizzas(string Size_Code, string Menu_Category_Code)
        {
            CatalogSpecialtyPizzasResponse catalogSpecialtyPizzasResponse = new CatalogSpecialtyPizzasResponse();
            List<CatalogSpecialtyPizzas> catalogSpecialtyPizzas = new List<CatalogSpecialtyPizzas>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetSpecialtyPizzas";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Size_Code=" + Size_Code + "&Menu_Category_Code=" + Menu_Category_Code;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Size_Code=" + Size_Code + "&Menu_Category_Code=" + Menu_Category_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogSpecialtyPizzasResponse = responseMessage.Content.ReadAsAsync<CatalogSpecialtyPizzasResponse>().Result;
                        catalogSpecialtyPizzas = catalogSpecialtyPizzasResponse.catalogSpecialtyPizzas;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetSpecialtyPizzas", requestString, string.Empty, ex);
            }

            return catalogSpecialtyPizzas;
        }

        public static List<CatalogMenuTypes> GetMenuTypes()
        {
            CatalogMenuTypesResponse catalogMenuTypesResponse = new CatalogMenuTypesResponse();
            List<CatalogMenuTypes> catalogMenuTypes = new List<CatalogMenuTypes>();
            string requestString = "";
            if (Session.MenuTypes != null && Session.MenuTypes.Count > 0)
            {
                return Session.MenuTypes;
            }

            try
            {

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuTypes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&WorkStation_ID=" + Session._WorkStationID;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&WorkStation_ID=" + Session._WorkStationID).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuTypesResponse = responseMessage.Content.ReadAsAsync<CatalogMenuTypesResponse>().Result;
                        catalogMenuTypes = catalogMenuTypesResponse.catalogMenuTypes;
                        Session.MenuTypes = catalogMenuTypes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuTypes", requestString, string.Empty, ex);
            }

            return catalogMenuTypes;
        }

        //public static List<CatalogOrderTypes> GetOrderTypes()
        //{
        //    CatalogOrderTypesResponse catalogOrderTypesResponse = new CatalogOrderTypesResponse();
        //    List<CatalogOrderTypes> catalogOrderTypes = new List<CatalogOrderTypes>();

        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetOrderTypes";

        //            httpClient.BaseAddress = new Uri(urlGetCatalog);
        //            httpClient.DefaultRequestHeaders.Accept.Clear();
        //            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&WorkStation_ID=" + Session._WorkStationID).Result;

        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                catalogOrderTypesResponse = responseMessage.Content.ReadAsAsync<CatalogOrderTypesResponse>().Result;
        //                catalogOrderTypes = catalogOrderTypesResponse.catalogOrderTypes;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return catalogOrderTypes;
        //}

        public static CatalogCartCaptions GetCartCaptions()
        {
            CatalogCartCaptions catalogCartCaptions = new CatalogCartCaptions();

            if (Session.CartCaptions != null)
            {
                return Session.CartCaptions;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCartCaptions";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogCartCaptions = responseMessage.Content.ReadAsAsync<CatalogCartCaptions>().Result;
                        Session.CartCaptions = catalogCartCaptions;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCartCaptions", string.Empty, string.Empty, ex);
            }

            return catalogCartCaptions;
        }

        public static bool GetPromptForDoublesStatus(List<CartItem> cartItems)
        {
            bool Response = false;
            String Uri = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "GetPromptForDoublesStatus";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cartItems), Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        Response = JsonConvert.DeserializeObject<bool>(jsonResponse);

                    }
                    else
                    {
                        Response = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetPromptForDoublesStatus", JsonConvert.SerializeObject(cartItems), string.Empty, ex);
            }

            return Response;

        }

        public static int ValidateLogin(CallType callType, EmployeeLoginRequest loginRequest)
        {

            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/EmployeeLogin";

            exception = new Exception();
            StatusCode = 0;
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    EmployeeLoginResponse employeeLoginResponse = JsonConvert.DeserializeObject<EmployeeLoginResponse>(jsonResponse);

                    if (employeeLoginResponse != null && employeeLoginResponse.Result != null && employeeLoginResponse.Result.LoginDetail != null)
                    {
                        StatusCode = Convert.ToInt32(employeeLoginResponse.Result.ResponseStatusCode);
                        StatusMsg = Convert.ToString(employeeLoginResponse.Result.ResponseStatus);

                        Session.CurrentEmployee = new EmployeeResult();
                        Session.CurrentEmployee = employeeLoginResponse.Result;

                    }
                    else if (employeeLoginResponse != null && employeeLoginResponse.Result != null && employeeLoginResponse.Result.ResponseStatus != null)
                    {
                        Session.LoginPassword = string.Empty;
                        StatusCode = 0;
                        StatusMsg = Convert.ToString(employeeLoginResponse.Result.ResponseStatus);

                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "ValidateLogin", JsonConvert.SerializeObject(loginRequest), string.Empty, ex);
            }

            //Session.LoginPassword = null;

            return StatusCode;
        }

        public static bool IsTechnicalSupport(CallType callType, EmployeeLoginRequest loginRequest)
        {
            bool isTechnicalSupport = false;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/IsTechnicalSupport";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    isTechnicalSupport = JsonConvert.DeserializeObject<bool>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "IsTechnicalSupport", JsonConvert.SerializeObject(loginRequest), string.Empty, ex);
            }

            return isTechnicalSupport;

        }

        public static bool IsAdministrator(CallType callType, EmployeeLoginRequest loginRequest)
        {
            bool isAdministrator = false;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/IsAdministrator";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    isAdministrator = JsonConvert.DeserializeObject<bool>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "IsAdministrator", JsonConvert.SerializeObject(loginRequest), string.Empty, ex);
            }

            return isAdministrator;

        }

        public static bool CheckSystemUser(CallType callType, EmployeeLoginRequest loginRequest)
        {
            bool checkSystemUser = false;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/CheckSystemUser";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    checkSystemUser = JsonConvert.DeserializeObject<bool>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "CheckSystemUser", JsonConvert.SerializeObject(loginRequest), string.Empty, ex);
            }

            return checkSystemUser;

        }

        //public static bool AuthMethodAllowed(CallType callType, CheckRecordRequest checkRecordRequest)
        //{
        //    bool authMethodAllowed = false;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/AuthMethodAllowed";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(checkRecordRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            authMethodAllowed = JsonConvert.DeserializeObject<bool>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return authMethodAllowed;

        //}

        //public static bool EmployeeSpecialAccess(CallType callType, EmployeeSpecialAccessRequest employeeSpecialAccessRequest)
        //{
        //    bool isEmployeeSpecialAccess = false;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/EmployeeSpecialAccess";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employeeSpecialAccessRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            isEmployeeSpecialAccess = JsonConvert.DeserializeObject<bool>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return isEmployeeSpecialAccess;

        //}

        public static int CheckPasswordExpiry(CallType callType, CheckEmployeeRequest checkPasswordExpiryRequest)
        {
            int checkPasswordExpiry = 0;
            EmployeeResponse employeeResponse = new EmployeeResponse();

            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/CheckPasswordExpiry";

            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(checkPasswordExpiryRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);

                    //checkPasswordExpiry = JsonConvert.DeserializeObject<int>(jsonResponse);
                    employeeResponse = JsonConvert.DeserializeObject<EmployeeResponse>(jsonResponse);
                    if (employeeResponse != null && employeeResponse.ResponseStatusCode == "1")
                    {
                        checkPasswordExpiry = employeeResponse.ResponseValue == "1" ? 1 : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "CheckPasswordExpiry", JsonConvert.SerializeObject(checkPasswordExpiryRequest), string.Empty, ex);
            }

            return checkPasswordExpiry;

        }

        public static int CheckEmployeePasswordReset(CallType callType, CheckBiometricDataRequest employeePasswordResetRequest)
        {
            int checkEmployeePasswordReset = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/CheckEmployeePasswordReset";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employeePasswordResetRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    checkEmployeePasswordReset = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "CheckEmployeePasswordReset", JsonConvert.SerializeObject(employeePasswordResetRequest), string.Empty, ex);
            }

            return checkEmployeePasswordReset;

        }

        //public static bool ClockInEmployeeSingleRole(CallType callType, CheckBiometricDataRequest employeePasswordResetRequest)

        //{
        //    bool clockInEmployeeSingleRole = false;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/ClockInEmployeeSingleRole";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employeePasswordResetRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            clockInEmployeeSingleRole = JsonConvert.DeserializeObject<bool>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return clockInEmployeeSingleRole;

        //}

        //public static ResetPasswordResponse ResetEmployeePassword(CallType callType, ResetPasswordRequest resetPasswordRequest)
        //{
        //    ResetPasswordResponse resetPasswordResponse = new ResetPasswordResponse();
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/ResetEmployeePassword";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(resetPasswordRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            resetPasswordResponse = JsonConvert.DeserializeObject<ResetPasswordResponse>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return resetPasswordResponse;
        //}

        public static CheckBiometricDataResponse CheckBiometricPunchData(CallType callType, CheckBiometricDataRequest checkBiometricDataRequest)
        {
            CheckBiometricDataResponse checkBiometricDataResponse = new CheckBiometricDataResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/CheckBiometricPunchData";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(checkBiometricDataRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    checkBiometricDataResponse = JsonConvert.DeserializeObject<CheckBiometricDataResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "CheckEmployeePasswordReset", JsonConvert.SerializeObject(checkBiometricDataRequest), string.Empty, ex);
            }

            return checkBiometricDataResponse;
        }

        public static EmployeeResponse UpdateEmployeePassword(CallType callType, EmployeeLoginRequest changePasswordRequest)
        {
            EmployeeResponse employeeResponse = new EmployeeResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/UpdateEmployeePassword";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(changePasswordRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    employeeResponse = JsonConvert.DeserializeObject<EmployeeResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "UpdateEmployeePassword", JsonConvert.SerializeObject(changePasswordRequest), string.Empty, ex);
            }

            return employeeResponse;
        }

        public static int TimeClockClockIn(CallType callType, TimeClockClockInOutRequest timeClockClockInOutRequest)
        {
            int timeClockClockIn = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/TimeClockClockIn";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockClockInOutRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    timeClockClockIn = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "TimeClockClockIn", JsonConvert.SerializeObject(timeClockClockInOutRequest), string.Empty, ex);
            }

            return timeClockClockIn;
        }

        //public static int TimeClockClockOut(CallType callType, TimeClockClockInOutRequest timeClockClockInOutRequest)
        //{
        //    int timeClockClockOut = 0;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/TimeClockClockOut";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockClockInOutRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            timeClockClockOut = JsonConvert.DeserializeObject<int>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return timeClockClockOut;
        //}

        public static int UpdateClockInAndOutDataBiometric(CallType callType, UpdateClockInAndOutDataBiometricRequest updateClockInAndOutDataBiometricRequest)
        {
            int updateClockInAndOutDataBiometric = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/UpdateClockInAndOutDataBiometric";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(updateClockInAndOutDataBiometricRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    updateClockInAndOutDataBiometric = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "UpdateClockInAndOutDataBiometric", JsonConvert.SerializeObject(updateClockInAndOutDataBiometricRequest), string.Empty, ex);
            }

            return updateClockInAndOutDataBiometric;

        }

        public static TimeClockConfirmationResponse TimeClockConfirmation(CallType callType, TimeClockConfirmationRequest timeClockConfirmationRequest)
        {
            TimeClockConfirmationResponse timeClockConfirmationResponse = new TimeClockConfirmationResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/TimeClockConfirmation";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockConfirmationRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    timeClockConfirmationResponse = JsonConvert.DeserializeObject<TimeClockConfirmationResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "TimeClockConfirmation", JsonConvert.SerializeObject(timeClockConfirmationRequest), string.Empty, ex);
            }

            return timeClockConfirmationResponse;

        }
        /*
        public static int PrintTimeClockConfirmation(CallType callType, PrintTimeClockConfirmationRequest printTimeClockConfirmationRequest)
        {
            int printTimeClockConfirmation = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/UpdateClockInAndOutDataBiometric";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(printTimeClockConfirmationRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    printTimeClockConfirmation = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return printTimeClockConfirmation;

        }
        */
        //public static string EmployeeMaxShiftNumber(CallType callType, CheckEmployeeRequest getEmployeeMaxShiftNumberRequest)
        //{
        //    string employeeMaxShiftNumber = string.Empty;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/EmployeeMaxShiftNumber";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(getEmployeeMaxShiftNumberRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            employeeMaxShiftNumber = JsonConvert.DeserializeObject<string>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return employeeMaxShiftNumber;

        //}

        public static int ValidateShiftOverlaps(CallType callType, ValidateShiftOverlapsRequest validateShiftOverlapsRequest)
        {
            int validateShiftOverlaps = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/ValidateShiftOverlaps";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(validateShiftOverlapsRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    validateShiftOverlaps = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "ValidateShiftOverlaps", JsonConvert.SerializeObject(validateShiftOverlapsRequest), string.Empty, ex);
            }

            return validateShiftOverlaps;
            
        }

        public static GetEmployeeInfoResponse EmployeeInfo(CallType callType, CheckEmployeeRequest getEmployeePositionRequest)
        {
            GetEmployeeInfoResponse getEmployeeInfoResponse = new GetEmployeeInfoResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/EmployeeInfo";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(getEmployeePositionRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getEmployeeInfoResponse = JsonConvert.DeserializeObject<GetEmployeeInfoResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "EmployeeInfo", JsonConvert.SerializeObject(getEmployeePositionRequest), string.Empty, ex);
            }

            return getEmployeeInfoResponse;

        }

        public static TimeClockGetEmpClockedInResponse TimeClockGetEmpClockedIn(CallType callType, CheckEmployeeRequest timeClockClockInOutRequest)
        {
            TimeClockGetEmpClockedInResponse timeClockGetEmpClockedInResponse = new TimeClockGetEmpClockedInResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/TimeClockGetEmpClockedIn";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockClockInOutRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    timeClockGetEmpClockedInResponse = JsonConvert.DeserializeObject<TimeClockGetEmpClockedInResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "TimeClockGetEmpClockedIn", JsonConvert.SerializeObject(timeClockClockInOutRequest), string.Empty, ex);
            }

            return timeClockGetEmpClockedInResponse;

        }

        public static string ClockInMagCard(CallType callType, ClockInMagCardRequest clockInMagCardRequest)
        {
            string clockInMagCard = string.Empty;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/ClockInMagCard";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(clockInMagCardRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    clockInMagCard = JsonConvert.DeserializeObject<string>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "ClockInMagCard", JsonConvert.SerializeObject(clockInMagCardRequest), string.Empty, ex);
            }

            return clockInMagCard;

        }

        public static int CheckRecord(CallType callType, CheckRecordRequest checkRecordRequest)
        {
            int checkRecord = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/CheckRecord";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(checkRecordRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    checkRecord = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "CheckRecord", JsonConvert.SerializeObject(checkRecordRequest), string.Empty, ex);
            }

            return checkRecord;

        }

        public static bool CheckEmployeeAlreadyLogin(CallType callType, CheckEmployeeRequest checkEmployeeAlreadyLoginRequest)
        {
            bool checkEmployeeAlreadyLogin = false;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/CheckEmployeeAlreadyLogin";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(checkEmployeeAlreadyLoginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    checkEmployeeAlreadyLogin = JsonConvert.DeserializeObject<bool>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "CheckEmployeeAlreadyLogin", JsonConvert.SerializeObject(checkEmployeeAlreadyLoginRequest), string.Empty, ex);
            }

            return checkEmployeeAlreadyLogin;

        }

        public static int EODHasBeenRan(CallType callType, EmployeeLoginRequest loginRequest)
        {
            int eodHasBeenRan = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/EODHasBeenRan";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    eodHasBeenRan = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "EODHasBeenRan", JsonConvert.SerializeObject(loginRequest), string.Empty, ex);
            }

            return eodHasBeenRan;

        }

        //public static string GetCurrentSystemDate(CallType callType, EmployeeLoginRequest loginRequest)
        //{
        //    string currentSystemDate = string.Empty;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/GetCurrentSystemDate";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            currentSystemDate = JsonConvert.DeserializeObject<string>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return currentSystemDate;

        //}

        public static string GetText(CallType callType, GetTextRequest getTextRequest)
        {
            string text = string.Empty;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetText";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(getTextRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    text = JsonConvert.DeserializeObject<string>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetText", JsonConvert.SerializeObject(getTextRequest), string.Empty, ex);
            }

            return text;

        }

        public static int ReturnDriver(CallType callType, EmployeeLoginRequest loginRequest)
        {
            int returnDriver = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/ReturnDriver";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    returnDriver = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "ReturnDriver", JsonConvert.SerializeObject(loginRequest), string.Empty, ex);
            }

            return returnDriver;

        }

        //public static int InsertAuditTrail(CallType callType, InsertAuditTrailRequest insertAuditTrailRequest)
        //{
        //    int insertAuditTrail = 0;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/InsertAuditTrail";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(insertAuditTrailRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            insertAuditTrail = JsonConvert.DeserializeObject<int>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return insertAuditTrail;

        //}

        public static int GetTimeClockCurrentDateShiftNumber(CallType callType, CheckEmployeeRequest timeClockClockInOutRequest)
        {
            int currentShiftnumber = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetTimeClockCurrentDateShiftNumber";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockClockInOutRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    currentShiftnumber = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetTimeClockCurrentDateShiftNumber", JsonConvert.SerializeObject(timeClockClockInOutRequest), string.Empty, ex);
            }

            return currentShiftnumber;

        }

        public static int GetTimeClockCurrentPositionShiftNumber(CallType callType, CheckEmployeeRequest timeClockClockInOutRequest)
        {
            int currentShiftnumber = 0;
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetTimeClockCurrentPositionShiftNumber";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockClockInOutRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    currentShiftnumber = JsonConvert.DeserializeObject<int>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetTimeClockCurrentPositionShiftNumber", JsonConvert.SerializeObject(timeClockClockInOutRequest), string.Empty, ex);
            }

            return currentShiftnumber;

        }

        public static GetWorkstationDeviceOptionsResponse GetWorkstationDeviceOptions(CallType callType, TimeClockConfirmationRequest timeClockConfirmationRequest)
        {
            GetWorkstationDeviceOptionsResponse getWorkstationDeviceOptionsResponse = new GetWorkstationDeviceOptionsResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetWorkstationDeviceOptions";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockConfirmationRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getWorkstationDeviceOptionsResponse = JsonConvert.DeserializeObject<GetWorkstationDeviceOptionsResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetWorkstationDeviceOptions", JsonConvert.SerializeObject(timeClockConfirmationRequest), string.Empty, ex);
            }

            return getWorkstationDeviceOptionsResponse;

        }

        public static GetPrinterSettingsResponse GetPrinterSettings(CallType callType, TimeClockConfirmationRequest timeClockConfirmationRequest)
        {
            GetPrinterSettingsResponse getPrinterSettingsResponse = new GetPrinterSettingsResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetPrinterSettings";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockConfirmationRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getPrinterSettingsResponse = JsonConvert.DeserializeObject<GetPrinterSettingsResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetPrinterSettings", JsonConvert.SerializeObject(timeClockConfirmationRequest), string.Empty, ex);
            }

            return getPrinterSettingsResponse;

        }

        //public static GetOrderTypeOptionsResponse GetOrderTypeOptions(CallType callType, TimeClockConfirmationRequest timeClockConfirmationRequest)
        //{
        //    GetOrderTypeOptionsResponse getOrderTypeOptionsResponse = new GetOrderTypeOptionsResponse();
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/GetOrderTypeOptions";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockConfirmationRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            getOrderTypeOptionsResponse = JsonConvert.DeserializeObject<GetOrderTypeOptionsResponse>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return getOrderTypeOptionsResponse;

        //}

        public static GetEmployeeDetailsResponse GetEmployeeDetails(CallType callType, EmployeeLoginRequest loginRequest)
        {
            GetEmployeeDetailsResponse getEmployeeDetailsResponse = new GetEmployeeDetailsResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetEmployeeDetails";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getEmployeeDetailsResponse = JsonConvert.DeserializeObject<GetEmployeeDetailsResponse>(jsonResponse);

                    //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
                    //{
                    //    Session.CurrentEmployee = new EmployeeResult();
                    //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetEmployeeDetails", JsonConvert.SerializeObject(loginRequest), string.Empty, ex);
            }

            return getEmployeeDetailsResponse;

        }

        public static GetEmployeeCodeResponse GetEmployeeCode(CallType callType, GetEmployeeCodeRequest employeeCodeRequest)
        {
            GetEmployeeCodeResponse getEmployeeCodeResponse = new GetEmployeeCodeResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetEmployeeCode";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employeeCodeRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getEmployeeCodeResponse = JsonConvert.DeserializeObject<GetEmployeeCodeResponse>(jsonResponse);

                    //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
                    //{
                    //    Session.CurrentEmployee = new EmployeeResult();
                    //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetEmployeeCode", JsonConvert.SerializeObject(employeeCodeRequest), string.Empty, ex);
            }

            return getEmployeeCodeResponse;

        }

        //public static string GetServerDateTime(CallType callType)
        //{
        //    string text = string.Empty;
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/GetServerDateTime";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.GET)
        //        {
        //            string jsonResponse = Get(Uri);
        //            text = JsonConvert.DeserializeObject<string>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return text;

        //}

        public static GetEmployeePositionResponse GetEmployeePositions(CallType callType, EmployeeLoginRequest getEmployeePositionRequest)
        {
            GetEmployeePositionResponse getEmployeePositionResponse = new GetEmployeePositionResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
            Uri += "/GetEmployeePositions";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(getEmployeePositionRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getEmployeePositionResponse = JsonConvert.DeserializeObject<GetEmployeePositionResponse>(jsonResponse);

                    //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
                    //{
                    //    Session.CurrentEmployee = new EmployeeResult();
                    //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetEmployeePositions", JsonConvert.SerializeObject(getEmployeePositionRequest), string.Empty, ex);
            }

            return getEmployeePositionResponse;
        }

        public static GetCustomerProfileResponse GetCustomerProfile(CallType callType, GetCustomerProfileRequest getCustomerProfileRequest)
        {
            GetCustomerProfileResponse getCustomerProfileResponse = new GetCustomerProfileResponse();

            string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
            Uri += "/GetCustomerProfile";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(getCustomerProfileRequest), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getCustomerProfileResponse = JsonConvert.DeserializeObject<GetCustomerProfileResponse>(jsonResponse);

                    //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
                    //{
                    //    Session.CurrentEmployee = new EmployeeResult();
                    //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetCustomerProfile", JsonConvert.SerializeObject(getCustomerProfileRequest), string.Empty, ex);
            }

            return getCustomerProfileResponse;
        }

        public static CustomerLookUpResponse CustomerLookUp(CallType callType, CustomerLookUpRequest requestModel)
        {
            CustomerLookUpResponse customerLookUpResponse = new CustomerLookUpResponse();

            string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
            Uri += "/CustomerLookUp";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    customerLookUpResponse = JsonConvert.DeserializeObject<CustomerLookUpResponse>(jsonResponse);

                    //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
                    //{
                    //    Session.CurrentEmployee = new EmployeeResult();
                    //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "CustomerLookUp", JsonConvert.SerializeObject(requestModel), string.Empty, ex);
            }

            return customerLookUpResponse;
        }

        //public static CustomerLookUpResponse CustomerLookUpGlobalList(CallType callType, CustomerLookUpRequest requestModel)
        //{
        //    CustomerLookUpResponse customerLookUpResponse = new CustomerLookUpResponse();

        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
        //    Uri += "/CustomerLookUpGlobalList";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            customerLookUpResponse = JsonConvert.DeserializeObject<CustomerLookUpResponse>(jsonResponse);

        //            //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
        //            //{
        //            //    Session.CurrentEmployee = new EmployeeResult();
        //            //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //    }

        //    return customerLookUpResponse;
        //}

        public static StreetLookUpResponse StreetLookUp(CallType callType, StreetLookUpRequest requestModel)
        {
            StreetLookUpResponse streetLookUpResponse = new StreetLookUpResponse();
            exception = new Exception();
            try
            {
                if (requestModel.StreetName == "%%" && Session.StreetLookUpResponseAll != null && Session.StreetLookUpResponseAll.Result != null && Session.StreetLookUpResponseAll.Result.Streets !=null && Session.StreetLookUpResponseAll.Result.Streets.Count >0 )
                {
                    return Session.StreetLookUpResponseAll;
                }

                string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
                Uri += "/StreetLookUp";


                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    streetLookUpResponse = JsonConvert.DeserializeObject<StreetLookUpResponse>(jsonResponse);

                    //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
                    //{
                    //    Session.CurrentEmployee = new EmployeeResult();
                    //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "StreetLookUp", JsonConvert.SerializeObject(requestModel), string.Empty, ex);
            }

            return streetLookUpResponse;
        }

        public static GetAllCitiesResponse GetAllCities(CallType callType)
        {
            GetAllCitiesResponse citiesResponse = new GetAllCitiesResponse();

            string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
            Uri += "/GetAllCities";

            exception = new Exception();
            try
            {
                if (callType == CallType.GET)
                {
                    //StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
                    //string jsonResponse = Post(Uri, stringContent);
                    string jsonResponse = Get(Uri);
                    citiesResponse = JsonConvert.DeserializeObject<GetAllCitiesResponse>(jsonResponse);

                    //if (getEmployeeDetailsResponse != null && getEmployeeDetailsResponse.Result != null && getEmployeeDetailsResponse.Result.EmployeeDetail != null)
                    //{
                    //    Session.CurrentEmployee = new EmployeeResult();
                    //    Session.CurrentEmployee.LoginDetail.EmployeeCode = getEmployeeDetailsResponse.Result.EmployeeDetail.;
                    //}
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetAllCities", string.Empty, string.Empty, ex);
            }

            return citiesResponse;
        }


        //public static async void POSTLoginAsync(string Uri, LoginRequest loginRequest)
        //{
        //    try
        //    {
        //        APIClient = new HttpClient();

        //        Uri += "/EmployeeLogin";


        //        APIClient.BaseAddress = new Uri(Uri);
        //        APIClient.DefaultRequestHeaders.Accept.Clear();
        //        APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


        //        HttpResponseMessage response = await APIClient.PostAsJsonAsync("", loginRequest);
        //        response.EnsureSuccessStatusCode();

        //        EmployeeResult employeeResult  = await response.Content.ReadAsAsync<EmployeeResult>();

        //        StatusCode = Convert.ToInt32(employeeResult.ResponseStatusCode);
        //        StatusMsg = Convert.ToString(employeeResult.ResponseStatus);
        //        LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(Convert.ToString(employeeResult.Response));

        //        Employee employee = new Employee();
        //        employee.LocationCode = loginResponse.LocationCode;
        //        employee.EmployeeCode = loginResponse.EmployeeCode;
        //        employee.DateShiftNumber = loginResponse.DateShiftNumber;
        //        employee.LastName = loginResponse.LastName;
        //        employee.FirstName = loginResponse.FirstName;
        //        employee.RightHanded = loginResponse.RightHanded;
        //        employee.LanguageCode = loginResponse.LanguageCode;
        //        employee.Password = loginRequest.Password;
        //        Session.CurrentEmployee = employee;


        //    }
        //    catch (Exception ex)
        //    {
        //        StatusCode = 0;
        //        exception = ex;
        //    }
        //}

        public static string Post(string uri, StringContent stringContent)
        {
            string result = string.Empty;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        result = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "Post", Convert.ToString(stringContent), string.Empty, ex);
            }
            return result;
        }

        public static string Get(string uri)
        {
            string result = string.Empty;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(uri).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        result = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "Get", uri, string.Empty, ex);
            }
            return result;
        }


        public static void ReceiptPrinting(PrintOrderReceiptRequest printOrderReceiptRequest)
        {

            String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "InsertOrder";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(printOrderReceiptRequest), Encoding.UTF8, "application/json");

            PrintOrderReceiptResponse printOrderReceiptResponse = new PrintOrderReceiptResponse();

            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        printOrderReceiptResponse = JsonConvert.DeserializeObject<PrintOrderReceiptResponse>(jsonResponse);
                        if (printOrderReceiptResponse.Result.ResponseStatus.ToLower() == "success")
                        {

                            //Session.currentOrderResponse = orderResponse.orderResponse;
                        }
                        else
                        {
                            Logger.Trace("Info", response.ToString(), null, false);
                        }
                    }
                    else
                    {
                        Logger.Trace("Info", response.ToString(), null, false);
                    }

                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "ReceiptPrinting", JsonConvert.SerializeObject(printOrderReceiptRequest), string.Empty, ex);
            }

        }

        public static bool PushOrder(OrderRequest orderRequest)
        {
            bool pushOrder = false;
            Session.responseMsg = String.Empty;
            String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "PushOrder";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

            OrderResponse orderResponse = new OrderResponse();
            string jsonResponse = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        orderResponse = JsonConvert.DeserializeObject<OrderResponse>(jsonResponse);
                        if (orderResponse.ResponseStatus.ToLower() == "success")
                        {
                            pushOrder = true;
                            Session.currentOrderResponse = orderResponse.orderResponseData;
                        }
                        else
                        {
                            Session.currentOrderResponse = null;
                            Session.responseMsg = orderResponse.ResponseStatus.ToString();
                            Logger.Trace("Info", orderResponse.ResponseStatus.ToString(), null, false);
                        }
                    }
                    else
                    {
                        Session.responseMsg = response.ToString();
                        Logger.Trace("Info", response.ToString(), null, false);
                    }

                }


            }
            catch (Exception ex)
            {
                Session.responseMsg = ex.Message.ToString();
                pushOrder = false;
                Logging("ERROR", "PushOrder", JsonConvert.SerializeObject(orderRequest), jsonResponse, ex);
            }
            return pushOrder;
        }

        public static List<CatalogUpsellMenu> GetUpsellMenu(string cart_item)
        {
            CatalogUpsellMenuResponse catalogUpsellMenuResponse = new CatalogUpsellMenuResponse();
            List<CatalogUpsellMenu> catalogUpsellMenu = new List<CatalogUpsellMenu>();
            string requestString = "";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetUpsellMenu";
                    requestString = urlGetCatalog + "?Cart_items=" + cart_item;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Cart_items=" + cart_item).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogUpsellMenuResponse = responseMessage.Content.ReadAsAsync<CatalogUpsellMenuResponse>().Result;
                        catalogUpsellMenu = catalogUpsellMenuResponse.catalogUpsellMenu;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetUpsellMenu", requestString, string.Empty, ex);
            }

            return catalogUpsellMenu;
        }

        public static string GetCatalogText(int Key_Field)
        {

            if (Session.catalogTexts != null && Session.catalogTexts.Count > 0)
            {
                CatalogText text = Session.catalogTexts.Find(x => x.Key_Field == Convert.ToString(Key_Field));
                if (text != null) return text.Modified_Text;
            }
            string requestString = "";
            CatalogText catalogText = new CatalogText();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetText";
                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Key_Field=" + Key_Field;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Key_Field=" + Key_Field).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogText = responseMessage.Content.ReadAsAsync<CatalogText>().Result;

                        if (Session.catalogTexts == null) Session.catalogTexts = new List<CatalogText>();
                        Session.catalogTexts.Add(catalogText);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCatalogText", requestString, string.Empty, ex);
            }
            return catalogText.Modified_Text;

        }

        public static List<CatalogUpsellMenuCoupon> GetUpsellMenuCoupon(string Menu_Code, string Size_Code, string Cart_items)
        {
            CatalogUpsellMenuCouponResponse catalogUpsellMenuCouponResponse = new CatalogUpsellMenuCouponResponse();
            List<CatalogUpsellMenuCoupon> catalogUpsellMenuCoupons = new List<CatalogUpsellMenuCoupon>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetUpsellMenuCoupon";
                    requestString = urlGetCatalog + "?Menu_Code=" + Menu_Code + "&Size_Code=" + Size_Code + "&Cart_items=" + Cart_items;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Menu_Code=" + Menu_Code + "&Size_Code=" + Size_Code + "&Cart_items=" + Cart_items).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogUpsellMenuCouponResponse = responseMessage.Content.ReadAsAsync<CatalogUpsellMenuCouponResponse>().Result;
                        catalogUpsellMenuCoupons = catalogUpsellMenuCouponResponse.catalogUpsellMenuCoupon;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetUpsellMenuCoupon", requestString, string.Empty, ex);
            }

            return catalogUpsellMenuCoupons;
        }

        public static List<CatalogReasons> GetCatalogReasons(string location_Code, int language_Code, int? reason_Group_Code)
        {
            List<CatalogReasons> lstcatalogReason = new List<CatalogReasons>();
            string requestString = "";
            CatalogReasonsResponse catalogReasonsResponse = new CatalogReasonsResponse();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetReasons";
                    requestString = urlGetCatalog + "?Location_Code=" + location_Code + "&Language_Code=" + language_Code + "&Reason_Group_Code=" + reason_Group_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + location_Code + "&Language_Code=" + language_Code + "&Reason_Group_Code=" + reason_Group_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogReasonsResponse = responseMessage.Content.ReadAsAsync<CatalogReasonsResponse>().Result;
                        lstcatalogReason = catalogReasonsResponse.catalogReasons;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCatalogReasons", requestString, string.Empty, ex);
            }

            return lstcatalogReason;
        }

        public static int GetEstimatedDeliveryTime()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetEstimatedDeliveryTime";
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<int>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetEstimatedDeliveryTime", string.Empty, string.Empty, ex);
            }

            return 0;
        }

        public static int GetEstimatedCarryOutTime()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetEstimatedCarryOutTime";
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<int>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetEstimatedCarryOutTime", string.Empty, string.Empty, ex);
            }

            return 0;
        }

        public static SpecialInformation GetSepcialInfo(string SystemDate, string LocationCode)
        {
            SpecialInfoResponse specialInfoResponse = new SpecialInfoResponse();
            SpecialInformation specialInformation = new SpecialInformation();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetSpecialInformation";
                    requestString = urlGetCatalog + "?SystemDate=" + SystemDate + "&LocationCode=" + LocationCode;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?SystemDate=" + SystemDate + "&LocationCode=" + LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        specialInfoResponse = responseMessage.Content.ReadAsAsync<SpecialInfoResponse>().Result;
                        specialInformation = specialInfoResponse.specialInformation;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetSepcialInfo", requestString, string.Empty, ex);
            }

            return specialInformation;
        }

        public static List<CatlogToppingDescriptonCode> GetCatalogToppingDescrptionCode(string LocationCode)
        {
            CatlogToppingDescriptonCodeResponse catlogToppingDescriptonCodeResponse = new CatlogToppingDescriptonCodeResponse();
            List<CatlogToppingDescriptonCode> lstcatalogTopping = new List<CatlogToppingDescriptonCode>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCatlogToppingDescriptonCode";
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?LocationCode=" + LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catlogToppingDescriptonCodeResponse = responseMessage.Content.ReadAsAsync<CatlogToppingDescriptonCodeResponse>().Result;
                        lstcatalogTopping = catlogToppingDescriptonCodeResponse.catlogToppingDescriptonCode;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuCategories", string.Empty, string.Empty, ex);
            }

            return lstcatalogTopping;
        }

        public static CatalogDelayedOrder CheckCatalogDelayOrders(string LocationCode, string DelayedOrder)
        {
            CatalogDelayedOrderResponse catalogDelayedOrderResponse = new CatalogDelayedOrderResponse();
            CatalogDelayedOrder catalogDelayedOrder = new CatalogDelayedOrder();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "CheckDelayedOrdersOrderDate";
                    requestString = urlGetCatalog + "?LocationCode=" + LocationCode + "&DelayedDate=" + DelayedOrder;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?LocationCode=" + LocationCode + "&DelayedDate=" + DelayedOrder).Result;


                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogDelayedOrderResponse = responseMessage.Content.ReadAsAsync<CatalogDelayedOrderResponse>().Result;
                        if (catalogDelayedOrderResponse != null)
                        {
                            catalogDelayedOrder = catalogDelayedOrderResponse.DelayedOrder;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "CheckCatalogDelayOrders", requestString, string.Empty, ex);
            }

            return catalogDelayedOrder;
        }

        public static bool AbandonOrder(OrderResponseData orderResponseData)
        {
            bool AbandonOrder = false;
            String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "AbandonOrder";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(orderResponseData), Encoding.UTF8, "application/json");

            AbandonOrderResponse abandonOrderResponse = new AbandonOrderResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        abandonOrderResponse = JsonConvert.DeserializeObject<AbandonOrderResponse>(jsonResponse);
                        if (abandonOrderResponse.ResponseStatusCode == "1")
                            AbandonOrder = true;
                    }
                }
            }
            catch (Exception ex)
            {
                AbandonOrder = false;
                Logging("ERROR", "AbandonOrder", JsonConvert.SerializeObject(orderResponseData), string.Empty, ex);
            }
            return AbandonOrder;
        }

        public static DateTime GetStoreClosingDateTime(string LocationCode, string DatetoCheck)
        {
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetStoreClosingTime";
                    requestString = urlGetCatalog + "?LocationCode=" + LocationCode + "&DateToCheck=" + DatetoCheck;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?LocationCode=" + LocationCode + "&DateToCheck=" + DatetoCheck).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<DateTime>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetStoreClosingDateTime", requestString, string.Empty, ex);
            }

            return Convert.ToDateTime("01-01-0001");
        }

        public static GetCallerIDLinesResponse GetCallerIDLines(CallType callType, GetAllCustomersRequest requestModel)
        {
            GetCallerIDLinesResponse getCallerIDLinesResponse = new GetCallerIDLinesResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
            Uri += "/GetCallerIDLines";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getCallerIDLinesResponse = JsonConvert.DeserializeObject<GetCallerIDLinesResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetCallerIDLines", JsonConvert.SerializeObject(requestModel), string.Empty, ex);
            }

            return getCallerIDLinesResponse;
        }

        public static GetCallerIDLogResponse GetCallerIDLog(CallType callType, GetCallerIDLogRequest requestModel)
        {
            GetCallerIDLogResponse getCallerIDLogResponse = new GetCallerIDLogResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
            Uri += "/GetCallerIDLog";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    getCallerIDLogResponse = JsonConvert.DeserializeObject<GetCallerIDLogResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "GetCallerIDLogs", JsonConvert.SerializeObject(requestModel), string.Empty, ex);
            }

            return getCallerIDLogResponse;
        }

        public static SaveCustomerResponse DeleteCallerIDLog(CallType callType, GetAllCustomersRequest requestModel)
        {
            SaveCustomerResponse saveCustomerResponse = new SaveCustomerResponse();
            string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
            Uri += "/DeleteCallerIDLog";

            exception = new Exception();
            try
            {
                if (callType == CallType.POST)
                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
                    string jsonResponse = Post(Uri, stringContent);
                    saveCustomerResponse = JsonConvert.DeserializeObject<SaveCustomerResponse>(jsonResponse);
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                Logging("ERROR", "DeleteCallerIDLog", JsonConvert.SerializeObject(requestModel), string.Empty, ex);
            }

            return saveCustomerResponse;
        }

        //public static SaveCustomerResponse DeleteCall(CallType callType, DeleteCallRequest requestModel)
        //{
        //    SaveCustomerResponse saveCustomerResponse = new SaveCustomerResponse();
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
        //    Uri += "/DeleteCall";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            saveCustomerResponse = JsonConvert.DeserializeObject<SaveCustomerResponse>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return saveCustomerResponse;
        //}

        public static List<CatalogCoupons> GetOrderCoupons(string Location_Code, bool Entire_Order, string Order_Type_Code, string Cart_items)
        {
            CatalogCouponsResponse catalogCouponsResponse = new CatalogCouponsResponse();
            List<CatalogCoupons> catalogCoupons = new List<CatalogCoupons>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCoupons";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCouponCatalog + "?Location_Code=" + Location_Code + "&Entire_Order=" + Entire_Order + "&Order_Type_Code=" + Order_Type_Code + "&Cart_items=" + Cart_items;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Location_Code + "&Entire_Order=" + Entire_Order + "&Order_Type_Code=" + Order_Type_Code + "&Cart_items=" + Cart_items).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogCouponsResponse = responseMessage.Content.ReadAsAsync<CatalogCouponsResponse>().Result;
                        catalogCoupons = catalogCouponsResponse.catalogCoupons;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetOrderCoupons", requestString, string.Empty, ex);
            }

            return catalogCoupons;
        }

        public static string GetSpecialtyPizzaDescription(string Specialty_Code)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetSpecialtyPizzaDescription";
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?LocationCode=" + Session._LocationCode + "&Specialty_Code=" + Specialty_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetSpecialtyPizzaDescription", Specialty_Code, string.Empty, ex);
            }
            return string.Empty;
        }

        public static List<CatalogPOSComboMealItems> GetComboMealItems(string Combo_Menu_Code, string Combo_Size_Code)
        {
            CatalogPOSComboMealItemsResponse catalogPOSComboMealItemsResponse = new CatalogPOSComboMealItemsResponse();
            List<CatalogPOSComboMealItems> catalogPOSComboMenuItems = new List<CatalogPOSComboMealItems>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "POSComboMealItems";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Combo_Menu_Code + "&Size_Code=" + Combo_Size_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Combo_Menu_Code + "&Size_Code=" + Combo_Size_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogPOSComboMealItemsResponse = responseMessage.Content.ReadAsAsync<CatalogPOSComboMealItemsResponse>().Result;
                        catalogPOSComboMenuItems = catalogPOSComboMealItemsResponse.catalogPOSComboMealItems;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetComboMealItems", requestString, string.Empty, ex);
            }

            return catalogPOSComboMenuItems;
        }

        public static List<CatalogPOSComboMealItemSizes> GetComboMealItemSizes(string Combo_Menu_Code, string Combo_Size_Code)
        {
            CatalogPOSComboMealItemSizesResponse catalogPOSComboMealItemSizesResponse = new CatalogPOSComboMealItemSizesResponse();
            List<CatalogPOSComboMealItemSizes> catalogPOSComboMenuItemSizes = new List<CatalogPOSComboMealItemSizes>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "POSComboMealItemSizes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Combo_Menu_Code + "&Size_Code=" + Combo_Size_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Combo_Menu_Code + "&Size_Code=" + Combo_Size_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogPOSComboMealItemSizesResponse = responseMessage.Content.ReadAsAsync<CatalogPOSComboMealItemSizesResponse>().Result;
                        catalogPOSComboMenuItemSizes = catalogPOSComboMealItemSizesResponse.catalogPOSComboMealItemSizes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetComboMealItemSizes", requestString, string.Empty, ex);
            }
            return catalogPOSComboMenuItemSizes;
        }

        public static List<CatalogPOSComboMealItemsForButtons> GetComboMealItemsForButtons(string Combo_Menu_Code, string Combo_Size_Code)
        {
            CatalogPOSComboMealItemsForButtonsResponse catalogPOSComboMealItemsForButtonsResponse = new CatalogPOSComboMealItemsForButtonsResponse();
            List<CatalogPOSComboMealItemsForButtons> catalogPOSComboMealItemsForButtons = new List<CatalogPOSComboMealItemsForButtons>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "POSComboMealItemsForButtons";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Combo_Menu_Code + "&Size_Code=" + Combo_Size_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Combo_Menu_Code + "&Size_Code=" + Combo_Size_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogPOSComboMealItemsForButtonsResponse = responseMessage.Content.ReadAsAsync<CatalogPOSComboMealItemsForButtonsResponse>().Result;
                        catalogPOSComboMealItemsForButtons = catalogPOSComboMealItemsForButtonsResponse.catalogPOSComboMealItemsForButtons;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetComboMealItemsForButtons", requestString, string.Empty, ex);
            }
            return catalogPOSComboMealItemsForButtons;
        }

        public static OriginalOrderInfo LoadOriginalOrderInfoRemake(string Location_code, DateTime Order_date, long Order_Number, bool Order_history)
        {
            OriginalOrderInfo originalOrder = new OriginalOrderInfo();
            OrderInfoResponse orderInfoResponse = new OrderInfoResponse();
            string requestString = "";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetorderInfo = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "GetOrderInfoRemake";
                    requestString = urlGetorderInfo + "?Location_Code=" + Location_code + "&Order_Date=" + Order_date + "&Order_number=" + Order_Number + "&History" + Order_history;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetorderInfo + "?Location_Code=" + Location_code + "&Order_Date=" + Order_date + "&Order_number=" + Order_Number + "&History" + Order_history).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {

                        orderInfoResponse = responseMessage.Content.ReadAsAsync<OrderInfoResponse>().Result;

                        if (orderInfoResponse.ResponseStatusCode == "1")
                        {
                            if (orderInfoResponse.cartHeader != null)
                            {
                                originalOrder.cartHeader = orderInfoResponse.cartHeader;
                            }
                            if (orderInfoResponse.cartItems != null)
                            {
                                originalOrder.cartItems = orderInfoResponse.cartItems;
                            }
                            if (orderInfoResponse.itemAttributeGroups != null)
                            {
                                originalOrder.itemAttributeGroups = orderInfoResponse.itemAttributeGroups;
                            }
                            if (orderInfoResponse.itemAttributes != null)
                            {
                                originalOrder.itemAttributes = orderInfoResponse.itemAttributes;
                            }
                            if (orderInfoResponse.itemOptions != null)
                            {
                                originalOrder.itemOptions = orderInfoResponse.itemOptions;
                            }
                            if (orderInfoResponse.itemReasons != null)
                            {
                                originalOrder.itemReasons = orderInfoResponse.itemReasons;
                            }
                            if (orderInfoResponse.itemSpecialtyPizzas != null)
                            {
                                originalOrder.itemSpecialtyPizzas = orderInfoResponse.itemSpecialtyPizzas;
                            }
                            if (orderInfoResponse.itemUDTs != null)
                            {
                                originalOrder.itemUDTs = orderInfoResponse.itemUDTs;
                            }
                            if (orderInfoResponse.orderCreditCards != null)
                            {
                                originalOrder.orderCreditCards = orderInfoResponse.orderCreditCards;
                            }
                            if (orderInfoResponse.itemCombos != null)
                            {
                                originalOrder.itemCombos = orderInfoResponse.itemCombos;
                            }
                            if (orderInfoResponse.orderPayments != null)
                            {
                                originalOrder.orderPayments = orderInfoResponse.orderPayments;
                            }
                            if (orderInfoResponse.orderReasons != null)
                            {
                                originalOrder.orderReasons = orderInfoResponse.orderReasons;
                            }
                            if (orderInfoResponse.orderUDT != null)
                            {
                                originalOrder.orderUDT = orderInfoResponse.orderUDT;

                            }
                            if (orderInfoResponse.itemOptionGroups != null)
                            {
                                originalOrder.itemOptionGroups = orderInfoResponse.itemOptionGroups;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "LoadOriginalOrderInfoRemake", requestString, string.Empty, ex);
            }

            return originalOrder;
        }

        public static OriginalOrderInfo LoadOriginalOrderInfo(string Location_code, DateTime Order_date, long Order_Number, bool Order_history)
        {
            OriginalOrderInfo originalOrder = new OriginalOrderInfo();
            OrderInfoResponse orderInfoResponse = new OrderInfoResponse();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetorderInfo = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "GetOrderInfo";
                    requestString = urlGetorderInfo + "?Location_Code=" + Location_code + "&Order_Date=" + Order_date.ToString("yyyy-MM-dd") + "&Order_number=" + Order_Number + "&History=" + Order_history;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetorderInfo + "?Location_Code=" + Location_code + "&Order_Date=" + Order_date.ToString("yyyy-MM-dd") + "&Order_number=" + Order_Number + "&History=" + Order_history).Result;



                    if (responseMessage.IsSuccessStatusCode)
                    {

                        orderInfoResponse = responseMessage.Content.ReadAsAsync<OrderInfoResponse>().Result;

                        if (orderInfoResponse.ResponseStatusCode == "1")
                        {
                            if (orderInfoResponse.cartHeader != null)
                            {
                                originalOrder.cartHeader = orderInfoResponse.cartHeader;
                                if (originalOrder.cartHeader.Driver_ID == null)
                                {
                                    originalOrder.cartHeader.Driver_ID = "";
                                }
                                originalOrder.cartHeader.Total = originalOrder.cartHeader.Final_Total;
                            }
                            if (orderInfoResponse.cartItems != null)
                            {
                                originalOrder.cartItems = orderInfoResponse.cartItems;
                            }
                            if (orderInfoResponse.itemAttributeGroups != null)
                            {
                                originalOrder.itemAttributeGroups = orderInfoResponse.itemAttributeGroups;
                            }
                            if (orderInfoResponse.itemAttributes != null)
                            {
                                originalOrder.itemAttributes = orderInfoResponse.itemAttributes;
                            }
                            if (orderInfoResponse.itemOptions != null)
                            {
                                originalOrder.itemOptions = orderInfoResponse.itemOptions;
                            }
                            if (orderInfoResponse.itemReasons != null)
                            {
                                originalOrder.itemReasons = orderInfoResponse.itemReasons;
                            }
                            if (orderInfoResponse.itemSpecialtyPizzas != null)
                            {
                                originalOrder.itemSpecialtyPizzas = orderInfoResponse.itemSpecialtyPizzas;
                            }
                            if (orderInfoResponse.itemUDTs != null)
                            {
                                originalOrder.itemUDTs = orderInfoResponse.itemUDTs;
                            }
                            if (orderInfoResponse.orderCreditCards != null)
                            {
                                originalOrder.orderCreditCards = orderInfoResponse.orderCreditCards;

                            }
                            if (orderInfoResponse.itemCombos != null)
                            {
                                originalOrder.itemCombos = orderInfoResponse.itemCombos;
                            }
                            if (orderInfoResponse.orderPayments != null)
                            {
                                originalOrder.orderPayments = orderInfoResponse.orderPayments;

                                foreach(OrderPayment orderPayment in originalOrder.orderPayments)
                                {
                                    orderPayment.TransactionTime = new DateTime(1899, 12, 30);
                                }
                            }
                            if (orderInfoResponse.orderReasons != null)
                            {
                                originalOrder.orderReasons = orderInfoResponse.orderReasons;
                            }
                            if (orderInfoResponse.orderUDT != null)
                            {
                                originalOrder.orderUDT = orderInfoResponse.orderUDT;
                                if (originalOrder.orderUDT.Added_By == null)
                                {
                                    originalOrder.orderUDT.Added_By = originalOrder.cartHeader.Added_By;
                                }

                            }
                            if (orderInfoResponse.itemOptionGroups != null)
                            {
                                originalOrder.itemOptionGroups = orderInfoResponse.itemOptionGroups;

                            }
                            if (orderInfoResponse.orderAdditionalCharges != null)
                            {
                                originalOrder.orderAdditionalCharges = orderInfoResponse.orderAdditionalCharges;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "LoadOriginalOrderInfo", requestString, string.Empty, ex);
            }

            return originalOrder;
        }


        //public static ResponsePayment PaymentRequestforCash(PaymentRequest paymentRequest)
        //{
        //    HttpClient client1 = new HttpClient();
        //    ResponsePayment responsePayment = new ResponsePayment();
        //    try
        //    {
        //        String Uri = Convert.ToString(ConfigurationManager.AppSettings["PaymentAPIUrl"]) + "GetOrderPayment";

        //        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
        //        var response1 = client1.PostAsync(Uri, stringContent).Result;
        //        if (response1.IsSuccessStatusCode)
        //        {

        //            if (response1.IsSuccessStatusCode)
        //            {
        //                var responseContent = response1.Content;
        //                string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
        //                responsePayment = JsonConvert.DeserializeObject<ResponsePayment>(jsonResponse);

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return responsePayment;
        //}

        //public static ResponsePayment PaymentRequestforCard(PaymentRequest paymentRequest)
        //{
        //    HttpClient client1 = new HttpClient();
        //    ResponsePayment responsePayment = new ResponsePayment();
        //    try
        //    {
        //        String Uri = Convert.ToString(ConfigurationManager.AppSettings["PaymentAPIUrl"]) + "GetcreditCardPayment";

        //        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(paymentRequest), Encoding.UTF8, "application/json");
        //        var response1 = client1.PostAsync(Uri, stringContent).Result;
        //        if (response1.IsSuccessStatusCode)
        //        {

        //            if (response1.IsSuccessStatusCode)
        //            {
        //                var responseContent = response1.Content;
        //                string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
        //                responsePayment = JsonConvert.DeserializeObject<ResponsePayment>(jsonResponse);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }
        //    return responsePayment;
        //}
        //public static Cart Add2CartOrderModify(Cart cartRequest)
        //{
        //    Cart cartResult = new Cart();
        //    String Uri = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "Add2CartOrderModify";
        //    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cartRequest), Encoding.UTF8, "application/json");

        //    CartResponse cartResponse = new CartResponse();
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseContent = response.Content;
        //                string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
        //                cartResponse = JsonConvert.DeserializeObject<CartResponse>(jsonResponse);

        //                if (cartResponse.ResponseCode == "1")
        //                {
        //                    cartResult = new Cart();
        //                    cartResult.cartHeader = cartResponse.cartHeader;
        //                    cartResult.Customer = cartResponse.Customer;
        //                    cartResult.cartItems = cartResponse.cartItems;
        //                    cartResult.orderGiftCards = cartResponse.orderGiftCards;
        //                    cartResult.orderUDT = cartResponse.orderUDT;
        //                }
        //                else
        //                {
        //                    cartResult = Session.cart;
        //                }

        //            }
        //            else
        //            {
        //                cartResult = Session.cart;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cartResult = Session.cart;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }
        //    return cartResult;

        //}

        public static PrintOrderReceiptResponse PrintReceiptGeneral(PrintOrderReceiptGeneralRequest printOrderReceiptGeneralRequest)
        {
            HttpClient client1 = new HttpClient();
            PrintOrderReceiptResponse printOrderReceipt = new PrintOrderReceiptResponse();
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["PrintAPIUrl"]) + "PrintOrderReceiptGeneral";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(printOrderReceiptGeneralRequest), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;
                if (response1.IsSuccessStatusCode)
                {

                    if (response1.IsSuccessStatusCode)
                    {
                        var responseContent = response1.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        printOrderReceipt = JsonConvert.DeserializeObject<PrintOrderReceiptResponse>(jsonResponse);
                        if (printOrderReceipt != null)
                        {
                            if (printOrderReceipt.Result.ResponseStatus == "ReprintLimitExceeded")
                            {
                                CustomMessageBox.Show(MessageConstant.ReprintLimit, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            }
                            if (printOrderReceipt.Result.ResponseStatus == "ReprintLimitTimeExceeded")
                            {
                                CustomMessageBox.Show(MessageConstant.ReprintTimeLimit, CustomMessageBox.Buttons.OK, CustomMessageBox.Icon.Warning);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "PrintRecieptGeneral", JsonConvert.SerializeObject(printOrderReceiptGeneralRequest), string.Empty, ex);
            }
            return printOrderReceipt;
        }

        public static GetOrderResponse getorder(GetOrderRequest getOrderRequest)
        {
            HttpClient client1 = new HttpClient();
            GetOrderResponse getOrderResponse = new GetOrderResponse();
            try
            {
                String Url = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "GetOrders";
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(getOrderRequest), Encoding.UTF8, "application/json");

                var response1 = client1.PostAsync(Url, stringContent).Result;
                if (response1.IsSuccessStatusCode)
                {

                    if (response1.IsSuccessStatusCode)
                    {
                        var responseContent = response1.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        getOrderResponse = JsonConvert.DeserializeObject<GetOrderResponse>(jsonResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetOrder", JsonConvert.SerializeObject(getOrderRequest), string.Empty, ex);
            }
            return getOrderResponse;
        }

        public static bool ModifyOrder(OrderRequest orderRequest)
        {
            bool pushOrder = false;
            String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "ModifyOrder";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(orderRequest), Encoding.UTF8, "application/json");

            OrderResponse orderResponse = new OrderResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        orderResponse = JsonConvert.DeserializeObject<OrderResponse>(jsonResponse);
                        if (orderResponse.ResponseStatus.ToLower() == "success")
                        {
                            pushOrder = true;
                            Session.currentOrderResponse = orderResponse.orderResponseData;
                        }
                        else
                        {
                            Session.currentOrderResponse = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                pushOrder = false;
                Logging("ERROR", "ModifyOrder", JsonConvert.SerializeObject(orderRequest), string.Empty, ex);
            }
            return pushOrder;
        }

        //Abhishek for remake

        public static List<CustomerOrderRemake> GetCustomerOrderRemake(string Location_Code, long Customer_Code)
        {
            CustomerOrderRemakeResponse customerOrderRemakeResponse = new CustomerOrderRemakeResponse();
            List<CustomerOrderRemake> customerOrderRemakes = new List<CustomerOrderRemake>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlCustomerOrderHistory = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "GetCustomerOrderRemake";

                    httpClient.BaseAddress = new Uri(urlCustomerOrderHistory);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlCustomerOrderHistory + "?Location_Code=" + Location_Code + "&Customer_Code=" + Customer_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        customerOrderRemakeResponse = responseMessage.Content.ReadAsAsync<CustomerOrderRemakeResponse>().Result;
                        customerOrderRemakes = customerOrderRemakeResponse.customerOrderRemake;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCustomerOrderRemake", Convert.ToString(Customer_Code), string.Empty, ex);
            }
            return customerOrderRemakes;
        }


        public static List<CustomerOrderHistory> GetCustomerOrderHistory(string Location_Code, long Customer_Code)
        {
            CustomerOrderHistoryResponse customerOrderHistoryResponse = new CustomerOrderHistoryResponse();
            List<CustomerOrderHistory> customerOrderHistories = new List<CustomerOrderHistory>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlCustomerOrderHistory = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "GetCustomerOrderHistory";

                    httpClient.BaseAddress = new Uri(urlCustomerOrderHistory);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlCustomerOrderHistory + "?Location_Code=" + Location_Code + "&Customer_Code=" + Customer_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        customerOrderHistoryResponse = responseMessage.Content.ReadAsAsync<CustomerOrderHistoryResponse>().Result;
                        customerOrderHistories = customerOrderHistoryResponse.customerOrderHistory;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCustomerOrderRemake", Convert.ToString(Customer_Code), string.Empty, ex);

            }
            return customerOrderHistories;
        }


        public static List<CatalogCoupons> GetOrderLineCoupons(string Cart_items)
        {
            CatalogCouponsResponse catalogCouponsResponse = new CatalogCouponsResponse();
            List<CatalogCoupons> catalogCoupons = new List<CatalogCoupons>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCoupons";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Entire_Order=" + false + "&Order_Type_Code=" + "" + "&Cart_items=" + Cart_items;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Entire_Order=" + false + "&Order_Type_Code=" + "" + "&Cart_items=" + Cart_items).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogCouponsResponse = responseMessage.Content.ReadAsAsync<CatalogCouponsResponse>().Result;
                        catalogCoupons = catalogCouponsResponse.catalogCoupons;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetOrderLineCoupons", requestString, string.Empty, ex);
            }
            return catalogCoupons;
        }

        public static int UpdateOrderStatus(UpdateOrderStatusRequest updateOrderStatusRequest)
        {
            int result = 0;
            try
            {

                String Url = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "UpdateOrderStatus";
                WebClient wc = new WebClient();

                wc.QueryString.Add("Location_Code", updateOrderStatusRequest.Location_Code);
                wc.QueryString.Add("Order_Number", Convert.ToString(updateOrderStatusRequest.Order_Number));
                wc.QueryString.Add("Order_Date", updateOrderStatusRequest.Order_Date.ToString("yyyy-MM-dd"));
                wc.QueryString.Add("Employee_Code", updateOrderStatusRequest.Employee_Code);
                wc.QueryString.Add("Order_Status", Convert.ToString(updateOrderStatusRequest.Order_Status));
                var data = wc.UploadValues(Url, "POST", wc.QueryString);
                var responseString = UnicodeEncoding.UTF8.GetString(data);
                if (responseString == "1")
                {
                    Logger.Trace("INFO", "UpdateOrderStatus", null, true);
                }

            }
            catch (Exception ex)
            {
                Logging("ERROR", "UpdateOrderStatus", JsonConvert.SerializeObject(updateOrderStatusRequest), string.Empty, ex);
            }
            return result;
        }

        public static bool IsCouponExistinCouponRule(string Coupon_Code)
        {
            bool IsExist = false;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "IsCouponExistinCouponRule";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Coupon_Code=" + Coupon_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        IsExist = responseMessage.Content.ReadAsAsync<Boolean>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "IsCouponExistinCouponRule", Coupon_Code, string.Empty, ex);
            }
            return IsExist;
        }

        public static List<CatalogMenuItemEDVCoupon> GetMenuItemEDVCoupons(string Coupon_Code, string Menu_Code, string Size_Code)
        {
            CatalogMenuItemEDVCouponsResponse catalogMenuItemEDVCouponsResponse = new CatalogMenuItemEDVCouponsResponse();
            List<CatalogMenuItemEDVCoupon> catalogMenuItemEDVCoupons = new List<CatalogMenuItemEDVCoupon>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "MenuItemEDVCoupons";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Coupon_Code=" + Coupon_Code + "&Menu_Code=" + Menu_Code + "&Size_Code=" + Size_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Coupon_Code=" + Coupon_Code + "&Menu_Code=" + Menu_Code + "&Size_Code=" + Size_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemEDVCouponsResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemEDVCouponsResponse>().Result;
                        catalogMenuItemEDVCoupons = catalogMenuItemEDVCouponsResponse.CatalogMenuItemEDVCoupons;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuItemEDVCoupons", requestString, string.Empty, ex);
            }
            return catalogMenuItemEDVCoupons;
        }

        public static int GetAnyItemCountCouponRule(string Coupon_Code)
        {
            if (String.IsNullOrEmpty(Coupon_Code)) return 0;
            int IsExist = 0;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAnyItemCountCouponRule";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Coupon_Code=" + Coupon_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        IsExist = responseMessage.Content.ReadAsAsync<int>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetAnyItemCountCouponRule", Coupon_Code, string.Empty, ex);
            }
            return IsExist;
        }

        public static bool IsCouponExistinCouponRuleEngine(string Coupon_Code)
        {
            bool IsExist = false;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "IsCouponExistinCouponRuleEngine";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Coupon_Code=" + Coupon_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        IsExist = responseMessage.Content.ReadAsAsync<Boolean>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "IsCouponExistinCouponRuleEngine", Coupon_Code, string.Empty, ex);
            }
            return IsExist;
        }

        public static CatalogPOSMenuItemUpsell POSMenuItemUpsell(string Item_String, string Coupon_Code)
        {
            CatalogPOSMenuItemUpsellResponse catalogPOSMenuItemUpsellResponse = new CatalogPOSMenuItemUpsellResponse();
            CatalogPOSMenuItemUpsell catalogPOSMenuItemUpsell = new CatalogPOSMenuItemUpsell();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "POSMenuItemUpsell";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Item_String=" + Item_String + "&Coupon_Code=" + Coupon_Code;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Item_String=" + Item_String + "&Coupon_Code=" + Coupon_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogPOSMenuItemUpsellResponse = responseMessage.Content.ReadAsAsync<CatalogPOSMenuItemUpsellResponse>().Result;
                        catalogPOSMenuItemUpsell = catalogPOSMenuItemUpsellResponse.catalogPOSMenuItemUpsell;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "POSMenuItemUpsell", requestString, string.Empty, ex);
            }
            return catalogPOSMenuItemUpsell;
        }

        public static List<CatalogPOSMenuItemUpsellDisItem> POSMenuItemUpsellDisItem(string Item_String, string Coupon_Code, string Menu_Code, string Size_Code)
        {
            CatalogPOSMenuItemUpsellDisItemResponse catalogPOSMenuItemUpsellDisItemResponse = new CatalogPOSMenuItemUpsellDisItemResponse();
            List<CatalogPOSMenuItemUpsellDisItem> catalogPOSMenuItemUpsellDisItems = new List<CatalogPOSMenuItemUpsellDisItem>();
            string requestString = "";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCouponCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "POSMenuItemUpsellDisItem";

                    httpClient.BaseAddress = new Uri(urlGetCouponCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Item_String=" + Item_String + "&Coupon_Code=" + Coupon_Code + "&Menu_Code=" + Menu_Code + "&Size_Code=" + Size_Code;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCouponCatalog + "?Location_Code=" + Session._LocationCode + "&Item_String=" + Item_String + "&Coupon_Code=" + Coupon_Code + "&Menu_Code=" + Menu_Code + "&Size_Code=" + Size_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogPOSMenuItemUpsellDisItemResponse = responseMessage.Content.ReadAsAsync<CatalogPOSMenuItemUpsellDisItemResponse>().Result;
                        catalogPOSMenuItemUpsellDisItems = catalogPOSMenuItemUpsellDisItemResponse.catalogPOSMenuItemUpsellDisItems;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "POSMenuItemUpsellDisItem", requestString, string.Empty, ex);
            }
            return catalogPOSMenuItemUpsellDisItems;
        }

        public static CatalogOrderPayTypeCodeResponse GetOrderPayTypeCodes(string Location_Code, int Language_Code, int DigitalMode)
        {

            CatalogOrderPayTypeCodeResponse catalogOrderPayTypeCode = new CatalogOrderPayTypeCodeResponse();
            List<CatalogOrderPayTypeCodes> orderPayTypeCodes = new List<CatalogOrderPayTypeCodes>();
            string requestString = "";

            try
            {
                if(Session.catalogOrderPayTypeCodeResponse != null)
                {
                    return Session.catalogOrderPayTypeCodeResponse;
                }

                using (var httpClient = new HttpClient())
                {
                    String url = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetOrderPayTypeCodes";

                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = url + "?Location_Code=" + Location_Code + "&Language_Code=" + Language_Code + "&DigitalMode=" + DigitalMode;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(url + "?Location_Code=" + Location_Code + "&Language_Code=" + Language_Code + "&DigitalMode=" + DigitalMode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogOrderPayTypeCode = responseMessage.Content.ReadAsAsync<CatalogOrderPayTypeCodeResponse>().Result;
                        //orderPayTypeCodes = catalogOrderPayTypeCode.catalogOrderPayTypeCodesForCash;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetOrderPayTypeCodes", requestString, string.Empty, ex);
            }

            return catalogOrderPayTypeCode;
        }

        public static int InsertCreditCardTransaction(CreditCardTrackingRequest creditCardTrackingRequest)
        {
            int TranID = 0;
            Session.responseMsg = String.Empty;
            String Uri = Convert.ToString(ConfigurationManager.AppSettings["PaymentAPIUrl"]) + "InsertCreditCardTransaction";
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(creditCardTrackingRequest), Encoding.UTF8, "application/json");

            CreditCardTrackingResponse creditCardTrackingResponse = new CreditCardTrackingResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        creditCardTrackingResponse = JsonConvert.DeserializeObject<CreditCardTrackingResponse>(jsonResponse);
                        if (creditCardTrackingResponse.Response_Status == "1")
                        {
                            TranID = creditCardTrackingResponse.Transaction_Number;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                TranID = 0;
                Logging("ERROR", "InsertCreditCardTransaction", JsonConvert.SerializeObject(creditCardTrackingRequest), string.Empty, ex);
            }
            return TranID;
        }

        public static int GetPaymentStatus(long TransactionID)
        {
            GetPaymentResponse getPaymentResponse = new GetPaymentResponse();
            int paymentStatus = -1;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String Uri = Convert.ToString(ConfigurationManager.AppSettings["PaymentAPIUrl"]) + "GetPaymentStatus";
                    //HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog).Result;
                    HttpResponseMessage responseMessage = httpClient.GetAsync(Uri + "?TransactionID=" + TransactionID).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        getPaymentResponse = responseMessage.Content.ReadAsAsync<GetPaymentResponse>().Result;
                        if (getPaymentResponse.ResponseStatus.Equals("1"))
                            paymentStatus = getPaymentResponse.TransactionStatusCode.HasValue ? getPaymentResponse.TransactionStatusCode.Value : paymentStatus;

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetPaymentStatus", Convert.ToString(TransactionID), string.Empty, ex);
            }

            return paymentStatus;
        }

        public static List<CatalogCurrencyDenomination> GetCurrencyDenomination(string Location_Code, string Currency_Code)
        {

            CatalogCurrencyDenominationResponse catalogCurrencyDenominationResponse = new CatalogCurrencyDenominationResponse();
            List<CatalogCurrencyDenomination> catalogCurrencyDenominations = new List<CatalogCurrencyDenomination>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String url = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCurrencyDenomination";

                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(url + "?Location_Code=" + Location_Code + "&Currency_Code=" + Currency_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogCurrencyDenominationResponse = responseMessage.Content.ReadAsAsync<CatalogCurrencyDenominationResponse>().Result;
                        catalogCurrencyDenominations = catalogCurrencyDenominationResponse.catalogCurrencyDenomination;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCurrencyDenomination", Currency_Code, string.Empty, ex);
            }

            return catalogCurrencyDenominations;
        }

        public static string LoadUpsellReminder()
        {
            string Notes = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlLoadUpsellReminder = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "LoadUpsellReminder";

                    httpClient.BaseAddress = new Uri(urlLoadUpsellReminder);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlLoadUpsellReminder + "?System_Date=" + Session.SystemDate.Date.ToString("yyyy-MM-dd") + "&Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Notes = responseMessage.Content.ReadAsAsync<string>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "LoadUpsellReminder", string.Empty, string.Empty, ex);
            }
            return Notes;
        }

        //public static TimeClockGetEmpClockedInResponse CheckUserClockedIn(CallType callType, CheckEmployeeRequest timeClockClockInOutRequest)
        //{
        //    TimeClockGetEmpClockedInResponse timeClockGetEmpClockedInResponse = new TimeClockGetEmpClockedInResponse();
        //    string Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]);
        //    Uri += "/TimeClockGetEmpClockedIn";

        //    exception = new Exception();
        //    try
        //    {
        //        if (callType == CallType.POST)
        //        {
        //            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(timeClockClockInOutRequest), Encoding.UTF8, "application/json");
        //            string jsonResponse = Post(Uri, stringContent);
        //            timeClockGetEmpClockedInResponse = JsonConvert.DeserializeObject<TimeClockGetEmpClockedInResponse>(jsonResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        exception = ex;
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return timeClockGetEmpClockedInResponse;

        //}
        public static int EmployeeClockIn(CheckEmployeeRequest checkEmployeeRequest)
        {
            try
            {
                TimeClockGetEmpClockedInResponse timeClockGetEmpClockedInResponse = new TimeClockGetEmpClockedInResponse();
                using (var httpClient = new HttpClient())
                {
                    // String Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]) + "/EmployeeClockIn";
                    String Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]) + "/TimeClockGetEmpClockedIn";

                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(checkEmployeeRequest), Encoding.UTF8, "application/json");
                    //HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog).Result;
                    var response1 = httpClient.PostAsync(Uri, stringContent).Result;
                    //HttpResponseMessage responseMessage = httpClient.GetAsync(Uri + "?Location_Code=" + Location_Code+ "&Systemdatetime="+ Systemdatetime+ "&Employee_Code="+ Employee_Code).Result;
                    if (response1.IsSuccessStatusCode)
                    {
                        var responseContent = response1.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        timeClockGetEmpClockedInResponse = JsonConvert.DeserializeObject<TimeClockGetEmpClockedInResponse>(jsonResponse);
                        if (timeClockGetEmpClockedInResponse.Result.ResponseStatusCode == "1")
                            return 1;
                        else
                            return 0;
                        // return response1.Content.ReadAsAsync<timeClockGetEmpClockedInResponse>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "EmployeeClockIn", JsonConvert.SerializeObject(checkEmployeeRequest), string.Empty, ex);
            }

            return 0;
        }

        public static bool CheckExeItemMov(string Location_Code, string Menu_Code, string Size_Code)
        {
            string requestString = "";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    String Uri = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "CheckExeItemMov";
                    requestString = Uri + "?Location_Code=" + Location_Code + "&Menu_code=" + Menu_Code + "&Size_code=" + Size_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(Uri + "?Location_Code=" + Location_Code + "&Menu_code=" + Menu_Code + "&Size_code=" + Size_Code).Result;


                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<bool>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "CheckExeItemMov", requestString, string.Empty, ex);
            }

            return false;
        }

        public static int GetDeliveryFeeMOV(string Location_Code, string Order_Type_Code, string Coupon_Code, double Amount)
        {
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String Uri = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetDeliveryFeeMOV";
                    requestString = Uri + "?Location_Code=" + Location_Code + "&Order_Type_Code=" + Order_Type_Code + "&Coupon_Code=" + Coupon_Code + "&Amount=" + Amount;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(Uri + "?Location_Code=" + Location_Code + "&Order_Type_Code=" + Order_Type_Code + "&Coupon_Code=" + Coupon_Code + "&Amount=" + Amount).Result;


                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<int>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetDeliveryFeeMOV", requestString, string.Empty, ex);
            }

            return 0;
        }

        public static string EmployeeCashDrop(string Location_code, DateTime System_date, string Employee_COde, decimal amount, string userId)
        {
            CashDropRequest cashDropRequest = new CashDropRequest();
            string jsonResponse = string.Empty;
            try
            {
                
                HttpClient client1 = new HttpClient();    
                cashDropRequest.Location_Code = Location_code;
                cashDropRequest.System_Date = System_date.Date;
                cashDropRequest.Employee_Code = Employee_COde;
                cashDropRequest.Amount = Convert.ToDecimal(amount);
                cashDropRequest.Added_By = userId;

                String Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]) + "/EmployeeCashDrop";
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cashDropRequest), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;

                if (response1.IsSuccessStatusCode)
                {
                    var responseContent = response1.Content;
                    jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();

                }
            }
            catch(Exception ex)
            {
                Logging("ERROR", "GetMenuCategories", JsonConvert.SerializeObject(cashDropRequest), string.Empty, ex);
            }
            return jsonResponse;
        }

        public static string GetClockedInEmployees(string Location_code, DateTime systemdate)
        {
            ClockInDriverRequest clockInDriverRequest = new ClockInDriverRequest();
            string jsonResponse = string.Empty;
            try
            {
                HttpClient client1 = new HttpClient();              
                clockInDriverRequest.Location_code = Location_code;
                clockInDriverRequest.System_date = systemdate.Date;
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]) + "/GetClockedInEmployees";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(clockInDriverRequest), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;

                if (response1.IsSuccessStatusCode)
                {

                    if (response1.IsSuccessStatusCode)
                    {
                        var responseContent = response1.Content;
                        jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();

                    }
                }
            }
            catch(Exception ex)
            {
                Logging("ERROR", "GetClockedInEmployees", JsonConvert.SerializeObject(clockInDriverRequest), string.Empty, ex);
            }
            return jsonResponse;
        }

        //public static List<CatalogText> GetAllCatalogText()
        //{
        //    CatalogTextResponse catalogTextResponse = new CatalogTextResponse();
        //    List<CatalogText> catalogTexts = new List<CatalogText>();
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAllText";
        //            HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + SystemSettings.settings.pintDefaultLanguageCode).Result;

        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                catalogTextResponse = responseMessage.Content.ReadAsAsync<CatalogTextResponse>().Result;
        //                catalogTexts = catalogTextResponse.catalogText;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return catalogTexts;
        //}

        //public static List<CatalogMenuItems> GetAllMenuItems()
        //{
        //    CatalogMenuItemsResponse catalogMenuItemsResponse = new CatalogMenuItemsResponse();
        //    List<CatalogMenuItems> catalogMenuItems = new List<CatalogMenuItems>();
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAllMenuItems";

        //            httpClient.BaseAddress = new Uri(urlGetCatalog);
        //            httpClient.DefaultRequestHeaders.Accept.Clear();
        //            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                catalogMenuItemsResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemsResponse>().Result;
        //                catalogMenuItems = catalogMenuItemsResponse.catalogMenuItems;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return catalogMenuItems;
        //}

        public static string TestAPI(UserTypes.APIType APIType)
        {
            string ReturnValue = "";
            String urlAPI = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                   

                    switch (APIType)
                    {
                        case UserTypes.APIType.Cart:
                            urlAPI = Convert.ToString(ConfigurationManager.AppSettings["CartAPIUrl"]) + "Test";
                            break;
                        case UserTypes.APIType.Catalog:
                            urlAPI = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "Test";
                            break;
                        case UserTypes.APIType.Customer:
                            urlAPI = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]) + "/Test";
                            break;
                        case UserTypes.APIType.Order:
                            urlAPI = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "Test";
                            break;
                        case UserTypes.APIType.Employee:
                            urlAPI = Convert.ToString(ConfigurationManager.AppSettings["LoginUri"]) + "/Test";
                            break;
                        case UserTypes.APIType.Payment:
                            urlAPI = Convert.ToString(ConfigurationManager.AppSettings["PaymentAPIUrl"]) + "Test";
                            break;
                        case UserTypes.APIType.Printing:
                            urlAPI = Convert.ToString(ConfigurationManager.AppSettings["PrintAPIUrl"]) + "Test";
                            break;
                    }

                    //urlAPI = urlAPI + "Test";

                    httpClient.BaseAddress = new Uri(urlAPI);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlAPI).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        ReturnValue = responseMessage.Content.ReadAsAsync<string>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "TestAPI", urlAPI, ReturnValue, ex);
            }
            return ReturnValue;
        }

        public static List<CatalogMenuItemSizes> GetAllMenuItemSizes()
        {
            CatalogMenuItemSizesResponse catalogMenuItemSizesResponse = new CatalogMenuItemSizesResponse();
            List<CatalogMenuItemSizes> catalogMenuItemSizes = new List<CatalogMenuItemSizes>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAllMenuItemSizes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemSizesResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemSizesResponse>().Result;
                        catalogMenuItemSizes = catalogMenuItemSizesResponse.catalogMenuItemSizes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetAllMenuItemSizes", string.Empty, string.Empty, ex);
            }

            return catalogMenuItemSizes;
        }

        public static string GetMenuItemOptionGroups(string Menu_Code)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuItemOptionGroups";
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuItemOptionGroups", Menu_Code, string.Empty, ex);
            }
            return string.Empty;
        }

        public static List<CatalogDefaultToppings> GetDefaultToppings(string Menu_Code, string Menu_Option_Group_Code)
        {
            CatalogDefaultToppingsResponse catalogDefaultToppingsResponse = new CatalogDefaultToppingsResponse();
            List<CatalogDefaultToppings> catalogDefaultToppings = new List<CatalogDefaultToppings>();
            string requestString = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetDefaultToppings";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    requestString = urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code + "&Menu_Option_Group_Code=" + Menu_Option_Group_Code;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code + "&Menu_Option_Group_Code=" + Menu_Option_Group_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogDefaultToppingsResponse = responseMessage.Content.ReadAsAsync<CatalogDefaultToppingsResponse>().Result;
                        catalogDefaultToppings = catalogDefaultToppingsResponse.CatalogDefaultToppings;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetDefaultToppings", requestString, string.Empty, ex);
            }

            return catalogDefaultToppings;
        }

        #region AsyncMethods

        public static async Task<List<CatalogMenuCategory>> GetMenuCategoriesAsync(int MenuTypeID)
        {
            List<CatalogMenuCategory> catalogMenuCategories = new List<CatalogMenuCategory>();
            try
            {
                CatalogMenuCategoryResponse catalogMenuCategoryResponse = new CatalogMenuCategoryResponse();

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuCategories";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Type_ID=" + MenuTypeID).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuCategoryResponse = responseMessage.Content.ReadAsAsync<CatalogMenuCategoryResponse>().Result;
                        if (catalogMenuCategoryResponse.catalogMenuCategories != null)
                            catalogMenuCategories = catalogMenuCategoryResponse.catalogMenuCategories;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogMenuCategories;
        }

        public async static Task<List<CatalogText>> GetAllCatalogTextAsync()
        {
            CatalogTextResponse catalogTextResponse = new CatalogTextResponse();
            List<CatalogText> catalogTexts = new List<CatalogText>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAllText";
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + SystemSettings.settings.pintDefaultLanguageCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogTextResponse = responseMessage.Content.ReadAsAsync<CatalogTextResponse>().Result;
                        catalogTexts = catalogTextResponse.catalogText;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogTexts;
        }

        public async static Task<List<CatalogText>> GetToppingSizesAsync()
        {
            List<CatalogText> toppingSizes = new List<CatalogText>();

            if (Session.ToppingSizes != null && Session.ToppingSizes.Count > 0)
            {
                return Session.ToppingSizes;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetToppingSizes";


                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        toppingSizes = responseMessage.Content.ReadAsAsync<List<CatalogText>>().Result;
                        Session.ToppingSizes = toppingSizes;
                    }
                }
            }
            catch (Exception ex)
            {
                //////Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return toppingSizes;
        }

        public async static Task<List<CatalogText>> GetSpecialtyPizzaTextAsync()
        {
            List<CatalogText> specialityPizza = new List<CatalogText>();

            if (Session.SpecialtyPizzaText != null && Session.SpecialtyPizzaText.Count > 0)
            {
                return Session.SpecialtyPizzaText;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetSpecialtyPizzaText";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        specialityPizza = responseMessage.Content.ReadAsAsync<List<CatalogText>>().Result;
                        Session.SpecialtyPizzaText = specialityPizza;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return specialityPizza;
        }

        public async static Task<List<CatalogText>> GetItemPartsAsync()
        {
            List<CatalogText> itemParts = new List<CatalogText>();

            if (Session.ItemParts != null && Session.ItemParts.Count > 0)
            {
                return Session.ItemParts;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetItemParts";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        itemParts = responseMessage.Content.ReadAsAsync<List<CatalogText>>().Result;
                        Session.ItemParts = itemParts;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return itemParts;
        }

        public async static Task<CatalogCartCaptions> GetCartCaptionsAsync()
        {
            CatalogCartCaptions catalogCartCaptions = new CatalogCartCaptions();

            if (Session.CartCaptions != null)
            {
                return Session.CartCaptions;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCartCaptions";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogCartCaptions = responseMessage.Content.ReadAsAsync<CatalogCartCaptions>().Result;
                        Session.CartCaptions = catalogCartCaptions;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogCartCaptions;
        }

        public async static Task<List<CatalogMenuTypes>> GetMenuTypesAsync()
        {
            CatalogMenuTypesResponse catalogMenuTypesResponse = new CatalogMenuTypesResponse();
            List<CatalogMenuTypes> catalogMenuTypes = new List<CatalogMenuTypes>();

            if (Session.MenuTypes != null && Session.MenuTypes.Count > 0)
            {
                return Session.MenuTypes;
            }

            try
            {

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuTypes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&WorkStation_ID=" + Session._WorkStationID).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuTypesResponse = responseMessage.Content.ReadAsAsync<CatalogMenuTypesResponse>().Result;
                        catalogMenuTypes = catalogMenuTypesResponse.catalogMenuTypes;
                        Session.MenuTypes = catalogMenuTypes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogMenuTypes;
        }

        public async static Task<int> GetEstimatedDeliveryTimeAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetEstimatedDeliveryTime";
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<int>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return 0;
        }

        public async static Task<int> GetEstimatedCarryOutTimeAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetEstimatedCarryOutTime";
                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<int>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return 0;
        }

        public async static Task<string> LoadUpsellReminderAsync()
        {
            string Notes = "";
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlLoadUpsellReminder = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "LoadUpsellReminder";

                    httpClient.BaseAddress = new Uri(urlLoadUpsellReminder);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlLoadUpsellReminder + "?System_Date=" + Session.SystemDate.Date.ToString("yyyy-MM-dd") + "&Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Notes = responseMessage.Content.ReadAsAsync<string>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }
            return Notes;
        }

        public async static Task<List<CatalogMenuItems>> GetAllMenuItemsAsync()
        {
            CatalogMenuItemsResponse catalogMenuItemsResponse = new CatalogMenuItemsResponse();
            List<CatalogMenuItems> catalogMenuItems = new List<CatalogMenuItems>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAllMenuItems";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemsResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemsResponse>().Result;
                        catalogMenuItems = catalogMenuItemsResponse.catalogMenuItems;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogMenuItems;
        }

        public async static Task<List<CatalogMenuItemSizes>> GetAllMenuItemSizesAsync()
        {
            CatalogMenuItemSizesResponse catalogMenuItemSizesResponse = new CatalogMenuItemSizesResponse();
            List<CatalogMenuItemSizes> catalogMenuItemSizes = new List<CatalogMenuItemSizes>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAllMenuItemSizes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemSizesResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemSizesResponse>().Result;
                        catalogMenuItemSizes = catalogMenuItemSizesResponse.catalogMenuItemSizes;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogMenuItemSizes;
        }

        public async static Task<List<CatalogOptionGroups>> GetOptionGroupsAsync(string Menu_Code)
        {
            List<CatalogOptionGroups> catalogOptionGroups = new List<CatalogOptionGroups>();
            try
            {
                if (Session.catalogOptionGroups != null && Session.catalogOptionGroups.Count > 0)
                {
                    List<CatalogOptionGroups> optionGroups = Session.catalogOptionGroups.FindAll(x => x.Menu_Code == Menu_Code);
                    if (optionGroups != null && optionGroups.Count > 0) return optionGroups;
                }

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetOptionGroups";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Menu_Code=" + Menu_Code).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogOptionGroups = responseMessage.Content.ReadAsAsync<List<CatalogOptionGroups>>().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogOptionGroups;
        }        
        
        public async static Task<StreetLookUpResponse> StreetLookUpAsync()
        {
            StreetLookUpResponse streetLookUpResponse = new StreetLookUpResponse();

            try
            {
                StreetLookUpRequest streetLookUpRequest = new StreetLookUpRequest();
                streetLookUpRequest.LocationCode = Session._LocationCode;
                streetLookUpRequest.StreetName = "%%";

                string Uri = Convert.ToString(ConfigurationManager.AppSettings["CustomerAPIUrl"]);
                Uri += "/StreetLookUp";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(streetLookUpRequest), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(Uri, stringContent).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        var result = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        streetLookUpResponse = JsonConvert.DeserializeObject<StreetLookUpResponse>(result);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return streetLookUpResponse;
        }

        public async static Task<CatalogOrderPayTypeCodeResponse> GetOrderPayTypeCodesAsync()
        {
            CatalogOrderPayTypeCodeResponse catalogOrderPayTypeCode = new CatalogOrderPayTypeCodeResponse();            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String url = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetOrderPayTypeCodes";

                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(url + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + 1 + "&DigitalMode=" + 0).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogOrderPayTypeCode = responseMessage.Content.ReadAsAsync<CatalogOrderPayTypeCodeResponse>().Result;                        
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogOrderPayTypeCode;
        }


        public static async Task<CatalogUpsellData> GetUpsellDataAsync()
        {
            CatalogUpsellData catalogUpsellData = new CatalogUpsellData();
            try
            {
                CatalogUpsellDataResponse catalogUpsellDataResponse = new CatalogUpsellDataResponse();

                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetUpsellData";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).ConfigureAwait(false);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogUpsellDataResponse = responseMessage.Content.ReadAsAsync<CatalogUpsellDataResponse>().Result;
                        if (catalogUpsellDataResponse.catalogUpsellData != null)
                            catalogUpsellData = catalogUpsellDataResponse.catalogUpsellData;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogUpsellData;
        }

        #endregion

        public static void PrintAbandedOrderResponse(PrintAbandedOrderRequest PrintAbandedOrderRequest)
        {
            HttpClient client1 = new HttpClient();
            PrintOrderReceiptResponse PrintAbandedOrderResponse = new PrintOrderReceiptResponse();
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["PrintAPIUrl"]) + "PrintAbandedOrder";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(PrintAbandedOrderRequest), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;

                if (response1.IsSuccessStatusCode)
                {
                    var responseContent = response1.Content;
                    string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    PrintAbandedOrderResponse = JsonConvert.DeserializeObject<PrintOrderReceiptResponse>(jsonResponse);
                }

            }
            catch (Exception ex)
            {
                Logging("ERROR", "PrintAbandedOrderResponse", JsonConvert.SerializeObject(PrintAbandedOrderRequest), string.Empty, ex);
            }            
        }

        public static bool PrintCashDropResponse(PrintCashDropRequest PrintCashDropRequest)
        {
            HttpClient client1 = new HttpClient();
            bool PrintCashDropResponse = false;
            //PrintOrderReceiptResponse printOrderReceipt = new PrintOrderReceiptResponse();
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["PrintAPIUrl"]) + "PrintCashDrop";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(PrintCashDropRequest), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;

                if (response1.IsSuccessStatusCode)
                {
                    var responseContent = response1.Content;
                    string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    PrintCashDropResponse = JsonConvert.DeserializeObject<bool>(jsonResponse);


                }

            }
            catch (Exception ex)
            {
                Logging("ERROR", "PrintCashDropResponse", JsonConvert.SerializeObject(PrintCashDropRequest), string.Empty, ex);
            }
            return PrintCashDropResponse;
        }

        public static List<EmployeeOrderTypes> GetEmployeeOrderTypes(string Location_Code, string Employee_Code)
        {
            EmployeeOrderTypesResponse getEmployeeOrderTypesResponse = new EmployeeOrderTypesResponse();
            List<EmployeeOrderTypes> EmployeeOrderTypes = new List<EmployeeOrderTypes>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetEmployeeOrderTypes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Employee_Code=" + Employee_Code).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        getEmployeeOrderTypesResponse = responseMessage.Content.ReadAsAsync<EmployeeOrderTypesResponse>().Result;
                        EmployeeOrderTypes = getEmployeeOrderTypesResponse.Order_Type_Code;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetEmployeeOrderTypes", Employee_Code, string.Empty, ex);
            }

            return EmployeeOrderTypes;
        }

        public static List<OrderType> GetOrderTypes()
        {
            OrderTypeResponse orderTypeResponse = new OrderTypeResponse();
            List<OrderType> orderType = new List<OrderType>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetOrderTypes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + "1").Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        orderTypeResponse = responseMessage.Content.ReadAsAsync<OrderTypeResponse>().Result;
                        orderType = orderTypeResponse.OrderType;

                    }
                }
            }
            catch (Exception ex)
            {

                Logging("ERROR", "GetOrderTypes", string.Empty, string.Empty, ex);
            }

            return orderType;
        }

        public static List<CatalogAddressTypes> GetAddressTypes()
        {
            CatalogAddressTypesResponse catalogAddressTypesResponse = new CatalogAddressTypesResponse();
            List<CatalogAddressTypes> catalogAddressTypes = new List<CatalogAddressTypes>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAddressTypes";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + "1").Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogAddressTypesResponse = responseMessage.Content.ReadAsAsync<CatalogAddressTypesResponse>().Result;
                        catalogAddressTypes = catalogAddressTypesResponse.AddressTypes;

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetAddressTypes", string.Empty, string.Empty, ex);
            }

            return catalogAddressTypes;
        }
        public static List<CatalogControlPropeties> GetControlSetting(string FormName)
        {
            CatalogControlPropetiesResponse catalogControlPropetiesResponse = new CatalogControlPropetiesResponse();
            List<CatalogControlPropeties> catalogControlPropeties = new List<CatalogControlPropeties>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetControlSetting";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Form_Name=" + FormName).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogControlPropetiesResponse = responseMessage.Content.ReadAsAsync<CatalogControlPropetiesResponse>().Result;
                        catalogControlPropeties = catalogControlPropetiesResponse.catalogControlPropeties;

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetControlSetting", FormName, string.Empty, ex);
            }

            return catalogControlPropeties;
        }

        public static List<CatalogControlText> GetControlText(string FormName)
        {
            CatalogControlTextResponse catalogControlTextResponse = new CatalogControlTextResponse();
            List<CatalogControlText> catalogControlText = new List<CatalogControlText>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetControlText";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + "1" + "&Form_Name=" + FormName).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogControlTextResponse = responseMessage.Content.ReadAsAsync<CatalogControlTextResponse>().Result;
                        catalogControlText = catalogControlTextResponse.catalogControlText;

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetControlText", FormName, string.Empty, ex);
            }

            return catalogControlText;
        }
        public static List<CatalogAllButtonText> GetAllButtonText()
        {
            CatalogAllButtonTextResponse catalogAllButtonTextResponse = new CatalogAllButtonTextResponse();
            List<CatalogAllButtonText> catalogAllButtonText = new List<CatalogAllButtonText>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetAllButtonText";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + "1").Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogAllButtonTextResponse = responseMessage.Content.ReadAsAsync<CatalogAllButtonTextResponse>().Result;
                        catalogAllButtonText = catalogAllButtonTextResponse.catalogAllButtonText;

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetAllButtonText", string.Empty, string.Empty, ex);
            }

            return catalogAllButtonText;
        }

        //public static int UpdateClosedOrderTime(string Location_Code, DateTime Order_Date,long Order_Number, bool blnNewDataSource, bool blnManual)
        //{
        //    try
        //    {

        //        using (var httpClient = new HttpClient())
        //        {
                    
        //            String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "UpdateClosedOrderTime";

        //            httpClient.BaseAddress = new Uri(Uri);
        //            httpClient.DefaultRequestHeaders.Accept.Clear();
        //            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            HttpResponseMessage responseMessage = httpClient.GetAsync(Uri + "?Location_Code=" + Location_Code + "&Order_Date=" + Order_Date.ToString("yyyy-MM-dd") + "&Order_Number=" + Order_Number + "&blnNewDataSource=" + blnNewDataSource + "&blnManual=" + blnManual).Result;
        //            //HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&Language_Code=" + "1").Result;

        //            if (responseMessage.IsSuccessStatusCode)
        //            {
        //                return responseMessage.Content.ReadAsAsync<int>().Result;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Trace("ERROR", ex.Message, ex, true);
        //    }

        //    return 0;
        //}

        public static int UpdateClosedOrderTime(string Location_Code, DateTime Order_Date, long Order_Number, bool blnNewDataSource, bool blnManual)
        {
            int result = 0;
            
            try
            {
                String Url = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "UpdateClosedOrderTime";
                WebClient wc = new WebClient();
                wc.QueryString.Add("Location_Code", Location_Code);
                wc.QueryString.Add("Order_Date", Order_Date.ToString("yyyy-MM-dd"));
                wc.QueryString.Add("Order_Number", Convert.ToString(Order_Number));
                wc.QueryString.Add("blnNewDataSource", Convert.ToString(blnNewDataSource));
                wc.QueryString.Add("blnManual", Convert.ToString(blnManual));
                var data = wc.UploadValues(Url, "POST", wc.QueryString);
                var responseString = UnicodeEncoding.UTF8.GetString(data);
                if (responseString == "1")
                {
                    result=Convert.ToInt32(responseString);
                }

            }
            catch (Exception ex)
            {
                Logging("ERROR", "UpdateClosedOrderTime", "ordno: " + Order_Number, string.Empty, ex);
            }
            return result;
        }

        private static void Logging(string type, string methodName, string request, string response, Exception ex)
        {
            string message = methodName + "- request: " + request + ", response: " + response + ", errorMsg: " + ex.Message;
            Logger.Trace(type.ToUpper(), message, ex, true);
        }

        public static async Task<List<CatalogBusinessUnit>> LoadBussinessUnitAsync(string Location_Code)
        {
            List<CatalogBusinessUnit> catalogBusinessUnit = new List<CatalogBusinessUnit>();
            try
            {
                CatalogBusinessUnitResponse catalogBusinessUnitResponse = new CatalogBusinessUnitResponse();

                using (var httpClient = new HttpClient())
                {
                    String urlGetBUnit = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetBusinessUnit";

                    httpClient.BaseAddress = new Uri(urlGetBUnit);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetBUnit + "?Location_Code=" + Session._LocationCode);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogBusinessUnitResponse = responseMessage.Content.ReadAsAsync<CatalogBusinessUnitResponse>().Result;
                        if (catalogBusinessUnitResponse.catalogBusinessUnit != null)
                            catalogBusinessUnit = catalogBusinessUnitResponse.catalogBusinessUnit;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogBusinessUnit;
        }


        public static async Task<List<CatalogSourceName>> LoadSourceNameAsync(string Location_Code)
        {
            List<CatalogSourceName> catalogSourceName = new List<CatalogSourceName>();
            try
            {
                CatalogSourceNameResponse catalogSourceNameResponse = new CatalogSourceNameResponse();

                using (var httpClient = new HttpClient())
                {
                    String urlGetSourceName = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetSourceName";

                    httpClient.BaseAddress = new Uri(urlGetSourceName);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(urlGetSourceName + "?Location_Code=" + Session._LocationCode);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogSourceNameResponse = responseMessage.Content.ReadAsAsync<CatalogSourceNameResponse>().Result;
                        if (catalogSourceNameResponse.catalogSourceName != null)
                            catalogSourceName = catalogSourceNameResponse.catalogSourceName;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Trace("ERROR", ex.Message, ex, true);
            }

            return catalogSourceName;
        }

        public static List<CatalogMenuItemStatus> GetMenuItemStatus()
        {
            CatalogMenuItemStatusResponse catalogMenuItemStatusResponse = new CatalogMenuItemStatusResponse();
            List<CatalogMenuItemStatus> catalogMenuItemStatus = new List<CatalogMenuItemStatus>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuItemStatus";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemStatusResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemStatusResponse>().Result;
                        catalogMenuItemStatus = catalogMenuItemStatusResponse.catalogMenuItemStatus;

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuItemStatus", string.Empty, string.Empty, ex);
            }

            return catalogMenuItemStatus;
        }

        public static List<CatalogMenuItemSizesStatus> GetMenuItemSizesStatus()
        {
            CatalogMenuItemSizesStatusResponse catalogMenuItemSizesStatusResponse = new CatalogMenuItemSizesStatusResponse();
            List<CatalogMenuItemSizesStatus> catalogMenuItemSizesStatus = new List<CatalogMenuItemSizesStatus>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetMenuItemSizesStatus";


                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        catalogMenuItemSizesStatusResponse = responseMessage.Content.ReadAsAsync<CatalogMenuItemSizesStatusResponse>().Result;
                        catalogMenuItemSizesStatus = catalogMenuItemSizesStatusResponse.catalogMenuItemSizesStatus;


                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetMenuItemSizesStatus", string.Empty, string.Empty, ex);
            }

            return catalogMenuItemSizesStatus;
        }
		
		public static List<CombosForUpsell> GetCombosForUpsell(string MenuCode1, string SizeCode1, string MenuCode2 = "", string SizeCode2 = "")
        {
            CombosForUpsellResponse combosForUpsellResponse = new CombosForUpsellResponse();
            List<CombosForUpsell> combosForUpsells = new List<CombosForUpsell>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCombosforUpsell";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?Location_Code=" + Session._LocationCode + "&MenuCode1=" + MenuCode1 + "&SizeCode1=" + SizeCode1 + "&MenuCode2=" + MenuCode2 + "&SizeCode2=" + SizeCode2).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        combosForUpsellResponse = responseMessage.Content.ReadAsAsync<CombosForUpsellResponse>().Result;
                        combosForUpsells = combosForUpsellResponse.combosForUpsell;

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetCombosForUpsell", string.Empty, string.Empty, ex);
            }

            return combosForUpsells;
        }


        public static void InsertCashRegisterReason(CashDrawerReason cashDrawerReason)
        {
            HttpClient client1 = new HttpClient();
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "InsertCashRegisterReason";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cashDrawerReason), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;

                if (response1.IsSuccessStatusCode)
                {
                    var responseContent = response1.Content;
                    string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    //cashDrawerUpdate = JsonConvert.DeserializeObject<bool>(jsonResponse);
                }

            }
            catch (Exception ex)
            {
                Logging("ERROR", "CashDrawerLockUpdate", JsonConvert.SerializeObject(cashDrawerReason), string.Empty, ex);
            }

        }


        public static CashDrawerInfoDto GetCashDrawerInfo(string workstationName, string employeeCode, int iType)
        {
            CashDrawerInfoResponse cashDrawerInfoResponse = new CashDrawerInfoResponse();
            List<CashDrawerInfoDto> cashDrawers = new List<CashDrawerInfoDto>();
            CashDrawerInfoDto cashDrawer = new CashDrawerInfoDto();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String urlGetCatalog = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCashDrawerInfo";

                    httpClient.BaseAddress = new Uri(urlGetCatalog);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage responseMessage = httpClient.GetAsync(urlGetCatalog + "?workstationName=" + workstationName + "&employeeCode=" + employeeCode + "&iType=" + iType).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        cashDrawerInfoResponse = responseMessage.Content.ReadAsAsync<CashDrawerInfoResponse>().Result;
                        cashDrawers = cashDrawerInfoResponse.cashDrawerInfo;
                        if (cashDrawers != null && cashDrawers.Count > 0)
                        {
                            cashDrawer = cashDrawers[0];
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logging("ERROR", "GetAddressTypes", string.Empty, string.Empty, ex);
            }

            return cashDrawer;
        }



        public static bool CashDrawerLockUpdate(CashDrawerLockUnlockRequest cashDrawerLockUnlockRequest)
        {
            HttpClient client1 = new HttpClient();
            bool cashDrawerUpdate = false;

            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "CashDrawerLockUpdate";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cashDrawerLockUnlockRequest), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;

                if (response1.IsSuccessStatusCode)
                {
                    var responseContent = response1.Content;
                    string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    cashDrawerUpdate = JsonConvert.DeserializeObject<bool>(jsonResponse);
                }

            }
            catch (Exception ex)
            {
                Logging("ERROR", "CashDrawerLockUpdate", JsonConvert.SerializeObject(cashDrawerLockUnlockRequest), string.Empty, ex);
            }
            return cashDrawerUpdate;
        }


        public static string GetWorkstationIP(int workstationID)
        {

            string workstationIP = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String Uri = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetWorkstationIP";

                    HttpResponseMessage responseMessage = httpClient.GetAsync(Uri + "?locationCode=" + Session._LocationCode + "&workstationID=" + workstationID).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsStringAsync().Result;
                    }

                }


            }
            catch (Exception ex)
            {
                Logging("ERROR", "workstationIP", "", string.Empty, ex);
            }
            return workstationIP;
        }

        public static void InsertCashRegisterReasonForOrder(List<CashDrawerReason> cashDrawerReasons)
        {
            HttpClient client1 = new HttpClient();
            bool cashDrawerUpdate = false;

            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "InsertCashRegisterReasonForOrder";

                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(cashDrawerReasons), Encoding.UTF8, "application/json");
                var response1 = client1.PostAsync(Uri, stringContent).Result;

                if (response1.IsSuccessStatusCode)
                {
                    var responseContent = response1.Content;
                    string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                    //cashDrawerUpdate = JsonConvert.DeserializeObject<bool>(jsonResponse);
                }

            }
            catch (Exception ex)
            {
                Logging("ERROR", "CashDrawerLockUpdate", JsonConvert.SerializeObject(cashDrawerReasons), string.Empty, ex);
            }
        }


        public static Int16 CashDrawerStatus(string Workstation_Name, string EmployeeCode, Int16 Flag)
        {
            HttpClient client1 = new HttpClient();
            Int16 CashDrawerStatus = 0;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    String Uri = Convert.ToString(ConfigurationManager.AppSettings["CatalogAPIUrl"]) + "GetCashDrawerStatus";

                    string requestString = Uri + "?Workstation_Name=" + Workstation_Name + "&EmployeeCode=" + EmployeeCode + "&Flag=" + Flag;

                    HttpResponseMessage responseMessage = httpClient.GetAsync(Uri + "?Workstation_Name=" + Workstation_Name + "&EmployeeCode=" + EmployeeCode + "&Flag=" + Flag).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseContent = responseMessage.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        CashDrawerStatus = JsonConvert.DeserializeObject<Int16>(jsonResponse);

                    }

                }


            }
            catch (Exception ex)
            {
                Logging("ERROR", "CashDrawerStatus", JsonConvert.SerializeObject(CashDrawerStatus), string.Empty, ex);
            }
            return CashDrawerStatus;
		}
		
        public static int LogItemwiseUpsellHistory(List<ItemwiseUpsellHistory> itemwiseUpsellHistory)
        {
            int result = 0;
            try
            {
                String Uri = Convert.ToString(ConfigurationManager.AppSettings["OrderAPIUrl"]) + "ItemwiseUpsellHistory";
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(itemwiseUpsellHistory), Encoding.UTF8, "application/json");
               
                using (var client = new HttpClient())
                {
                    var response = client.PostAsync(Uri, stringContent).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;
                        string jsonResponse = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();
                        result = JsonConvert.DeserializeObject<int>(jsonResponse);                       

                    }                    
                }
            }
            catch (Exception ex)
            {                
                Logging("ERROR", "LogItemwiseUpsellHistory", JsonConvert.SerializeObject(itemwiseUpsellHistory), string.Empty, ex);
            }

            return result;

        }


    }
}