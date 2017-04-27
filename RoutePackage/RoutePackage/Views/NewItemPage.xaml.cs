using System;
using Xamarin.Forms.Maps;
using RoutePackage.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RoutePackage.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        Dictionary<string, SearchCategory> SearchCategories = new Dictionary<string, SearchCategory>
        {
            { "Seleccione ..",new SearchCategory()}
        };

        public NewItemPage()
        {
            InitializeComponent();

            //FillCategories();

            //Lat.Text = point.Latitude.ToString();
            //Long.Text = point.Longitude.ToString();
            //Item = new Item
            //{
            //    Text = "Item name",
            //    Description = "This is a nice description"
            //};

            //BindingContext = this; 

        }

        public void Save_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "AddItem", Item);
            //await Navigation.PopToRootAsync();
        }

        async void FillCategories()
        {
            try
            {
                var service = new Services.ServicioRest();
                var cats = await service.GetCategories();

                if (cats.searchcategory.Any())
                {
                    foreach (var item in cats.searchcategory)
                    {
                        SearchCategories.Add(item.Id.ToString(), item);
                    }

                    foreach (var item in SearchCategories)
                    {
                        Categories.Items.Add(item.Key);
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }

        }

    }
}