using RoutePackage.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoutePackage.Services
{
    public class ServicioRest
    {

        public async Task<SearchCategories> GetCategories()
        {

            var categories = new SearchCategories();

            try
            {
                using (var httpClient = new HttpClient())
                {

                    HttpResponseMessage llamada = await httpClient.GetAsync("http://meraki.esy.es/xamarin/api.php/searchcategory?transform=1").ConfigureAwait(false);

                    if (llamada.IsSuccessStatusCode)
                    {

                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        categories = JsonConvert.DeserializeObject<SearchCategories>(json);

                        foreach (var item in categories.searchcategory)
                        {
                            item.Name = item.Name.ToUpper();
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                categories = null;
            }

            return categories;
        }

        public async Task<SearchPlaces> GetPlacesByCategoryId(int categorid)
        {

            var places = new SearchPlaces();

            try
            {
                using (var httpClient = new HttpClient())
                {

                    HttpResponseMessage llamada = await httpClient.GetAsync("http://meraki.esy.es/xamarin/api.php/searchplace?transform=1&filter=categoryid,eq," + categorid.ToString()).ConfigureAwait(false);

                    if (llamada.IsSuccessStatusCode)
                    {

                        var json = await llamada.Content.ReadAsStringAsync().ConfigureAwait(false);

                        places = JsonConvert.DeserializeObject<SearchPlaces>(json);

                    }
                }
            }
            catch (Exception ex)
            {
                places = null;
            }

            return places;
        }
    }
}
