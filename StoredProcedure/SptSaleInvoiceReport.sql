USE [BMS]
GO
/****** Object:  StoredProcedure [dbo].[SptSaleInvoiceReport]    Script Date: 11/26/2016 11:14:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Akber Ahmed>
-- Create date: <Create Date,,08-Aug-2016>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SptSaleInvoiceReport]
@TypeId int = NULL,
@DateFilterType int  = NULL,
@StartDate DATETIME = NULL,
@EndDate DATETIME = NULL,
@InvoiceStatusId INT = -1 
AS
BEGIN
	IF(@TypeId = 1)
	Begin 
		--		DECLARE @DateFilterType int  = 1
		--DECLARE @StartDate DATETIME = '2016-08-01'
		--DECLARE @EndDate DATETIME = '2016-11-30'
		--DECLARE @InvoiceStatusId INT = -1 

		SELECT si.SaleInvoiceId, si.SaleInvoiceParentId, si.InvoiceNumber, si.InvoiceDate,
			   si.CounterId, si.SaleTypeId, si.BranchId, si.DeliveryDate, 
			   CASE WHEN si2.SaleInvoiceId is null then 'Not Delivered' else 'Delivered' end InvoiceStatus,
			   CASE WHEN si2.SaleInvoiceId is null then 0 else 1 end InvoiceStatusId ,si.NetAmount 
			   INTO #SaleInvoice
		  FROM SaleInvoice AS si
		LEFT OUTER JOIN SaleInvoice AS si2 ON si2.SaleInvoiceParentId = si.SaleInvoiceId
		WHERE si.RecordStatus <> 0 AND si.RecordStatus = 2 AND 
		CASE WHEN @DateFilterType = 1 THEN si.InvoiceDate
			 WHEN @DateFilterType = 2 THEN si.DeliveryDate  
		ELSE si.DataEntryDate 
		END BETWEEN @StartDate AND @EndDate


		SELECT s.*
		  FROM #SaleInvoice s
		WHERE s.InvoiceStatusId = CASE WHEN @InvoiceStatusId = -1 THEN s.InvoiceStatusId ELSE @InvoiceStatusId END

		DROP TABLE #SaleInvoice
	END
	
	IF (@TypeId = 2)
	BEGIN
		--DECLARE @StartDate DATETIME = '2016-08-01'
		--DECLARE @EndDate DATETIME = '2016-11-30'

		SELECT  sii.ProductItemId, pi1.barcode, pi1.productItemname, Sum(ISNULL(sii.Quantity,0)) Quantity, sii.Price
		INTO #SaleInvoice2
		  FROM SaleInvoice AS si
		LEFT OUTER JOIN SaleInvoiceItem AS sii ON sii.SaleInvoiceId = si.SaleInvoiceId AND sii.RecordStatus <> 0
		LEFT OUTER JOIN ProductsInfo AS pi1
					   ON pi1.productItemId = sii.ProductItemId
		WHERE si.DataEntryDate BETWEEN @StartDate AND @EndDate
		GROUP BY sii.ProductItemId, pi1.barcode, pi1.productItemname, sii.Price 

		SELECT * FROM #SaleInvoice2 s
		ORDER BY s.Quantity DESC 

		DROP TABLE #SaleInvoice
	END

END

