using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;

namespace KMCI_System.PurchasingModule
{
    public class KinglandPOPdfGenerator
    {
        // Color definitions matching your template
        private readonly BaseColor primaryBlue = new BaseColor(0, 158, 227); // #009EE3
        private readonly BaseColor darkGray = new BaseColor(64, 64, 64);
        private readonly BaseColor lightGray = new BaseColor(242, 242, 242);

        public void GeneratePurchaseOrderPdf(int purchaseOrderId, string savePath, string? logoPath = null)
        {
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();

                // Get data from database
                var purchaseOrder = GetPurchaseOrderData(purchaseOrderId);

                // Add logo and header
                AddHeader(document, purchaseOrder, logoPath);

                // Add blue title bar
                AddTitleBar(document, purchaseOrder.PurchaseOrderNumber);

                // Add purchase order info section
                AddPurchaseOrderInfo(document, purchaseOrder);

                // Add shipping/delivery section
                AddShippingInfo(document, purchaseOrder);

                // Add items table
                AddItemsTable(document, purchaseOrder);

                // Add remarks section
                AddRemarksSection(document, purchaseOrder);

                // Add totals section
                AddTotalsSection(document, purchaseOrder);

                // Add footer with signatures
                AddFooter(document, purchaseOrder);

                document.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (document.IsOpen())
                    document.Close();
                throw; // Re-throw to allow caller to handle
            }
        }

        private void AddHeader(Document document, PurchaseOrderData purchaseOrder, string? logoPath)
        {
            PdfPTable headerTable = new PdfPTable(2);
            headerTable.WidthPercentage = 100;
            headerTable.SetWidths(new float[] { 3f, 2f });

            // Left side - Company logo and name
            PdfPCell leftCell = new PdfPCell();
            leftCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            leftCell.PaddingBottom = 15;

            if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
            {
                try
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(150f, 60f);
                    leftCell.AddElement(logo);
                }
                catch
                {
                    // If logo fails, add text
                    Paragraph companyName = new Paragraph("KINGLAND\nMARKETING COMPANY INC.",
                        FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, primaryBlue));
                    leftCell.AddElement(companyName);
                }
            }
            else
            {
                Paragraph companyName = new Paragraph("KINGLAND\nMARKETING COMPANY INC.",
                    FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, primaryBlue));
                leftCell.AddElement(companyName);
            }

            // Right side - Date
            PdfPCell rightCell = new PdfPCell();
            rightCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            rightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            rightCell.PaddingBottom = 15;

            Paragraph dateText = new Paragraph();
            dateText.Add(new Chunk(purchaseOrder.PurchaseOrderDate.ToString("MMMM dd, yyyy") + "\n",
                FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, primaryBlue)));
            dateText.Add(new Chunk("Purchase Order Date",
                FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.GRAY)));
            dateText.Alignment = Element.ALIGN_RIGHT;
            rightCell.AddElement(dateText);

            headerTable.AddCell(leftCell);
            headerTable.AddCell(rightCell);

            document.Add(headerTable);
        }

        private void AddTitleBar(Document document, string purchaseOrderNumber)
        {
            PdfPTable titleTable = new PdfPTable(2);
            titleTable.WidthPercentage = 100;
            titleTable.SetWidths(new float[] { 1f, 1f });
            titleTable.SpacingBefore = 10;

            iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);

            PdfPCell leftTitle = new PdfPCell(new Phrase("Purchase Order    #", titleFont));
            leftTitle.BackgroundColor = primaryBlue;
            leftTitle.Border = iTextSharp.text.Rectangle.NO_BORDER;
            leftTitle.Padding = 8;
            leftTitle.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell rightTitle = new PdfPCell(new Phrase(purchaseOrderNumber, titleFont));
            rightTitle.BackgroundColor = primaryBlue;
            rightTitle.Border = iTextSharp.text.Rectangle.NO_BORDER;
            rightTitle.Padding = 8;
            rightTitle.HorizontalAlignment = Element.ALIGN_CENTER;

            titleTable.AddCell(leftTitle);
            titleTable.AddCell(rightTitle);

            document.Add(titleTable);
        }

        private void AddPurchaseOrderInfo(Document document, PurchaseOrderData purchaseOrder)
        {
            PdfPTable infoTable = new PdfPTable(2);
            infoTable.WidthPercentage = 100;
            infoTable.SetWidths(new float[] { 1f, 3f });
            infoTable.SpacingBefore = 15;

            iTextSharp.text.Font labelFont = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, darkGray);
            iTextSharp.text.Font valueFont = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Purchase Order to section
            AddSimpleInfoRow(infoTable, "Purchase Order to:", "", labelFont, valueFont);
            AddSimpleInfoRow(infoTable, "Name:", purchaseOrder.PurchaseOrderToName, labelFont, valueFont);
            AddSimpleInfoRow(infoTable, "Address:", purchaseOrder.PurchaseOrderToAddress, labelFont, valueFont);
            AddSimpleInfoRow(infoTable, "Attn:", purchaseOrder.Attention, labelFont, valueFont);
            AddSimpleInfoRow(infoTable, "Contact No.:", purchaseOrder.ContactNumber, labelFont, valueFont);

            document.Add(infoTable);

            // Purchase Order by section
            PdfPTable infoTable2 = new PdfPTable(2);
            infoTable2.WidthPercentage = 100;
            infoTable2.SetWidths(new float[] { 1f, 3f });
            infoTable2.SpacingBefore = 10;

            AddSimpleInfoRow(infoTable2, "Purchase Order by:", "", labelFont, valueFont);
            AddSimpleInfoRow(infoTable2, "Company Name:", purchaseOrder.PurchaseOrderByName, labelFont, valueFont);
            AddSimpleInfoRow(infoTable2, "Address:", purchaseOrder.PurchaseOrderByAddress, labelFont, valueFont);
            AddSimpleInfoRow(infoTable2, "Tin:", purchaseOrder.Tin, labelFont, valueFont);

            document.Add(infoTable2);
        }

        private void AddShippingInfo(Document document, PurchaseOrderData purchaseOrder)
        {
            PdfPTable shippingTable = new PdfPTable(5);
            shippingTable.WidthPercentage = 100;
            shippingTable.SetWidths(new float[] { 2f, 2f, 1.5f, 1.5f, 1f });
            shippingTable.SpacingBefore = 15;

            iTextSharp.text.Font headerFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

            AddColoredHeaderCell(shippingTable, "Shipped / Delivered to", headerFont);
            AddColoredHeaderCell(shippingTable, "Contact", headerFont);
            AddColoredHeaderCell(shippingTable, "PAYMENT", headerFont);
            AddColoredHeaderCell(shippingTable, "DELIVERY DATE", headerFont, 2);

            iTextSharp.text.Font normalFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font labelFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            AddDataCell(shippingTable, "Name: " + purchaseOrder.ShippedToName, normalFont);
            AddDataCell(shippingTable, purchaseOrder.ShippedToContact, normalFont);
            AddDataCell(shippingTable, purchaseOrder.PaymentTerms, normalFont);
            AddDataCell(shippingTable, purchaseOrder.DeliveryDate, normalFont, Element.ALIGN_LEFT, 2);

            AddDataCell(shippingTable, "Address: " + purchaseOrder.ShippedToAddress, normalFont);
            AddDataCell(shippingTable, purchaseOrder.ShippedToContactNumber, normalFont);

            // Created By field - spans across PAYMENT and DELIVERY DATE columns
            PdfPCell createdByCell = new PdfPCell();
            createdByCell.Colspan = 3;
            createdByCell.HorizontalAlignment = Element.ALIGN_LEFT;
            createdByCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            createdByCell.Padding = 5;
            createdByCell.MinimumHeight = 20;

            Paragraph createdByText = new Paragraph();
            createdByText.Add(new Chunk("Created By: ", labelFont));
            createdByText.Add(new Chunk(purchaseOrder.CreatedBy, normalFont));
            createdByCell.AddElement(createdByText);

            shippingTable.AddCell(createdByCell);

            document.Add(shippingTable);
        }

        private void AddItemsTable(Document document, PurchaseOrderData purchaseOrder)
        {
            PdfPTable itemsTable = new PdfPTable(7);
            itemsTable.WidthPercentage = 100;
            itemsTable.SetWidths(new float[] { 1.5f, 3f, 1.5f, 1f, 1f, 1.5f, 1.5f });
            itemsTable.SpacingBefore = 10;

            iTextSharp.text.Font headerFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

            // Headers
            AddColoredHeaderCell(itemsTable, "ITEM NO", headerFont);
            AddColoredHeaderCell(itemsTable, "DESCRIPTION", headerFont);
            AddColoredHeaderCell(itemsTable, "Brand", headerFont);
            AddColoredHeaderCell(itemsTable, "QTY", headerFont);
            AddColoredHeaderCell(itemsTable, "Unit", headerFont);
            AddColoredHeaderCell(itemsTable, "UNIT PRICE", headerFont);
            AddColoredHeaderCell(itemsTable, "TOTAL", headerFont);

            iTextSharp.text.Font normalFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Add items
            foreach (var item in purchaseOrder.Items)
            {
                AddDataCell(itemsTable, item.ItemNo, normalFont);
                AddDataCell(itemsTable, item.Description, normalFont);
                AddDataCell(itemsTable, item.Brand, normalFont);
                AddDataCell(itemsTable, item.Quantity.ToString(), normalFont, Element.ALIGN_CENTER);
                AddDataCell(itemsTable, item.Unit, normalFont, Element.ALIGN_CENTER);
                AddDataCell(itemsTable, "₱" + item.UnitPrice.ToString("N2"), normalFont, Element.ALIGN_RIGHT);
                AddDataCell(itemsTable, "₱" + item.Total.ToString("N2"), normalFont, Element.ALIGN_RIGHT);
            }

            // Add empty rows to fill space
            for (int i = purchaseOrder.Items.Count; i < 10; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    AddDataCell(itemsTable, "", normalFont);
                }
            }

            document.Add(itemsTable);
        }

        private void AddRemarksSection(Document document, PurchaseOrderData purchaseOrder)
        {
            PdfPTable remarksTable = new PdfPTable(1);
            remarksTable.WidthPercentage = 60;
            remarksTable.HorizontalAlignment = Element.ALIGN_LEFT;
            remarksTable.SpacingBefore = 10;

            iTextSharp.text.Font labelFont = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font normalFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Remarks cell
            PdfPCell remarksCell = new PdfPCell();
            remarksCell.Border = iTextSharp.text.Rectangle.BOX;
            remarksCell.BorderColor = primaryBlue;
            remarksCell.BorderWidth = 2;
            remarksCell.Padding = 10;
            remarksCell.MinimumHeight = 80;

            Paragraph remarksText = new Paragraph();
            remarksText.Add(new Chunk("Remarks/Instructions:\n", labelFont));
            if (!string.IsNullOrEmpty(purchaseOrder.Remarks))
            {
                remarksText.Add(new Chunk(purchaseOrder.Remarks, normalFont));
            }
            remarksCell.AddElement(remarksText);

            remarksTable.AddCell(remarksCell);

            document.Add(remarksTable);
        }

        private void AddTotalsSection(Document document, PurchaseOrderData purchaseOrder)
        {
            PdfPTable totalsTable = new PdfPTable(3);
            totalsTable.WidthPercentage = 100;
            totalsTable.SetWidths(new float[] { 2f, 2f, 1f });
            totalsTable.SpacingBefore = 10;

            iTextSharp.text.Font labelFont = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font valueFont = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font totalFont = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, BaseColor.WHITE);

            // Thank you section
            PdfPCell thankYouCell = new PdfPCell(new Phrase("THANK YOU",
                    FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.NORMAL, primaryBlue)));
            thankYouCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            thankYouCell.Rowspan = 7;
            thankYouCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            totalsTable.AddCell(thankYouCell);

            // Totals
            AddTotalRow(totalsTable, "SUBTOTAL", "₱" + purchaseOrder.Subtotal.ToString("N2"), labelFont, valueFont, false);
            AddTotalRow(totalsTable, "TAX RATE", purchaseOrder.TaxRate, labelFont, valueFont, false);
            AddTotalRow(totalsTable, "VAT INPUT TAX", "₱" + purchaseOrder.VatInputTax.ToString("N2"), labelFont, valueFont, false);
            AddTotalRow(totalsTable, "AMOUNT (NET OF VAT)", "₱" + purchaseOrder.AmountNetOfVat.ToString("N2"), labelFont, valueFont, false);
            AddTotalRow(totalsTable, "SHIPPING/HANDLING", "₱" + purchaseOrder.Shipping.ToString("N2"), labelFont, valueFont, false);
            AddTotalRow(totalsTable, "OTHER", "₱" + purchaseOrder.Other.ToString("N2"), labelFont, valueFont, false);
            AddTotalRow(totalsTable, "TOTAL AMOUNT", "₱" + purchaseOrder.TotalAmount.ToString("N2"), totalFont, totalFont, true);

            document.Add(totalsTable);
        }

        private void AddFooter(Document document, PurchaseOrderData purchaseOrder)
        {
            // Signature section
            PdfPTable signatureTable = new PdfPTable(2);
            signatureTable.WidthPercentage = 100;
            signatureTable.SpacingBefore = 20;

            signatureTable.AddCell(CreateSignatureCell(purchaseOrder.ApprovedBy, purchaseOrder.ApprovedByTitle, "Approved by:"));
            signatureTable.AddCell(CreateSignatureCell(purchaseOrder.CheckedBy, purchaseOrder.CheckedByTitle, "Checked by:"));

            document.Add(signatureTable);

            // Company footer
            PdfPTable footerTable = new PdfPTable(2);
            footerTable.WidthPercentage = 100;
            footerTable.SpacingBefore = 20;

            iTextSharp.text.Font footerFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);

            PdfPCell footerLeft = new PdfPCell();
            footerLeft.BackgroundColor = primaryBlue;
            footerLeft.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footerLeft.Padding = 10;

            Paragraph leftText = new Paragraph();
            leftText.Add(new Chunk("Phase 4B Block 7 Lot 28 Golden City, Dila\n", footerFont));
            leftText.Add(new Chunk("City of Santa Rosa, Laguna\n", footerFont));
            leftText.Add(new Chunk("Philippines 4026", footerFont));
            footerLeft.AddElement(leftText);

            PdfPCell footerRight = new PdfPCell();
            footerRight.BackgroundColor = primaryBlue;
            footerRight.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footerRight.Padding = 10;
            footerRight.HorizontalAlignment = Element.ALIGN_RIGHT;

            Paragraph rightText = new Paragraph();
            rightText.Add(new Chunk("For questions concerning this Purchase Order, please contact\n", footerFont));
            rightText.Add(new Chunk("Richard A. Abanilla, 0917-198-8306, r.abanilla@kingland.ph\n", footerFont));
            rightText.Add(new Chunk("inquiry@kingland.ph\n", footerFont));
            rightText.Add(new Chunk("https://shop.kingland.ph", footerFont));
            rightText.Alignment = Element.ALIGN_RIGHT;
            footerRight.AddElement(rightText);

            footerTable.AddCell(footerLeft);
            footerTable.AddCell(footerRight);

            document.Add(footerTable);
        }

        // Helper methods
        private void AddSimpleInfoRow(PdfPTable table, string label, string value, iTextSharp.text.Font labelFont, iTextSharp.text.Font valueFont)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont));
            labelCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            labelCell.PaddingBottom = 3;

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont));
            valueCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            valueCell.PaddingBottom = 3;

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }

        private void AddInfoRow(PdfPTable table, string label, string value, iTextSharp.text.Font labelFont, iTextSharp.text.Font? valueFont = null)
        {
            if (valueFont == null) valueFont = labelFont;

            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont));
            labelCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            labelCell.PaddingBottom = 3;

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont));
            valueCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            valueCell.Colspan = 3;
            valueCell.PaddingBottom = 3;

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }

        private void AddEmptyRow(PdfPTable table)
        {
            for (int i = 0; i < 4; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(" "));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.FixedHeight = 5;
                table.AddCell(cell);
            }
        }

        private void AddColoredHeaderCell(PdfPTable table, string text, iTextSharp.text.Font font, int colspan = 1)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.BackgroundColor = primaryBlue;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            cell.Colspan = colspan;
            table.AddCell(cell);
        }

        private void AddDataCell(PdfPTable table, string text, iTextSharp.text.Font font, int alignment = Element.ALIGN_LEFT, int colspan = 1)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 5;
            cell.Colspan = colspan;
            cell.MinimumHeight = 20;
            table.AddCell(cell);
        }

        private void AddTotalRow(PdfPTable table, string label, string value, iTextSharp.text.Font labelFont, iTextSharp.text.Font valueFont, bool isTotal)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont));
            labelCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            labelCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            labelCell.Padding = 5;

            if (isTotal)
            {
                labelCell.BackgroundColor = primaryBlue;
            }

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont));
            valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            valueCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            valueCell.Padding = 5;

            if (isTotal)
            {
                valueCell.BackgroundColor = primaryBlue;
            }

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }

        private PdfPCell CreateSignatureCell(string name, string title, string headerText)
        {
            PdfPCell cell = new PdfPCell();
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.PaddingTop = 10;

            Paragraph header = new Paragraph(headerText, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD, primaryBlue));
            header.Alignment = Element.ALIGN_CENTER;

            Paragraph sig = new Paragraph();
            sig.Add(new Chunk("\n\n\n", FontFactory.GetFont("Arial", 9)));
            sig.Add(new Chunk("_____________________\n", FontFactory.GetFont("Arial", 9)));
            sig.Add(new Chunk(name + "\n", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD)));
            sig.Add(new Chunk(title, FontFactory.GetFont("Arial", 9)));
            sig.Alignment = Element.ALIGN_CENTER;

            cell.AddElement(header);
            cell.AddElement(sig);
            return cell;
        }

        private PurchaseOrderData GetPurchaseOrderData(int purchaseOrderId)
        {
            string connectionString = "server=localhost;database=kmci_database;uid=root;pwd=;";
            PurchaseOrderData data = new PurchaseOrderData();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Get purchase order header with customer info from proper tables
                string query = @"
                    SELECT 
                        po.*,
                        c.company_name,
                        c.tin,
                        ca.house_num,
                        ca.street,
                        ca.barangay,
                        ca.city,
                        ca.province,
                        p.proponent_name,
                        p.proponent_number,
                        po.created_by
                        FROM purchase_order po
                        JOIN company_list c ON po.company_id = c.id
                        LEFT JOIN company_address ca ON po.address_id = ca.id
                        LEFT JOIN proponents p ON po.proponent_id = p.id
                        WHERE po.purchase_order_id = @purchaseOrderId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@purchaseOrderId", purchaseOrderId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception($"No purchase order found for ID: {purchaseOrderId}");
                        }

                        data.PurchaseOrderNumber = "KMCI-PO-" + purchaseOrderId.ToString("D4");
                        data.PurchaseOrderDate = Convert.ToDateTime(reader["purchase_order_date"]);

                        // Purchase Order To
                        data.PurchaseOrderToName = reader["company_name"]?.ToString() ?? "";

                        // Concatenate address, excluding "N/A" parts
                        List<string> addressParts = new List<string>();
                        string[] addressFields = { "house_num", "street", "barangay", "city", "province" };

                        foreach (string field in addressFields)
                        {
                            string value = reader[field]?.ToString() ?? "";
                            if (!string.IsNullOrEmpty(value) && value.Trim().ToUpper() != "N/A")
                            {
                                addressParts.Add(value.Trim());
                            }
                        }

                        data.PurchaseOrderToAddress = string.Join(", ", addressParts);
                        data.Attention = reader["proponent_name"]?.ToString() ?? "";
                        data.ContactNumber = reader["proponent_number"]?.ToString() ?? "";

                        // Purchase Order By (Your company info)
                        data.PurchaseOrderByName = "Kingland Marketing Company Inc.";
                        data.PurchaseOrderByAddress = "Phase 4B Blk 7 Lot 28 Golden City, Dila, City of Santa Rosa, Laguna, Philippines 4026";
                        data.Tin = "645-630-230-000";

                        // Shipping info
                        data.ShippedToName = reader["company_name"]?.ToString() ?? "";
                        data.ShippedToAddress = string.Join(", ", addressParts);
                        data.ShippedToContact = reader["proponent_name"]?.ToString() ?? "";
                        data.ShippedToContactNumber = reader["proponent_number"]?.ToString() ?? "";

                        data.PaymentTerms = reader["payment"]?.ToString() + " CD";
                        data.DeliveryDate = reader["delivery_time"] != DBNull.Value ?
                        reader["delivery_time"]?.ToString() + " Days" :
                        "3-5 Days upon receipt of PO";

                        // Created By
                        data.CreatedBy = reader["created_by"]?.ToString() ?? "Unknown";

                        // Totals
                        data.Subtotal = Convert.ToDecimal(reader["bid_price"]);
                        data.TaxRate = "12%";
                        data.VatInputTax = data.Subtotal * 0.12m;
                        data.AmountNetOfVat = data.Subtotal - data.VatInputTax;
                        data.Shipping = 0;
                        data.Other = 0;
                        data.TotalAmount = data.Subtotal;

                        data.Remarks = reader["remarks"] != DBNull.Value ? reader["remarks"]?.ToString() ?? "" : "";

                        // Signatures
                        data.ApprovedBy = "Richard A. Abanilla";
                        data.ApprovedByTitle = "President";
                        data.CheckedBy = "Katrina M. Abanilla";
                        data.CheckedByTitle = "Vice President";
                    }
                }

                // Get purchase order items
                string itemsQuery = @"
                    SELECT poi.*, p.prod_name, p.brand, p.uom
                    FROM purchase_order_items poi
                    JOIN product_list p ON poi.sku_upc = p.sku_upc
                    WHERE poi.purchase_order_id = @purchaseOrderId
                    GROUP BY poi.sku_upc, poi.quantity, poi.unit_price, poi.sub_total, p.prod_name, p.brand
                    ORDER BY poi.item_id AND p.prod_name";

                using (MySqlCommand cmd = new MySqlCommand(itemsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@purchaseOrderId", purchaseOrderId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Items.Add(new PurchaseOrderItem
                            {
                                ItemNo = reader["sku_upc"]?.ToString() ?? "",
                                Description = reader["prod_name"]?.ToString() ?? "",
                                Brand = reader["brand"]?.ToString() ?? "",
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Unit = reader["uom"]?.ToString() ?? "",
                                UnitPrice = Convert.ToDecimal(reader["proposal_price"]),
                                Total = Convert.ToDecimal(reader["sub_total"])
                            });
                        }
                    }
                }
            }

            return data;
        }
    }

    // Data classes
    public class PurchaseOrderData
    {
        public string PurchaseOrderNumber { get; set; } = "";
        public DateTime PurchaseOrderDate { get; set; }

        public string PurchaseOrderToName { get; set; } = "";
        public string PurchaseOrderToAddress { get; set; } = "";
        public string Attention { get; set; } = "";
        public string ContactNumber { get; set; } = "";

        public string PurchaseOrderByName { get; set; } = "";
        public string PurchaseOrderByAddress { get; set; } = "";
        public string Tin { get; set; } = "";

        public string ShippedToName { get; set; } = "";
        public string ShippedToAddress { get; set; } = "";
        public string ShippedToContact { get; set; } = "";
        public string ShippedToContactNumber { get; set; } = "";
        public string PaymentTerms { get; set; } = "";
        public string DeliveryDate { get; set; } = "";
        public string CreatedBy { get; set; } = "";

        public decimal Subtotal { get; set; }
        public string TaxRate { get; set; } = "";
        public decimal VatInputTax { get; set; }
        public decimal AmountNetOfVat { get; set; }
        public decimal Shipping { get; set; }
        public decimal Other { get; set; }
        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; } = "";

        public string ApprovedBy { get; set; } = "";
        public string ApprovedByTitle { get; set; } = "";
        public string CheckedBy { get; set; } = "";
        public string CheckedByTitle { get; set; } = "";

        public List<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
    }

    public class PurchaseOrderItem
    {
        public string ItemNo { get; set; } = "";
        public string Description { get; set; } = "";
        public string Brand { get; set; } = "";
        public int Quantity { get; set; }
        public string Unit { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}