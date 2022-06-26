using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConversion
{
    public class ClientSide
    {
        
        FrmProgressBar? progressBar;
        private ConvertPdfComputation ConvertPdfComputation;

        public ClientSide(ConvertPdfComputation convertPdfComputation)
        {
            ConvertPdfComputation = convertPdfComputation;
        }

        public void RunAsync(string title, ParamArgs paramArgs)
        {
            ConvertPdfComputation.ProgressBarform += () =>
            {
                if (progressBar != null)
                    progressBar.Close();
            };
            Form? parentForm = null;
            progressBar = null;
            if (true)
            {

                progressBar = new FrmProgressBar();
                if (!string.IsNullOrEmpty(title))
                    progressBar.Text = title;

                if (Application.OpenForms.Count > 0)
                {
                    parentForm = Application.OpenForms[0];
                    progressBar.Show(parentForm);
                }
                else
                    progressBar.Show();
            }
            ConvertPdfComputation.ConvertToDocx(paramArgs);
        }
       
    }
}
