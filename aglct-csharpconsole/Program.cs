using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace aglct_csharpconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please wait, searching for cats...");
            var data = GetDataFromFile();
            var data1 = data.GroupBy(x => x.gender).Select(g => new {
                OwnerGender = g.Key,
                Cats = g.Where(i => i.pets != null).SelectMany(o => o.pets.Where(p => p.type.Equals("Cat")).Select(c => c.name).ToList()).OrderBy(cn => cn).ToList()
            }).ToList();
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
}
