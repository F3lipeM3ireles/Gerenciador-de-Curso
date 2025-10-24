using System;
using System.IO;
using Gerenciador_de_Curso.Bussiness.Entities;
using iText.IO.Image;                            // imagens
using iText.Kernel.Colors;                       // cores 
using iText.Kernel.Geom;                         // PageSize
using iText.Kernel.Pdf;                          // PdfWriter / PdfDocument
using iText.Kernel.Pdf.Canvas;                   // PdfCanvas para desenhar borda
using iText.Layout;                              // Document
using iText.Layout.Element;                      // Paragraph, Image
using iText.Layout.Properties;                   // alinhamentos

namespace Gerenciador_de_Curso.Bussiness.Services
{
    //Aqui é onde o certificado realmente será gerado 
    public class CertificadoPdfService
    {
        private readonly string _imagensFolder;

        //construct para as imgs
        public CertificadoPdfService(string imagensFolder){
            _imagensFolder = imagensFolder ?? throw new ArgumentNullException(nameof(imagensFolder));
        }

        // aq vai gerra o PDF para retornar os Bytes NÃO MEXER PLMDS
        public byte[] GerarPdf(Context cert)
        {
            if (cert == null) throw new ArgumentNullException(nameof(cert));

            using var ms = new MemoryStream(); //gera o PDF em memória(mais fácil para retornar via API).
            var writer = new PdfWriter(ms); // essa e a document é para criar e organizar o cer tif.
            var pdf = new PdfDocument(writer);
            var pageSize = PageSize.A4;
            var page = pdf.AddNewPage(pageSize);

            // Todo esse monte de código só para a borda azul, sdds do meu CSS 
            var canvas = new PdfCanvas(page);
            canvas.SetStrokeColor(ColorConstants.BLUE);
            canvas.SetLineWidth(6);
            float margin = 20f; // Fahrenheit
            float x = margin;
            float y = margin;
            float width = pageSize.GetWidth() - margin * 2;
            float height = pageSize.GetHeight() - margin * 2;
            canvas.Rectangle(x, y, width, height);
            canvas.Stroke();

            // config do layout
            var document = new Document(pdf, pageSize);
            document.SetMargins(60, 40, 60, 40);

            try
            {
                var logoPath = System.IO.Path.Combine(_imagensFolder, "logo.png");
                if (File.Exists(logoPath))
                {
                    var logo = new Image(ImageDataFactory.Create(logoPath));
                    logo.SetWidth(120);
                    logo.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    document.Add(logo);
                }
            }
            catch { }
            document.Add(new Paragraph("\n"));

            var titulo = new Paragraph("CERTIFICADO")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(30)
                .SetFontColor(ColorConstants.BLUE);
           
            document.Add(titulo);

            document.Add(new Paragraph("\n\n"));

            string texto = $"Certificamos que {cert.NomeAluno} " +
                $"concluiu com êxito o curso {cert.NomeCurso}, " +
                $"com carga horária total de {cert.CargaHoraria} horas, " +
                $"realizado no periodo de {cert.Inicio:dd/MM/yyyy} a {cert.Termino:dd/MM/yyyy}.";
            var corpo = new Paragraph(texto)
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetFontSize(14)
                .SetMarginTop(10)
                .SetMarginBottom(10);
            document.Add(corpo);
            document.Add(new Paragraph("\n\n\n"));

            // Assinatura 
            try
            {
                var assinaturaPath = System.IO.Path.Combine(_imagensFolder, "assinatura.png");
                if (File.Exists(assinaturaPath))
                {
                    var assinatura = new Image(ImageDataFactory.Create(assinaturaPath));
                    assinatura.SetWidth(150);
                    assinatura.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                    document.Add(assinatura);
                }
            }
            catch { }
            var nomeGestor = new Paragraph("Gestor da Eficaz")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(12)
                .SetFontColor(ColorConstants.DARK_GRAY);
            document.Add(nomeGestor);
            document.Close();

            return ms.ToArray();
        }
    } 

}
