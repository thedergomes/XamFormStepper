using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CustomTab.Models.Market
{
    public class ProductModel
    {
        private string mainImage = "";

        [JsonIgnore]
        public string MainImage
        {
            get
            {
                if (Pictures.Any())
                {
                    return Pictures[0];
                }
                return mainImage;
            }
        }

        //ignorar
        [JsonIgnore]
        public int LayoutOption
        {
            get;
            set;
        }

        //ignore 
        //numero de pagina
        [JsonIgnore]
        public int Index
        {
            get;
            set;
        }

        //name
        [JsonProperty("name")]
        public string Title
        {
            get;
            set;
        }

        //description
        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }

        //price
        [JsonProperty("price")]
        public string Price
        {
            get;
            set;
        }

        //username
        [JsonProperty("username")]
        public string Seller
        {
            get;
            set;
        }

        //pictures
        [JsonProperty("pictures")]
        public List<string> Pictures
        {
            get;
            set;
        }
    }
}
