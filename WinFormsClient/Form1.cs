using System.Xml;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace WinFormsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WebRequest();
        }

        private async void WebRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://xiaomirepair.net/");

                if (response.IsSuccessStatusCode)
                {
                    string htmlContent = await response.Content.ReadAsStringAsync();

                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(htmlContent);

                    HtmlNode ortaPanelDiv = document.DocumentNode.SelectSingleNode("//div[@class='ortapanel']");
                    if (ortaPanelDiv != null)
                    {
                        var imgsTags = ortaPanelDiv.SelectNodes(".//img");
                        if (imgsTags != null)
                        {
                            foreach (var imgTag in imgsTags)
                            {
                                imgTag.Remove();
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Could not find the specified div.");
                    }
                    string? modifiedInnerHtml = ortaPanelDiv?.InnerHtml;

                    richTextBox1.Text = modifiedInnerHtml;



                }
                else
                {
                    MessageBox.Show("HTTP request failed.");
                }
            }
        }
    }
}