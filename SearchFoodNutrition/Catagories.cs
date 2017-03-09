using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SearchFoodNutrition
{

    public class Catagories
    {
        public event MyCompleted Completed;

        private string raw;
        public List<string> Cats;

        public Catagories()
        {

        }

        public void LoadCatagories(string raw)
        {
            this.raw = raw;

            LoadData(raw);
        }

        private async void LoadData(string raw)
        {
            HttpClient hpClient = new HttpClient();
            string results = await hpClient.GetStringAsync(raw);
            
            CatagoryRoot cr = Deserialize<CatagoryRoot>(results);
            Cats = (from a in cr.list.item
                          where a.name != ""
                          select a.name).ToList<string>();
            Cats.Insert(0, "All");
            Completed();
        }

        private static CatagoryRoot Deserialize<CatagoryRoot>(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CatagoryRoot));
                return (CatagoryRoot)serializer.ReadObject(ms);
            }
        }

        private static string Serialize<CatagoryRoot>(CatagoryRoot obj)
        {
            byte[] json = new byte[0];
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                json = ms.ToArray();
            }
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

    }

    public class Items
    {
        public int offset { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Lists
    {
        public string lt { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int total { get; set; }
        public string sr { get; set; }
        public string sort { get; set; }
        public List<Items> item { get; set; }
    }

    public class CatagoryRoot
    {
        public Lists list { get; set; }
    }
}
