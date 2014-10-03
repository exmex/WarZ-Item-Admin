using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WarZLocal_Admin
{
    public partial class ImportURL : Form
    {
        public ImportURL()
        {
            InitializeComponent();
        }

        private void ImportURL_Load(object sender, EventArgs e)
        {
            textBox1.LostFocus += (EventHandler) ((o, ev) =>
            {
                var request = HttpWebRequest.Create(textBox1.Text) as HttpWebRequest;
                if (request != null)
                {
                    var response = request.GetResponse() as HttpWebResponse;

                    string contentType = "";

                    if (response != null)
                        contentType = response.ContentType;

                    if (contentType != "text/xml")
                    {
                        errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleRight);
                        errorProvider1.SetError(textBox1, "URL is invalid!\nMust be an XML File");
                    }
                    else
                        errorProvider1.SetError(textBox1, "");
                }
            });
        }

        public string importURL = "";
        private void button1_Click(object sender, EventArgs e)
        {
            var request = HttpWebRequest.Create(textBox1.Text) as HttpWebRequest;
            if (request != null)
            {
                var response = request.GetResponse() as HttpWebResponse;

                string contentType = "";

                if (response != null)
                    contentType = response.ContentType;

                if (contentType != "text/xml")
                {
                    errorProvider1.SetIconAlignment(textBox1, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError(textBox1, "URL is invalid!\nMust be an XML File");
                }
                else
                {
                    errorProvider1.SetError(textBox1, "");
                    importURL = textBox1.Text;
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
