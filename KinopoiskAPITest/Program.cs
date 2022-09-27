using MovieAPIs.UnofficialKinopoiskApi.Models;
using System.Numerics;

namespace KinopoiskAPITest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ApiProcessor.InitializeClient("b049b2fb-f0f7-48d6-86f7-7e8c36348dcf"); //Your apiKey here

            List<int> filmIds = (await ApiProcessor.GetDramaFilmsIdsAsync()).ToList();

            List<int> staffIds = new List<int>();
            for (int i = 0; i < filmIds.Count; i++)
            {
                staffIds.AddRange(await ApiProcessor.GetStaffIdsAsync(filmIds[i]));
            }

            List<PersonModel> people = new List<PersonModel>();
            for (int i = 0; i < staffIds.Count; i++)
            {
                people.Add(await ApiProcessor.GetPersonDetailsAsync(staffIds[i]));
            }

            Console.WriteLine(people.Where(i => i.Sex == "MALE").MinBy(i => i.Age).Name);
            Console.WriteLine(people.Where(i => i.Sex == "FEMALE").MinBy(i => i.Age).Name);
            var test = await ApiProcessor.GetStaffIdsAsync(301);
        }
    }
}