using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Spire.Pdf;

namespace PdfConversion
{
    public class ConvertPdfComputation
    {
        private BackgroundWorker worker;
        public event Action ProgressBarform;
        public ConvertPdfComputation(BackgroundWorker bgw)
        {
            worker = bgw;
        }

        private void worker_Dowork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(5000);
            ParamArgs? paramArgs = e.Argument as ParamArgs;
            if (paramArgs != null)
                ConversionMethod(paramArgs);
            
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBarform?.Invoke();
            MessageBox.Show("Conversion Completed", "Feedback", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ConvertToDocx(ParamArgs param)
        {
            worker.DoWork += new DoWorkEventHandler(worker_Dowork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync(param);
        }
        private void ConversionMethod(ParamArgs args)
        {
            try
            {
                SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
                if(f.PageCount > 0)
                {
                    switch (args.typeName)
                    {
                        case "Docx":
                            {
                                f.ToWord(Path.Combine(@args.DestinationName, "ConvertedFile." + Form1._dictionary[args.typeName]));
                                break;
                            };
                        case "Images":
                            {
                                f.ImageOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                f.ImageOptions.Dpi = 200;
                                f.ToImage(@args.DestinationName, "Page");
                                break;
                            };
                        case "Text":
                            {
                                f.ToText(Path.Combine(@args.DestinationName, "ConvertedFile." + Form1._dictionary[args.typeName]));
                                break;
                            };
                        case "Xml":
                            {
                                f.XmlOptions.ConvertNonTabularDataToSpreadsheet = false;
                                f.ToXml(Path.Combine(@args.DestinationName, "ConvertedFile." + Form1._dictionary[args.typeName]));
                                break;
                            };
                    }
                }
                f.OpenPdf(args.SourceName);
                if (f.PageCount > 0)
                    f.ToWord(Path.Combine(args.DestinationName, "ConvertedFile.doc"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} - Please set the path to the file and/or destination path");
            }

        }
    }
}
