using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace KMCI_System.PurchasingModule.PurchaseRequestModule
{
    public class PurchaseRequestPDFGenerator
    {
        public static void GeneratePR(string prNumber, string vendorName, string projectCode)
        {
            try
            {
                // Create SaveFileDialog
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Save Purchase Requisition",
                    FileName = $"{prNumber}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                // Create document
                Document document = new Document(PageSize.LETTER, 36, 36, 36, 36);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveDialog.FileName, FileMode.Create));
                document.Open();

                // Fonts
                iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                iTextSharp.text.Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                iTextSharp.text.Font smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 9);
                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                // Header Section with Logo and PR Number
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 70, 30 });

                // Logo cell (you can add your logo image here)
                PdfPCell logoCell = new PdfPCell(new Phrase("KINGLAND\nMARKETING COMPANY INC.", labelFont));
                logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerTable.AddCell(logoCell);

                // PR Number cell
                PdfPCell prCell = new PdfPCell(new Phrase($"PR No. {prNumber}", normalFont));
                prCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                prCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                prCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerTable.AddCell(prCell);

                document.Add(headerTable);
                document.Add(new Paragraph(" "));

                // Title
                Paragraph title = new Paragraph("PURCHASE REQUISITION", headerFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 10;
                document.Add(title);

                // Info Section
                PdfPTable infoTable = new PdfPTable(4);
                infoTable.WidthPercentage = 100;
                infoTable.SetWidths(new float[] { 10, 40, 25, 25 });

                // Row 1
                infoTable.AddCell(CreateInfoCell("To:", labelFont, false));
                infoTable.AddCell(CreateInfoCell("Purchasing & Accounting", normalFont, false));
                infoTable.AddCell(CreateInfoCell("From", labelFont, true));
                infoTable.AddCell(CreateInfoCell("Date Requested", labelFont, true));

                // Row 2
                infoTable.AddCell(CreateInfoCell("", normalFont, false));
                infoTable.AddCell(CreateInfoCell("", normalFont, false));
                infoTable.AddCell(CreateInfoCell("Sales", normalFont, true));
                infoTable.AddCell(CreateInfoCell(DateTime.Now.ToString("M/d/yyyy"), normalFont, true));

                document.Add(infoTable);
                document.Add(new Paragraph(" "));

                // Required for section
                PdfPTable reqTable = new PdfPTable(2);
                reqTable.WidthPercentage = 100;
                reqTable.SetWidths(new float[] { 20, 80 });

                reqTable.AddCell(CreateInfoCell("Required for:", labelFont, false));

                string requiredFor = !string.IsNullOrEmpty(projectCode)
                    ? $"Client Ordered - {projectCode}"
                    : "Client Ordered - Monthly Usage";

                iTextSharp.text.Font redFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.RED);
                PdfPCell reqForCell = new PdfPCell(new Phrase(requiredFor, redFont));
                reqForCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                reqForCell.Padding = 5;
                reqForCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                reqTable.AddCell(reqForCell);

                document.Add(reqTable);
                document.Add(new Paragraph(" "));

                // Supplier
                Paragraph supplier = new Paragraph($"Supplier:        {vendorName}", labelFont);
                supplier.SpacingAfter = 10;
                document.Add(supplier);

                // Items Table - Changed to 4 columns
                PdfPTable itemsTable = new PdfPTable(4);
                itemsTable.WidthPercentage = 100;
                itemsTable.SetWidths(new float[] { 20, 15, 15, 50 });

                // Table Headers
                itemsTable.AddCell(CreateTableHeaderCell("Item #"));
                itemsTable.AddCell(CreateTableHeaderCell("Qty"));
                itemsTable.AddCell(CreateTableHeaderCell("Unit"));
                itemsTable.AddCell(CreateTableHeaderCell("Description"));

                // Get items from database
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            qi.sku_upc,
                            qi.quantity,
                            p.prod_name,
                            p.uom
                        FROM quotation_items qi
                        INNER JOIN quotation q ON qi.quotation_id = q.quotation_id
                        INNER JOIN product_list p ON qi.sku_upc = p.sku_upc 
                            AND p.pref_vendor = @vendor_name
                        WHERE qi.pref_vendor = @vendor_name
                        AND q.status = 'Approved'";

                    if (!string.IsNullOrEmpty(projectCode))
                    {
                        query += " AND q.project_code = @project_code";
                    }

                    query += " GROUP BY qi.sku_upc, qi.quantity, p.prod_name, p.uom ORDER BY p.prod_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@vendor_name", vendorName);
                        if (!string.IsNullOrEmpty(projectCode))
                        {
                            cmd.Parameters.AddWithValue("@project_code", projectCode);
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string sku = reader["sku_upc"]?.ToString() ?? "";
                                string qty = reader["quantity"]?.ToString() ?? "0";
                                string unit = reader["uom"]?.ToString() ?? "";
                                string description = reader["prod_name"]?.ToString() ?? "";

                                itemsTable.AddCell(CreateTableCell(sku, smallFont));
                                itemsTable.AddCell(CreateTableCell(qty, smallFont, Element.ALIGN_CENTER));
                                itemsTable.AddCell(CreateTableCell(unit, smallFont, Element.ALIGN_CENTER));
                                itemsTable.AddCell(CreateTableCell(description, smallFont));
                            }
                        }
                    }
                }

                // Add empty rows to fill the page - Changed to 4 columns
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        itemsTable.AddCell(CreateTableCell("", smallFont));
                    }
                }

                document.Add(itemsTable);
                document.Add(new Paragraph(" "));

                // Remarks Box Section
                PdfPTable remarksTable = new PdfPTable(1);
                remarksTable.WidthPercentage = 100;
                remarksTable.SpacingBefore = 10;
                remarksTable.SpacingAfter = 10;

                // Remarks label and content cell
                PdfPCell remarksCell = new PdfPCell();
                remarksCell.Border = iTextSharp.text.Rectangle.BOX;
                remarksCell.Padding = 10;
                remarksCell.MinimumHeight = 80f;

                Paragraph remarksContent = new Paragraph();
                remarksContent.Add(new Phrase("Remarks:\n\n", labelFont));
                remarksContent.Add(new Phrase("", normalFont)); // Empty space for writing

                remarksCell.AddElement(remarksContent);
                remarksTable.AddCell(remarksCell);

                document.Add(remarksTable);

                // Signature Section
                PdfPTable signatureTable = new PdfPTable(3);
                signatureTable.WidthPercentage = 100;
                signatureTable.SetWidths(new float[] { 33, 34, 33 });

                signatureTable.AddCell(CreateSignatureCell("Prepared by:", "#REF!"));
                signatureTable.AddCell(CreateSignatureCell("Requested and Checked by:", "#REF!"));
                signatureTable.AddCell(CreateSignatureCell("Approved by:", ""));

                document.Add(signatureTable);

                // Close document
                document.Close();
                writer.Close();

                MessageBox.Show($"Purchase Requisition PDF generated successfully!\n\nSaved to: {saveDialog.FileName}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the PDF
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static PdfPCell CreateInfoCell(string text, iTextSharp.text.Font font, bool hasBorder)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Border = hasBorder ? iTextSharp.text.Rectangle.BOX : iTextSharp.text.Rectangle.NO_BORDER;
            cell.Padding = 5;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }

        private static PdfPCell CreateTableHeaderCell(string text)
        {
            iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            PdfPCell cell = new PdfPCell(new Phrase(text, headerFont));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            cell.Border = iTextSharp.text.Rectangle.BOX;
            return cell;
        }

        private static PdfPCell CreateTableCell(string text, iTextSharp.text.Font font, int alignment = Element.ALIGN_LEFT)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            cell.Border = iTextSharp.text.Rectangle.BOX;
            cell.MinimumHeight = 20f;
            return cell;
        }

        private static PdfPCell CreateSignatureCell(string label, string name)
        {
            iTextSharp.text.Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.ITALIC);
            iTextSharp.text.Font nameFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            Paragraph p = new Paragraph();
            p.Add(new Phrase(label + "\n\n\n", labelFont));
            p.Add(new Phrase("_________________________\n", nameFont));
            p.Add(new Phrase(name, nameFont));

            PdfPCell cell = new PdfPCell(p);
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingTop = 20;
            return cell;
        }

        private static string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}