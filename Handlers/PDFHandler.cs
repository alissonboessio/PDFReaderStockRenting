using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using PDFReaderStockRenting.Requests;
using PDFReaderStockRenting.Responses;

namespace PDFReaderStockRenting.Handlers
{
    public class PDFHandler
    {
        public FileResponse Process(FileRequest req)
        {
            Dictionary<string, List<PapelDoado>> resumo = new Dictionary<string, List<PapelDoado>>();
            decimal totalLiquido = 0;

            FileResponse ret = new FileResponse();
            
            if (req == null)
            {
                ret.message = "Nenhum arquivo encontrado!";
                ret.success = false;
                ret.totalLiquido = totalLiquido;

                return ret;
            }

            foreach (string fileName in req.files)
            {
                List<String> pdfRead = PdfTextExtractorTest.ExtractTextFromPdf(System.AppDomain.CurrentDomain.BaseDirectory + "\\notas\\" + fileName);

                foreach (string page in pdfRead)
                {
                    var initial = page.LastIndexOf("Valor Liquido") + 14;

                    totalLiquido += decimal.Parse(page.Substring(initial, page.Length - initial));
                }

            }

            ret.totalLiquido = totalLiquido;
            ret.success = true;
            ret.message = "Arquivos processados!";

             return ret;

        }
    }

    public static class PdfTextExtractorTest
    {
        public static List<string> ExtractTextFromPdf(string path)
        {
            List<string> textList = new List<string>();
            using (PdfReader pdfReader = new PdfReader(path))
            {
                PdfDocument pdfDoc = new PdfDocument(pdfReader);
                for (int pageNum = 1; pageNum <= pdfDoc.GetNumberOfPages(); pageNum++)
                {
                    LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                    string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(pageNum), strategy);
                    textList.Add(RemoveAccents(pageText));
                }
            }
            return textList;
        }

        static string RemoveAccents(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }

    public class PapelDoado 
    {
        public DateTime dataAbertura { get; set; }
        public DateTime dataEmissao{ get; set; }
        public DateTime dataVencimento { get; set; }
        public DateTime dataLiquidacao { get; set; }
        public decimal valorLiquidacao { get; set; }
        public decimal taxaEmprestimo { get; set; }
        public decimal  qtdOriginal { get; set; }
        public decimal valorBruto{ get; set; }
        public decimal valorEmolumentos{ get; set; }
        public decimal valorIRRF{ get; set; }
        public decimal valorExecucao{ get; set; }
        public decimal valorClearing{ get; set; }
        public decimal valorLiquido{ get; set; }
    }



}
