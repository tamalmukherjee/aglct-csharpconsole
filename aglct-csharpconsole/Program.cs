using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace aglct_csharpconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please wait, searching for cats...");
            var data = GetDataFromFile();
            var groupedData = GetGroupedData(data);
            DisplayData(groupedData);
            Console.WriteLine("Press any key to exit.");
            Console.Read();
        }

        private static List<PetOwners> DownloadData(string url)
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<List<PetOwners>>(json_data) : new List<PetOwners>();
            }
        }

        private static List<PetOwners> GetDataFromFile()
        {            
            using (StreamReader r = new StreamReader("people.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<PetOwners>>(json);
            }
        }

        private static List<GroupedData> GetGroupedData(List<PetOwners> doenloadedData)
        {
            return doenloadedData.GroupBy(x => x.gender).Select(g => new GroupedData
            {
                OwnerGender = g.Key,
                Cats = g.Where(i => i.pets != null).SelectMany(o => o.pets.Where(p => p.type.Equals("Cat")).Select(c => c.name).ToList()).OrderBy(cn => cn).ToList()
            }).ToList();
        }

        private static void DisplayData(List<GroupedData> data) {
            Console.WriteLine();
            foreach(var g in data)
            {
                Console.WriteLine(g.OwnerGender);
                foreach(var c in g.Cats)
                {
                    Console.WriteLine("\t * " + c);
                }
            }
            Console.WriteLine();
        }
    }

    public class Pet
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class PetOwners
    {
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public List<Pet> pets { get; set; }
    }

    public class GroupedData {
        public string OwnerGender { get; set; }
        public List<string> Cats { get; set; }
    }
}
