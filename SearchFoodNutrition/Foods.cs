using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Diagnostics;

namespace SearchFoodNutrition
{
    public struct Foods
    {
        public int Index;
        public string Name;
        public double Weight;
        public string Measure;
        public string Carb;
        public string Fiber;
        public string Fat;
    }

    public delegate void MyCompleted();
    public delegate void MyStatus(string message);

    public class AllFoods
    {
        public event MyCompleted Completed;
        public event MyStatus Status;

        public Foods Current = new Foods();

        private string prefix = "https://api.nal.usda.gov/ndb/nutrients/?format=json&";
        private string suffix = "&max=1500&nutrients=204&nutrients=205&nutrients=291&api_key=UtLGRdU1PNb1tQ2fuU3oecOgCCPUAdjQHwodMWFn";
        private string apikey = "&api_key=UtLGRdU1PNb1tQ2fuU3oecOgCCPUAdjQHwodMWFn";
        private FoodRoot fr;
        private List<Food> fd;
        private string allFoods;
        private readonly string jsonFile = @"foods.jsn";

        public AllFoods()
        {
            Current.Index = -1;
            LoadFile();
        }

        /// <summary>
        /// Use load to load the search from the database
        /// </summary>
        public void ProcessFoods()
        {
            LoadNutrition();
        }

        public System.Windows.Forms.AutoCompleteStringCollection GetNames(string pre)
        {
            System.Windows.Forms.AutoCompleteStringCollection Names = new System.Windows.Forms.AutoCompleteStringCollection();
            if (fd == null)
                Names.Add("");
            else
            {
                Names.AddRange((from a in fr.report.foods
                                where ( a.name.StartsWith(pre, true, System.Globalization.CultureInfo.CurrentCulture) )
                                select a.name).ToArray<string>());
            }
            return Names;
        }

        public Foods LoadActive(int index)
        {
            Foods food = new Foods();
            food.Index = -1;
            if (index < 0 || index > fd.Count)
                return food;
            food.Index = index;
            food.Name = fd[index].name;
            food.Carb = fd[index].nutrients[0].value;
            food.Fiber = fd[index].nutrients[1].value;
            food.Fat = fd[index].nutrients[2].value;
            return food;
        }

        public Foods LoadActive(string selected)
        {
            int fi = fd.FindIndex(a => a.name.ToString() == selected);
            return LoadActive(fi);
        }

        private async void LoadNutrition()
        {

            string[] json;
            int j = 0;
            int start = 1;
            int end = 1500;

            string raw = prefix + "&offset=0" + suffix;
            try
            {
                HttpClient hpClient = new HttpClient();
                string results = await hpClient.GetStringAsync(raw); //.ConfigureAwait(false);
                int tstart = results.IndexOf("\"total\": ") + 8;
                int tend = results.IndexOf(",", tstart);
                string totals = results.Substring(tstart, tend - tstart);
                int total = int.Parse(totals);
                if (fr != null && fr.report.total == total)
                {
                    //we already have the data
                    //fd = fr.report.foods;
                    //Current = LoadActive(0);
                    Completed();
                    return;
                }
                int ts = (int)(total / 1500) + 1;
                int current = 1500;
                json = new string[ts];
                json[j] = results;
                j += 1;
                Status("Processed 1 of " + ts + ".");
                do
                {
                    start += 1500;
                    end += 1500;
                    raw = prefix + "&offset=" + start + suffix;
                    results = string.Empty;
                    results = await hpClient.GetStringAsync(raw); //.ConfigureAwait(false);
                    json[j] = results;
                    tstart = results.IndexOf("\"end\": ") + 7;
                    tend = results.IndexOf(",", tstart);
                    totals = results.Substring(tstart, tend - tstart);
                    current = int.Parse(totals);
                    j += 1;
                    Status("Processed " + j + " of " + ts + ".");
                } while (total > current);

                ProcessAll(json);
            }
            catch (Exception ex)
            {
                Debug.Write("Error loading query: \n\t" + raw + " \n\t:: " + ex);
            }
        }

        private void ProcessAll(string[] json)
        {
            Status("Processed string 1 of " + (json.Length) + ".");
            fr = Deserialize<FoodRoot>(json[0]);

            for (int j = 1; j < json.Length; j++)
            {
                Status("Processed string " + j + " of " + (json.Length - 1) + ".");
                FoodRoot tr = Deserialize<FoodRoot>(json[j]);
                fr.report.foods.AddRange(tr.report.foods);
            }

            fd = fr.report.foods;
            Current = LoadActive(0);
            Status("Foods loaded");
            Completed();
            WriteFile();
        }

        private void ProcessAll(string json)
        {
            fr = Deserialize<FoodRoot>(json);
            fd = fr.report.foods;
            Current = LoadActive(0);
        }

        private static FoodRoot Deserialize<FoodRoot>(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FoodRoot));
                return (FoodRoot)serializer.ReadObject(ms);
            }
        }

        private static string Serialize<FoodRoot>(FoodRoot obj)
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

        private void LoadFile()
        {
            if (File.Exists(jsonFile) == false)
            {
                allFoods = string.Empty;
                return;
            }
            using (var streamReader = new StreamReader(jsonFile, Encoding.UTF8))
            {
                allFoods = streamReader.ReadToEnd();
            }
            ProcessAll(allFoods);
        }

        private void WriteFile()
        {
            allFoods = Serialize(fr);
            var path = jsonFile;
            File.WriteAllText(path, allFoods);
        }
    }

    public class Nutrient
    {
        public string nutrient_id { get; set; }
        public string nutrient { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
        public object gm { get; set; }
    }

    public class Food
    {
        public string ndbno { get; set; }
        public string name { get; set; }
        public double weight { get; set; }
        public string measure { get; set; }
        public List<Nutrient> nutrients { get; set; }
    }

    public class Report
    {
        public string sr { get; set; }
        public string groups { get; set; }
        public string subset { get; set; }
        public int end { get; set; }
        public int start { get; set; }
        public int total { get; set; }
        public List<Food> foods { get; set; }
    }

    public class FoodRoot
    {
        public Report report { get; set; }
    }
}
