using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.UI;
using System.Collections.Generic;
using WoWinfo.Models;
using System.Json;


namespace WoWinfo
{
    public partial class _Default : Page
    {
        private static bool filled1 = false;
        private static bool filled2 = false;

        private static string selectedRegion;
        private static string selectedRealm;
        private static string name;
        private static string locale;
        private static string namesp;

        private static List<Region> regionList = new List<Region>()
            {
                new Region("United States", "us"),
                new Region("Europe", "eu"),
                new Region("Korea", "kr"),
                new Region("Taiwan", "tw"),
                new Region("China", "cn")
            };

        public _Default()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // LOAD REGIONS
            
            if (!filled1)
            {
                foreach (Region region in regionList)
                {
                    DropDownList1.Items.Add(region.Name);
                }

                filled1 = true;
            }

        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            filled2 = false;

            // LOAD REALMS

            if (DropDownList1.SelectedItem.Text == DropDownList1.Items[0].ToString())
            {
                string script = "alert('Please, select a region.');";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnInfo, this.GetType(), "Test", script, true);
                DropDownList2.Items.Clear();
                DropDownList1.Items.Clear();
                filled1 = false;
                filled2 = false;
                Response.Redirect(Request.RawUrl);
            }

            string apiURL = "";

            foreach (Region region in regionList)
            {
                if (DropDownList1.SelectedItem.Text == region.Name)
                {
                    selectedRegion = region.Code;
                }
            }

            if (selectedRegion == "cn")
            {
                apiURL = $"https://gateaway.battlenet.com.{selectedRegion}/data/wow/search/connected-realm?namespace=dynamic-{selectedRegion}&locale=en_GB&access_token=US5M8P89Akh0u5QLT1WzuxvPGZx0q5nPU5";

            }
            else
            {
                apiURL = $"https://{selectedRegion}.api.blizzard.com/data/wow/search/connected-realm?namespace=dynamic-{selectedRegion}&locale=en_GB&access_token=US5M8P89Akh0u5QLT1WzuxvPGZx0q5nPU5";
            }

            var rootRealm = new RootRealm();

            try
            {
                using (WebClient client = new WebClient())
                {
                    var realmInfo = client.DownloadString(apiURL);
                    rootRealm = JsonConvert.DeserializeObject<RootRealm>(realmInfo);
                }
            }
            catch (Exception ex)
            {
                Session["ErrorMessage"] = ex.ToString();
                Response.Redirect("ErrorDisplay.aspx");
            }


            var realmList = new List<string>();


            foreach (Results result in rootRealm.results)
            {
                foreach (Realms realm in result.data.realms)
                {
                    realmList.Add(realm.Slug);

                }
            }

            realmList.Sort();

            if (!filled2)
            {
                foreach (string realm in realmList)
                {
                    DropDownList2.Items.Add(realm);
                }

                filled2 = true;
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedRealm = DropDownList2.SelectedItem.Text;
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox5_TextChanged(object sender, EventArgs e) //Locale
        {
            locale = TextBox5.Text;
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e) //Character Name
        {
            name = TextBox4.Text;
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e) //Namespace
        {
            namesp = TextBox6.Text;
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {

            if (name != "" && locale != "" && namesp != "")
            {
                string apiURL = $"https://{selectedRegion}.api.blizzard.com/profile/wow/character/{selectedRealm}/{name}?namespace={namesp}&locale={locale}&access_token=US5M8P89Akh0u5QLT1WzuxvPGZx0q5nPU5";

                Character character = new Character();

                try
                {
                    using (WebClient client = new WebClient())
                    {
                        var info = client.DownloadString(apiURL);
                        character = JsonConvert.DeserializeObject<Character>(info);
                    }
                }
                catch (Exception ex)
                {
                    Session["ErrorMessage"] = ex.ToString();
                    Response.Redirect("ErrorDisplay.aspx");
                }

                TextBox1.Text = character.Name;
                TextBox2.Text = character.Level.ToString();
                TextBox3.Text = character.ID.ToString();
            }
            else
            {
                string script = "alert('Please, fill the required fields.');";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(btnInfo, this.GetType(), "Test", script, true);
            }

            
        }


    }
}