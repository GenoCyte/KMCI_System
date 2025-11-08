using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace KMCI_System.PurchasingModule.PurchaseOrderModule
{
    public class PurchaseOrderPDFGenerator
    {
        private static readonly BaseColor KMCIBlue = new BaseColor(0, 174, 239);
        private static readonly BaseColor LightGray = new BaseColor(240, 240, 240);

        public static void GeneratePO(string poNumber, string vendorName, string prNumber, string? projectCode = null)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    Title = "Save Purchase Order",
                    FileName = $"{poNumber}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                };

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                Document document = new Document(PageSize.LETTER, 36, 36, 36, 36);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveDialog.FileName, FileMode.Create));
                document.Open();

                // Fonts
                iTextSharp.text.Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, KMCIBlue);
                iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, BaseColor.WHITE);
                iTextSharp.text.Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                iTextSharp.text.Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                iTextSharp.text.Font smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 9);
                iTextSharp.text.Font largeFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                iTextSharp.text.Font thankyouFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, KMCIBlue);

                // Header with Logo and Date
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.WidthPercentage = 100;
                headerTable.SetWidths(new float[] { 70, 30 });

                // Logo section
                PdfPCell logoCell = new PdfPCell();
                logoCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                Paragraph logoPara = new Paragraph();
                logoPara.Add(new Chunk("KINGLAND\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, KMCIBlue)));
                logoPara.Add(new Chunk("MARKETING COMPANY INC.", FontFactory.GetFont(FontFactory.HELVETICA, 10, KMCIBlue)));
                logoCell.AddElement(logoPara);
                headerTable.AddCell(logoCell);

                // Date section
                PdfPCell dateCell = new PdfPCell();
                dateCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                Paragraph datePara = new Paragraph();
                datePara.Add(new Chunk(DateTime.Now.ToString("MMMM dd, yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 11, KMCIBlue)));
                datePara.Add(new Chunk("\nPurchase Order Date", FontFactory.GetFont(FontFactory.HELVETICA, 9)));
                dateCell.AddElement(datePara);
                headerTable.AddCell(dateCell);

                document.Add(headerTable);
                document.Add(new Paragraph(" "));

                // PO Title Bar
                PdfPTable titleTable = new PdfPTable(1);
                titleTable.WidthPercentage = 100;
                PdfPCell titleCell = new PdfPCell(new Phrase($"PURCHASE ORDER   #                    {poNumber}", titleFont));
                titleCell.BackgroundColor = KMCIBlue;
                titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
                titleCell.Padding = 10;
                titleCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                titleTable.AddCell(titleCell);
                document.Add(titleTable);
                document.Add(new Paragraph(" ", normalFont));

                // Supplier and Sold/Invoice To Section
                PdfPTable detailsTable = new PdfPTable(2);
                detailsTable.WidthPercentage = 100;
                detailsTable.SetWidths(new float[] { 50, 50 });

                // Get vendor and company details from database
                string vendorAddress = "", vendorAttn = "", vendorContact = "";
                string companyName = "Kingland Marketing Company Inc.";
                string companyAddress = "Phase 4B Blk 7 Lot 28 Golden City, Dila,\nCity of Santa Rosa, Laguna, Philippines 4026";
                string companyTin = "645-630-230-000";
                string contactPerson = "Katrina M. Abanilla";
                string contactNumber = "0956-457-2521";
                string deliveryAddress = "#930 Mamafid Road\nCabuyao, Laguna";

                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT vendor_address, vendor_phone, vendor_email, vendor_person FROM vendor_list WHERE vendor_name = @vendor_name LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@vendor_name", vendorName);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                vendorAddress = reader["vendor_address"]?.ToString() ?? "";
                                vendorContact = reader["vendor_phone"]?.ToString() ?? "";
                                vendorAttn = reader["vendor_person"]?.ToString() ?? "N/A";
                            }
                        }
                    }
                }

                // Left column - Supplier Details
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                leftCell.PaddingRight = 10;

                Paragraph supplierPara = new Paragraph();
                supplierPara.Add(new Chunk("Supplier Details\n", labelFont));
                supplierPara.Add(new Chunk($"Company Name: ", normalFont));
                supplierPara.Add(new Chunk($"{vendorName}\n", labelFont));
                supplierPara.Add(new Chunk($"Address:\n{vendorAddress}\n\n", normalFont));
                supplierPara.Add(new Chunk($"Tin:\n", normalFont));
                supplierPara.Add(new Chunk($"Attn: ", normalFont));
                supplierPara.Add(new Chunk($"{vendorAttn}\n", normalFont));
                supplierPara.Add(new Chunk($"Contact No.: ", normalFont));
                supplierPara.Add(new Chunk($"{vendorContact}", normalFont));

                leftCell.AddElement(supplierPara);
                detailsTable.AddCell(leftCell);

                // Right column - Sold/Invoice To
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                Paragraph invoicePara = new Paragraph();
                invoicePara.Add(new Chunk("Sold / Invoice to:\n", labelFont));
                invoicePara.Add(new Chunk($"Company Name:                 ", normalFont));
                invoicePara.Add(new Chunk($"{companyName}\n", labelFont));
                invoicePara.Add(new Chunk($"Address:                              {companyAddress}\n\n", normalFont));
                invoicePara.Add(new Chunk($"Tin:                                       {companyTin}\n\n", normalFont));
                invoicePara.Add(new Chunk("from PR Number                    ", normalFont));

                rightCell.AddElement(invoicePara);

                // PR Number in yellow box
                PdfPTable prBox = new PdfPTable(1);
                prBox.WidthPercentage = 50;
                prBox.HorizontalAlignment = Element.ALIGN_RIGHT;
                PdfPCell prCell = new PdfPCell(new Phrase(prNumber, labelFont));
                prCell.BackgroundColor = BaseColor.YELLOW;
                prCell.Padding = 5;
                prCell.HorizontalAlignment = Element.ALIGN_CENTER;
                prBox.AddCell(prCell);
                rightCell.AddElement(prBox);

                detailsTable.AddCell(rightCell);
                document.Add(detailsTable);
                document.Add(new Paragraph(" "));

                // Shipped/Delivered To Section
                PdfPTable shippingTable = new PdfPTable(4);
                shippingTable.WidthPercentage = 100;
                shippingTable.SetWidths(new float[] { 25, 25, 25, 25 });

                // Headers
                shippingTable.AddCell(CreateBlueHeaderCell("Shipped / Delivered to"));
                shippingTable.AddCell(CreateBlueHeaderCell("Contact"));
                shippingTable.AddCell(CreateBlueHeaderCell("PAYMENT"));
                shippingTable.AddCell(CreateBlueHeaderCell("PICKUP/DELIVERY DATE"));

                // Values
                shippingTable.AddCell(CreateNormalCell($"Name:             {companyName}\nAddress:         {deliveryAddress}"));
                shippingTable.AddCell(CreateNormalCell(contactPerson + "\n" + contactNumber));
                shippingTable.AddCell(CreateNormalCell("COD"));
                shippingTable.AddCell(CreateNormalCell("September 30, 2025"));

                document.Add(shippingTable);
                document.Add(new Paragraph(" ", smallFont));

                // Items Table
                PdfPTable itemsTable = new PdfPTable(6);
                itemsTable.WidthPercentage = 100;
                itemsTable.SetWidths(new float[] { 18, 35, 12, 12, 12, 15 });

                // Headers
                itemsTable.AddCell(CreateBlueHeaderCell("ITEM NO."));
                itemsTable.AddCell(CreateBlueHeaderCell("DESCRIPTION"));
                itemsTable.AddCell(CreateBlueHeaderCell("QTY"));
                itemsTable.AddCell(CreateBlueHeaderCell("Unit of Measure"));
                itemsTable.AddCell(CreateBlueHeaderCell("UNIT PRICE"));
                itemsTable.AddCell(CreateBlueHeaderCell("TOTAL"));

                // Get items from database
                decimal subtotal = 0;
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            pri.sku_upc,
                            pri.quantity,
                            pri.unit_price,
                            pri.sub_total,
                            p.prod_name,
                            p.uom AS unit
                        FROM purchase_order_items pri
                        INNER JOIN purchase_order po ON pri.po_id = po.id
                        INNER JOIN product_list p ON pri.sku_upc = p.sku_upc 
                            AND p.pref_vendor = @vendor_name
                        WHERE po.vendor_name = @vendor_name
                        AND po.po_name = @pr_name
                        ORDER BY p.prod_name";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@vendor_name", vendorName);
                        cmd.Parameters.AddWithValue("@pr_name", prNumber);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string sku = reader["sku_upc"]?.ToString() ?? "";
                                string description = reader["prod_name"]?.ToString() ?? "";
                                int qty = Convert.ToInt32(reader["quantity"]);
                                string unit = reader["unit"]?.ToString() ?? "";
                                decimal unitPrice = Convert.ToDecimal(reader["unit_price"]);
                                decimal total = Convert.ToDecimal(reader["sub_total"]);

                                subtotal += total;

                                itemsTable.AddCell(CreateItemCell(sku));
                                itemsTable.AddCell(CreateItemCell(description));
                                itemsTable.AddCell(CreateItemCell(qty.ToString(), Element.ALIGN_CENTER));
                                itemsTable.AddCell(CreateItemCell(unit, Element.ALIGN_CENTER));
                                itemsTable.AddCell(CreateItemCell($"₱{unitPrice:N2}", Element.ALIGN_RIGHT));
                                itemsTable.AddCell(CreateItemCell($"₱{total:N2}", Element.ALIGN_RIGHT));
                            }
                        }
                    }
                }

                // Add empty rows
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        itemsTable.AddCell(CreateItemCell(""));
                    }
                }

                document.Add(itemsTable);
                document.Add(new Paragraph(" "));

                // Remarks and Totals Section
                PdfPTable bottomTable = new PdfPTable(2);
                bottomTable.WidthPercentage = 100;
                bottomTable.SetWidths(new float[] { 55, 45 });

                // Remarks cell
                PdfPCell remarksCell = new PdfPCell();
                remarksCell.Border = iTextSharp.text.Rectangle.BOX;
                remarksCell.MinimumHeight = 100;
                Paragraph remarksPara = new Paragraph("Remarks / Instructions:", normalFont);
                remarksCell.AddElement(remarksPara);
                bottomTable.AddCell(remarksCell);

                // Totals cell
                PdfPCell totalsCell = new PdfPCell();
                totalsCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                decimal taxRate = 0.12m;
                decimal vatInput = subtotal * taxRate;
                decimal amountNetVat = subtotal - vatInput;
                decimal shipping = 0;
                decimal discount = 0;
                decimal totalAmount = subtotal + shipping - discount;

                PdfPTable totalsTable = new PdfPTable(2);
                totalsTable.WidthPercentage = 100;
                totalsTable.SetWidths(new float[] { 60, 40 });

                totalsTable.AddCell(CreateTotalLabelCell("SUBTOTAL"));
                totalsTable.AddCell(CreateTotalValueCell($"₱{subtotal:N2}"));

                totalsTable.AddCell(CreateTotalLabelCell("TAX RATE"));
                totalsTable.AddCell(CreateTotalValueCell($"{taxRate * 100}%"));

                totalsTable.AddCell(CreateTotalLabelCell("VAT INPUT TAX"));
                totalsTable.AddCell(CreateTotalValueCell($"₱{vatInput:N2}"));

                totalsTable.AddCell(CreateTotalLabelCell("AMOUNT (NET OF VAT)"));
                totalsTable.AddCell(CreateTotalValueCell($"₱{amountNetVat:N2}"));

                totalsTable.AddCell(CreateTotalLabelCell("SHIPPING/HANDLING"));
                totalsTable.AddCell(CreateTotalValueCell($"₱{shipping:N2}"));

                totalsTable.AddCell(CreateTotalLabelCell("OTHER (Discount)"));
                totalsTable.AddCell(CreateTotalValueCell($"₱{discount:N2}"));

                PdfPCell totalLabelCell = new PdfPCell(new Phrase("TOTAL AMOUNT", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.WHITE)));
                totalLabelCell.BackgroundColor = KMCIBlue;
                totalLabelCell.Padding = 8;
                totalLabelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalsTable.AddCell(totalLabelCell);

                PdfPCell totalValueCell = new PdfPCell(new Phrase($"₱{totalAmount:N2}", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.WHITE)));
                totalValueCell.BackgroundColor = KMCIBlue;
                totalValueCell.Padding = 8;
                totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                totalsTable.AddCell(totalValueCell);

                totalsCell.AddElement(totalsTable);
                bottomTable.AddCell(totalsCell);

                document.Add(bottomTable);
                document.Add(new Paragraph(" "));

                // Thank You
                Paragraph thankYou = new Paragraph("THANK YOU", thankyouFont);
                thankYou.Alignment = Element.ALIGN_LEFT;
                document.Add(thankYou);
                document.Add(new Paragraph(" "));

                // Signature Section
                PdfPTable signatureTable = new PdfPTable(2);
                signatureTable.WidthPercentage = 100;
                signatureTable.SetWidths(new float[] { 50, 50 });

                signatureTable.AddCell(CreateSignatureHeaderCell("Approved by:"));
                signatureTable.AddCell(CreateSignatureHeaderCell("Checked by:"));

                signatureTable.AddCell(CreateSignatureCell("Richard A. Abanilla", "President & CEO"));
                signatureTable.AddCell(CreateSignatureCell("Katrina M. Abanilla", "Vice President & COO"));

                document.Add(signatureTable);
                document.Add(new Paragraph(" "));

                // Footer
                PdfPTable footerTable = new PdfPTable(2);
                footerTable.WidthPercentage = 100;
                footerTable.SetWidths(new float[] { 50, 50 });

                PdfPCell footerLeftCell = new PdfPCell();
                footerLeftCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                footerLeftCell.BackgroundColor = KMCIBlue;
                footerLeftCell.Padding = 10;
                Paragraph footerLeft = new Paragraph();
                footerLeft.Add(new Chunk("Phase 4B Block 7 Lot 28 Golden City, Dila\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.WHITE)));
                footerLeft.Add(new Chunk("City of Santa Rosa, Laguna\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.WHITE)));
                footerLeft.Add(new Chunk("Philippines 4026", FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.WHITE)));
                footerLeftCell.AddElement(footerLeft);
                footerTable.AddCell(footerLeftCell);

                PdfPCell footerRightCell = new PdfPCell();
                footerRightCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                footerRightCell.BackgroundColor = KMCIBlue;
                footerRightCell.Padding = 10;
                footerRightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                Paragraph footerRight = new Paragraph();
                footerRight.Add(new Chunk("For questions concerning this Purchase Order, please contact\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE)));
                footerRight.Add(new Chunk("Richard A. Abanilla, 0917-135-8805, raabanilla@kingland.ph\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE)));
                footerRight.Add(new Chunk("https://kingland.ph\n", FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE)));
                footerRight.Add(new Chunk("https://shop.kingland.ph", FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.WHITE)));
                footerRight.Alignment = Element.ALIGN_RIGHT;
                footerRightCell.AddElement(footerRight);
                footerTable.AddCell(footerRightCell);

                document.Add(footerTable);

                document.Close();
                writer.Close();

                MessageBox.Show($"Purchase Order PDF generated successfully!\n\nSaved to: {saveDialog.FileName}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open PDF with default application using ProcessStartInfo
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = saveDialog.FileName,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                catch (Exception openEx)
                {
                    MessageBox.Show($"PDF created successfully but couldn't open automatically.\n\nLocation: {saveDialog.FileName}\n\nError: {openEx.Message}",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}\n\n{ex.StackTrace}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static PdfPCell CreateBlueHeaderCell(string text)
        {
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.BackgroundColor = KMCIBlue;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 8;
            return cell;
        }

        private static PdfPCell CreateNormalCell(string text)
        {
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA, 9);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Padding = 5;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cell;
        }

        private static PdfPCell CreateItemCell(string text, int alignment = Element.ALIGN_LEFT)
        {
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA, 9);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            cell.MinimumHeight = 20f;
            return cell;
        }

        private static PdfPCell CreateTotalLabelCell(string text)
        {
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Padding = 5;
            return cell;
        }

        private static PdfPCell CreateTotalValueCell(string text)
        {
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.BackgroundColor = KMCIBlue;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Padding = 5;
            return cell;
        }

        private static PdfPCell CreateSignatureHeaderCell(string text)
        {
            iTextSharp.text.Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.BackgroundColor = KMCIBlue;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Padding = 8;
            return cell;
        }

        private static PdfPCell CreateSignatureCell(string name, string title)
        {
            iTextSharp.text.Font nameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
            iTextSharp.text.Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            Paragraph p = new Paragraph();
            p.Add(new Phrase("\n\n\n", nameFont));
            p.Add(new Phrase(name + "\n", nameFont));
            p.Add(new Phrase(title, titleFont));
            p.Alignment = Element.ALIGN_CENTER;

            PdfPCell cell = new PdfPCell(p);
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.MinimumHeight = 60;
            return cell;
        }

        private static string GetConnectionString()
        {
            return "Server=localhost;Database=kmci_database;Uid=root;Pwd=;";
        }
    }
}