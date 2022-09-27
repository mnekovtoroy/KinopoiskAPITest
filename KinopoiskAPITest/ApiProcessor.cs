using MovieAPIs.Common.Responses;
using MovieAPIs.UnofficialKinopoiskApi;
using MovieAPIs.UnofficialKinopoiskApi.Models;

namespace KinopoiskAPITest
{
    public static class ApiProcessor
    {
        public static UnofficialKinopoiskApiClient ApiClient { get; set; }

        public static void InitializeClient(string apiKey)
        {
            ApiClient = new UnofficialKinopoiskApiClient(apiKey);
        }

        public static async Task<int[]> GetDramaFilmsIdsAsync()
        {
            /*var films = await ApiClient.GetFilmsByFiltersAsync(
                countryId: 1, //USA 
                genreId: 2, //Drama
                type: MovieType.FILM
                );*/
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("X-API-KEY", "b049b2fb-f0f7-48d6-86f7-7e8c36348dcf");
            ItemsResponseWithPagesCount<Film> films = new ItemsResponseWithPagesCount<Film>();

            using (var response = await client.GetAsync("https://kinopoiskapiunofficial.tech/api/v2.2/films?countries=1&genres=2&type=FILM"))
            {
                if(response.IsSuccessStatusCode)
                {
                    films = await response.Content.ReadAsAsync<ItemsResponseWithPagesCount<Film>>();
                }
            }

            int[] top5Ids = new int[5];
            for (int i = 0; i < 5; i++)
            {
                top5Ids[i] = films.Items.ElementAt(i).KinopoiskId;
            }
            return top5Ids;
        }

        public static async Task<int[]> GetStaffIdsAsync(int filmId)
        {
            var staff = await ApiClient.GetStaffByFilmIdAsync(filmId);
            int[] staffIds = new int[staff.Length];
            for (int i = 0; i < staff.Length; i++)
            {
                staffIds[i] = staff.ElementAt(i).StaffId;
            }
            return staffIds;
        }
        
        public static async Task<PersonModel> GetPersonDetailsAsync(int personId)
        {
            var personDetails = await ApiClient.GetStaffByPersonIdAsync(personId);
            var personModel = new PersonModel();
            personModel.Age = personDetails.Age;
            personModel.Name = personDetails.NameRu;
            personModel.Sex = personDetails.Sex;
            return personModel;
        }
    }
}
